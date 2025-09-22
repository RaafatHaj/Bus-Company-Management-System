


CREATE procedure [dbo].[sp_AssignVehicleToTrip3]
                 @TripId int,
				 -- Table of Trips Ids ( valid for one or multipel trips )
				 @VehicleId int,
				 @MainTripDateTime datetime ,
				 @ReturnTripDateTime datetime =null,
				 @MainTripCurrentDateTime datetime,
				 @ReturnTripCurrentDateTime datetime,
				 @IsAssigend bit output,
				 @ReturnTripId int output,
				 @ErrorMessage nvarchar(max) output
as
begin

declare @DefualtBreakInMinits int=cast( dbo.GetApplicationConst('MinBreak') as int);
--declare @AssigmentId int;


declare @ReverseRouteId int;
declare @RouteId int;
--declare @ReverseRouteId int;
declare @Seats int;

--declare @MinBreakSpan int;
declare @TripEstimeatedDuration int;
declare @MianTripTime time = cast(@MainTripCurrentDateTime as time);
declare @ReturnTripTime time = cast(@ReturnTripCurrentDateTime as time);
declare @StationStopMinutes int;
declare @BreakMinutes int;
declare @RouteStationsNumber int;

declare @HasBresk bit;
declare @StationOrderNextToBreak int;
declare @ArrivedStationOrder int;

begin transaction
begin try


--set @MinBreakSpan =cast (dbo.GetApplicationConst('MinBreak') as int);

select @HasBresk=t.HasBreak,@StationOrderNextToBreak=t.StationOrderNextToBreak,@ArrivedStationOrder=t.ArrivedStationOrder,
@TripEstimeatedDuration=r.EstimatedTime ,@ReverseRouteId=r.ReverseRouteId ,@RouteStationsNumber=r.StationsNumber,
@StationStopMinutes=t.StationStopMinutes,@BreakMinutes=t.BreakMinutes,
@RouteId=t.RouteId
from Trips t inner join Routes r on t.RouteId=r.RouteId
where t.Id=@TripId;



declare @TripRealOccupiedDureaion int;

set @TripRealOccupiedDureaion=(@TripEstimeatedDuration)+(@DefualtBreakInMinits)+((@RouteStationsNumber-2)*@StationStopMinutes)+
case when @BreakMinutes is null then 0 else @BreakMinutes end;


--****************************************** Cancel if ( Return time less then main trip time + trip duration + break duration)

if(
@ReturnTripCurrentDateTime <DATEADD(MINUTE ,@TripRealOccupiedDureaion, @MainTripCurrentDateTime))
begin

set @ErrorMessage='Return trip timing is not valid , retrurn trip timing should not be greater then Main trip timing with trip duration ';
set @IsAssigend=0;
set @ReturnTripId=0
rollback;
return;

end


--****************************************** Cancel if ( Vehicle is not available )

if(not exists (
select 1 from GetAvalibleVehicleForTrip3 (@TripId)  av
where av.VehicleId=@VehicleId 
--and @MainTripCurrentDateTime > DATEADD(minute,@DefualtBreakInMinits, av.AvailabilityStartTime )
--and @ReturnTripCurrentDateTime < DATEADD(minute,@DefualtBreakInMinits, av.AvailabilityEndTime ) 

))
begin

set @ErrorMessage='Vehicle is not available for this trip.';
set @IsAssigend=0;
set @ReturnTripId=0
rollback;
return;

end


--****************************************** update Trips 1.Update Main Trip time , date and status
--													      2.Delete Return Trips if existes , and insert new one
--declare @RouteId int;
--declare @ReverseRouteId int;
--declare @Seats int;

--select @RouteId=t.RouteId ,@Seats=t.Seats from Trips t 
--where t.Id=@TripId

--select @ReverseRouteId=r.ReverseRouteId from Routes r
--where r.RouteId=@RouteId


--****************** Check if there is a trip has the same timing and route

if(exists( select 1 from Trips t
where t.Time=@MianTripTime and t.Date=cast(@MainTripCurrentDateTime as date) and t.RouteId=@RouteId and t.Id <>@TripId))
begin

set @ErrorMessage='Main trip timing is not valid , there is another trip has the same timing with the same route.';
set @IsAssigend=0;
set @ReturnTripId=0
rollback;
return;

end;


--****************** Update Main trip and delete return trip if Exists

--update Trips 
--set Time=cast(@MainTripDateTime as time) ,Date=@MainTripDateTime , status=0
--where Id=@TripId;

delete from Trips 
where MainTripId=@TripId;


--****************** Check if there is a trip has the same timing and route of the return trip

if(exists( select 1 from Trips t
where t.Time=@ReturnTripTime and t.Date=cast(@ReturnTripCurrentDateTime as date) and t.RouteId=@ReverseRouteId ))
begin

set @ErrorMessage='Return trip timing is not valid , there is another trip has the same timing with the same route.';
set @IsAssigend=0;
set @ReturnTripId=0
rollback;
return;

end;

--****************** Insert New Return Trip ...

--with cte
--as
--(

--select t.Seats,r.ReverseRouteId from Trips t inner join Routes r on t.RouteId=r.RouteId
--where t.Id =@TripId

--)
select @Seats=v.Seats from Vehicles v where v.VehicleId=@VehicleId

--Date,Time,status,RouteId,HasBookedSeat,IsVehicleMoving,HasBreak,StationOrderNextToBreak,StationStopMinutes,
--BreakMinutes,ArrivedStationOrder

insert into Trips(Date,Time,status,	RouteId,Seats,HasBookedSeat,IsVehicleMoving,HasBreak,StationOrderNextToBreak,StationStopMinutes,
BreakMinutes,ArrivedStationOrder,MainTripId)
values( cast(@ReturnTripCurrentDateTime as datetime) ,cast(@ReturnTripCurrentDateTime as time),0,@ReverseRouteId,@Seats,0,0,@HasBresk
,@StationOrderNextToBreak,@StationStopMinutes,@BreakMinutes,@ArrivedStationOrder,@TripId )



set @ReturnTripId=SCOPE_IDENTITY();

update Trips 
set Time=cast(@MainTripCurrentDateTime as time) ,Date=@MainTripCurrentDateTime ,Seats=@Seats, status=0 ,ReturnTripId=@ReturnTripId
where Id=@TripId;



--****************************************** Insert new assigmen for Main and Return Trips ...


with cte
as
(


select t.Id,t.Date,t.Time ,r.FirstStationId, r.LastStationId,r.EstimatedTime from Trips t inner join Routes r on t.RouteId=r.RouteId
where t.Id in (@TripId , @ReturnTripId)


)
insert into TripAssignments(TripId,VehicleId,DepartureStationId,StopedStationId,StartDateAndTime,EndDateAndTime)
select c.Id ,@VehicleId ,c.FirstStationId ,c.LastStationId ,cast(c.Date as datetime)+cast(c.Time as datetime),
DATEADD(MINUTE,@TripRealOccupiedDureaion,cast(c.Date as datetime)+cast(c.Time as datetime))
from cte c;


----****************************************** Set Trips Pattern ...


exec sp_SetTripPattern
                 @RouteId = @RouteId,
				 @Time = @MianTripTime;

exec sp_SetTripPattern
                 @RouteId = @ReverseRouteId,
				 @Time = @ReturnTripTime;



if(@MainTripDateTime is not null and @MainTripDateTime <> @MainTripCurrentDateTime)
begin

declare @Time1 time=cast(@MainTripDateTime as time);
exec sp_SetTripPattern
                 @RouteId = @RouteId,
				 @Time = @Time1;
end



if(@ReturnTripDateTime is not null and @ReturnTripDateTime <> @ReturnTripCurrentDateTime)
begin
declare @Time2 time = cast (@ReturnTripDateTime as time);
exec sp_SetTripPattern
                 @RouteId = @ReverseRouteId,
				 @Time = @Time2;
end



set @ErrorMessage='No Error.';


set @IsAssigend=1;
commit;		
return;

end try
begin catch
set @IsAssigend=0;
set @ReturnTripId=0
rollback;
end catch




end