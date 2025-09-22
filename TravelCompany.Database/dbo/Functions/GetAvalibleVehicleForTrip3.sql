
--select *from GetAvalibleVehicleForTrip3(10649)

CREATE FUNCTION [dbo].[GetAvalibleVehicleForTrip3]
(
                     @TripId int
)
RETURNS @ResultTable TABLE (VehicleId int , AvailabilityStartTime datetime , AvailabilityEndTime datetime)
AS
begin

declare @DefualtBreakInMinits int=cast( dbo.GetApplicationConst('MinBreak') as int);
declare @TripDateAndTime datetime ;
declare @StationId int;
declare @TripSpanInMinits int;


declare @TripTime time;
declare @TripDate date;
declare @StationStopMinutes int;
declare @BreakMinutes int;
declare @RouteStationsNumber int;
declare @ReturnTripId int;
--select @TripTime=t.Time ,@TripDate=t.Date from Trips t where t.Id=@TripId;
--select @TripDate=t.Date from Trips t where t.Id=@TripId;

select @StationStopMinutes=t.StationStopMinutes,@BreakMinutes=t.BreakMinutes,@RouteStationsNumber=r.StationsNumber,
@TripTime=t.Time ,@TripDate=t.Date, @StationId=r.FirstStationId , @TripSpanInMinits=r.EstimatedTime 
,@ReturnTripId=case when t.ReturnTripId is null then 0 else t.ReturnTripId end
from Trips t inner join Routes r on t.RouteId=r.RouteId
where t.Id=@TripId;

set @TripDateAndTime = cast (@TripDate as datetime) + cast(@TripTime as datetime);

--declare @TripRealOccupiedDureaion int;
--set @TripRealOccupiedDureaion=(@TripSpanInMinits*2)+(@DefualtBreakInMinits*3)+((@RouteStationsNumber-2)*@StationStopMinutes)+
--case when @BreakMinutes is null then 0 else @BreakMinutes end;

declare @TripRealOccupiedDureaion int;
set @TripRealOccupiedDureaion=(@TripSpanInMinits*2+@DefualtBreakInMinits*3+(((@RouteStationsNumber-2)*@StationStopMinutes)*2)+
case when @BreakMinutes is null then 0 else @BreakMinutes*2 end);




--declare @OneWeekBeforeTripDate datetime= DATEADD(day,-7,@TripDateAndTime);
declare @OneWeekAfterTripDate datetime= DATEADD(day,7,@TripDateAndTime);


declare @FilterStartDate datetime=DATEADD(day,-7,CAST(CONVERT(date, @TripDateAndTime) AS datetime)); -- one week before at 00:00 am

if(@FilterStartDate < GETDATE())
begin
set @FilterStartDate=GETDATE();
end;


-->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
--*** Filter Trips Assigments table so that we get Assigments 10 days befor trip and 10 days after trip ...

with 
HomeStationVehicles
as
(
select *from Vehicles v where v.StationId=@StationId
)
,FilteredData0
as
(
select 

ta.VehicleId , ta.DepartureStationId ,ta.StartDateAndTime,ta.EndDateAndTime
,ta.StopedStationId

from TripAssignments ta 

where ta.EndDateAndTime between DATEADD(day,-14, @TripDateAndTime) and DATEADD(day,14, @TripDateAndTime) 
and ta.TripId not in (@TripId , @ReturnTripId)

)
,FilteredData
as
(

select
f.*, 
lag(f.EndDateAndTime,1)over(partition by f.VehicleId order by f.StartDateAndTime) as PervAssigmentEndDateTime,
lead(f.StartDateAndTime,1)over(partition by f.VehicleId order by f.StartDateAndTime) as NextAssigmenStartDateTime

from FilteredData0 f 
where f.EndDateAndTime between @FilterStartDate and DATEADD(day,7, @TripDateAndTime) 
--and f.StopedStationId=@StationId

)

-->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
-- *** Handle Vehicles Stoped On Station and do not belong to Home Station ...
,ParkedVehiclesAssigments
as
(
select 
f.VehicleId , f.PervAssigmentEndDateTime,f.DepartureStationId,f.StartDateAndTime,f.EndDateAndTime
,f.StopedStationId,f.NextAssigmenStartDateTime,HomeStationVehicle=0


from FilteredData f
where f.StopedStationId =@StationId 
and f.VehicleId not in (select v.VehicleId from HomeStationVehicles v)

)
-->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
-- *** Get the last assigment for the vehicles that have assigments befor the trip  ...
,VehiclesHaveNoAssigmentsFromHomeStation
as
(
select 

v.VehicleId , null as PervAssigmentEndDateTime,null as DepartureStationId , null as StartDateAndTime,null as EndDateAndTime
,null as StopedStationId,null as NextAssigmenStartDateTime ,HomeStationVehicle=1

from HomeStationVehicles v

where (not exists (select 1 from FilteredData f where f.VehicleId=v.VehicleId))
)

---->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
---- *** Get Vehicles assigments for Home vehicles that have no assigment befor the trip but just after it ...

,HomeStationVehiclesAssigments
as
(
select 
f.VehicleId , f.PervAssigmentEndDateTime,f.DepartureStationId,f.StartDateAndTime,f.EndDateAndTime
,f.StopedStationId,f.NextAssigmenStartDateTime,HomeStationVehicle=1


from FilteredData f
where f.VehicleId in (select v.VehicleId from HomeStationVehicles v)
)
,Result
as
(
select * from ParkedVehiclesAssigments 
union all
select * from HomeStationVehiclesAssigments
union all
select * from VehiclesHaveNoAssigmentsFromHomeStation

)
--select *from Result
--order by VehicleId,StartDateAndTime
,FilterdResult
as
(
select distinct
r.VehicleId,
case
    when r.HomeStationVehicle =1 then
	     case
		     when r.StartDateAndTime is null then @FilterStartDate
			 when r.StopedStationId <> @StationId then
			     case
				     when r.DepartureStationId <> @StationId then 'NotValid'
					 else
					     case
						     when r.PervAssigmentEndDateTime is null then @FilterStartDate
							 else r.PervAssigmentEndDateTime
						 end

				 end
             when r.StopedStationId = @StationId then r.EndDateAndTime
		 end
    else r.EndDateAndTime
end 
as AvailabilityStartDate,

case
    when r.HomeStationVehicle=1 then
	    case
		    when r.StartDateAndTime is null then  DATEADD(day,7,@TripDateAndTime)
			when r.StopedStationId <> @StationId then
			    case
				    when r.DepartureStationId <> @StationId then 'NotValid'
					else r.StartDateAndTime
				end
			when r.StopedStationId = @StationId then
			    case
				    when r.NextAssigmenStartDateTime is null then  DATEADD(day,7,@TripDateAndTime)
					else r.NextAssigmenStartDateTime
				end
		end
    else r.NextAssigmenStartDateTime

end as AvailabilityEndDate



from Result r
)
,FinalResult
as
(
select *from FilterdResult f
where DATEDIFF(MINUTE,f.AvailabilityStartDate,f.AvailabilityEndDate) >= @TripRealOccupiedDureaion
)

insert into @ResultTable(VehicleId,AvailabilityStartTime,AvailabilityEndTime)
select f.VehicleId,f.AvailabilityStartDate,f.AvailabilityEndDate from FinalResult f

--select f.VehicleId ,s.StationName as HomeStation,  v.Type,v.VehicleNumber,f.AvailabilityStartDate,f.AvailabilityEndDate
--from FinalResult f inner join Vehicles v 
--inner join Stations s on v.StationId=s.StationId
--on f.VehicleId=v.VehicleId
--order by f.VehicleId,f.AvailabilityStartDate
return;

end