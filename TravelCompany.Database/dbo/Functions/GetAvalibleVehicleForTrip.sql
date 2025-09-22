

CREATE FUNCTION [dbo].[GetAvalibleVehicleForTrip]
(
                     @TripId int
)
RETURNS @ResultTable TABLE (VehicleId int , IsAvaliable bit , AvalibilityStartTime datetime , AvalibilityEndTime datetime)
AS
begin


declare @TripDateAndTime datetime ;
declare @StationId int;
declare @TripSpanInMinits int;
declare @DefualtBreakInMinits int=15;


declare @TripTime time;
declare @TripDate date;

select @TripTime=t.Time from Trips t where t.Id=@TripId;
select @TripDate=t.Date from Trips t where t.Id=@TripId;
set @TripDateAndTime = cast (@TripDate as datetime) + cast(@TripTime as datetime);

select @StationId=r.FirstStationId , @TripSpanInMinits=r.EstimatedTime from Trips t inner join Routes r on t.RouteId=r.RouteId
where t.Id=@TripId;




-->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
-- \\\ Get all vehicles existed in the station (owned by station and parking in the station
--                                              or owned by another station but parking in the station at @TripTime) ...

with 
ExistedVehicles
as
(

select v.* from Vehicles v inner join 
 GetAllVehiclesExistedInStationAtTime(@TripDateAndTime ,@StationId) av 
 on v.VehicleId = av.VehicleId
)

-->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
-- \\\ Get existed vehicles Assigments
--     - Saperate them into partitions 
--     - Get availability using Lag

,VehiclesAssigments
as
(

select e.VehicleId ,e.Type,e.VehicleNumber,
lag(ta.EndDateAndTime,1)over(partition by ta.VehicleId order by ta.StartDateAndTime) as AvalibilityStartTime ,
ta.StartDateAndTime as TripStartTime ,ta.EndDateAndTime as TripEndTime,ta.StopedStationId,
case when
 lead(ta.StartDateAndTime,1)over(partition by ta.VehicleId order by ta.StartDateAndTime) is null then 1 else 0 end as NoCommingTrip

from TripAssignments ta right join ExistedVehicles e on ta.VehicleId=e.VehicleId

--     * Not: These lines we will be improving the proformance by avoid filtering all existed data
--            but we will may lose acurancy in some cases .

--where (r1.AvailabilityStartTime is null and r1.AvailabilityEndTime is null ) or
--( r1.AvailabilityEndTime between DATEADD(day,-15,@TripTime) and DATEADD(day,15,@TripTime) ) 

)

-->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
-- \\\ Get Availales vehicles for the trip

,AvailableVehicles 
as
(

select * from VehiclesAssigments va
where (va.TripStartTime is null and va.TripEndTime is null) or
(va.AvalibilityStartTime <= @TripDateAndTime and va.TripStartTime>= DATEADD(MINUTE,@TripSpanInMinits*2+@DefualtBreakInMinits, @TripDateAndTime)) or
(va.AvalibilityStartTime is null and va.TripStartTime>= DATEADD(MINUTE,@TripSpanInMinits*2+@DefualtBreakInMinits, @TripDateAndTime )) or
(va.TripEndTime<=@TripDateAndTime and va.NoCommingTrip=1)
)
--select c.VehicleId from AvailableVehicles c --right join ExistedVehicles e on c.VehicleId=e.VehicleId

insert into @ResultTable(VehicleId, IsAvaliable , AvalibilityStartTime,AvalibilityEndTime)
select e.VehicleId,
case when av.VehicleId is null then 0 else 1 end as IsAvailable,
av.AvalibilityStartTime,

case when av.NoCommingTrip=1 then null else
av.TripStartTime end as  AvalibilityEndTime

from ExistedVehicles e
left join AvailableVehicles av on e.VehicleId=av.VehicleId






return;

end