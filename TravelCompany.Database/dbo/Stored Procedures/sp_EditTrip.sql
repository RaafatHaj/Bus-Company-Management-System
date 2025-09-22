
--HasBreak,StationOrderNextToBreak,StationStopMinutes,
--BreakMinutes


CREATE procedure [dbo].[sp_EditTrip]
                 @TripId int,
				 @VehicleId int=null,

				 @MainTrip_OldDateTime datetime ,
				 @ReturnTrip_OldDateTime datetime =null,
				 @MainTrip_NewDateTime datetime,
				 @ReturnTrip_NewDateTime datetime,

				 @MainTrip_HasBookedSeats bit,
				 @MainTrip_StationStopMinutes int,
				 @MainTrip_HasBreak bit,
				 @MainTrip_BreakMinutes int=null,
				 @MainTrip_StationOrderNextToBreak int=null,
				
			     @ReturnTrip_HasBookedSeats bit,
				 @ReturnTrip_StationStopMinutes int,
				 @ReturnTrip_HasBreak bit,
				 @ReturnTrip_BreakMinutes int,
				 @ReturnTrip_StationOrderNextToBreak int,

				 @IsSuccess bit output,
				 @ReturnTripId int output,
				 @ErrorMessage nvarchar(max) output
as
begin



declare @DefualtBreakInMinits int=cast( dbo.GetApplicationConst('MinBreak') as int);
--declare @AssigmentId int;


declare @RouteId int;
declare @ReverseRouteId int;
declare @Seats int=null;
declare @IsTripActive bit;

declare @TripEstimeatedDuration int;
declare @MianTripTime time = cast(@MainTrip_NewDateTime as time);
declare @ReturnTripTime time = cast(@ReturnTrip_NewDateTime as time);

declare @RouteStationsNumber int;

select @IsTripActive=t.IsVehicleMoving,
@TripEstimeatedDuration=r.EstimatedTime ,@ReverseRouteId=r.ReverseRouteId ,@RouteStationsNumber=r.StationsNumber,
@RouteId=t.RouteId , @ReturnTripId=case when t.ReturnTripId  is not null then t.ReturnTripId else 0 end
from Trips t inner join Routes r on t.RouteId=r.RouteId
where t.Id=@TripId;

begin transaction
begin try


if(@IsTripActive =1 )
begin

set @ErrorMessage='Can not edit active Trip.';
set @IsSuccess=0;
set @ReturnTripId=0

rollback;
return;
end

declare @MainTrip_RealOccupiedDuration int;
declare @ReturnTrip_RealOccupiedDuration int;
--declare @TaskDuration int;
declare @TaskStartDateTime datetime;
declare @TaskEndDateTime datetime;

set @MainTrip_RealOccupiedDuration=(@TripEstimeatedDuration)+(@DefualtBreakInMinits)+((@RouteStationsNumber-2)*@MainTrip_StationStopMinutes)+
case when @MainTrip_BreakMinutes is null then 0 else @MainTrip_BreakMinutes end;

set @ReturnTrip_RealOccupiedDuration=(@TripEstimeatedDuration)+(@DefualtBreakInMinits)+((@RouteStationsNumber-2)*@ReturnTrip_StationStopMinutes)+
case when @ReturnTrip_BreakMinutes is null then 0 else @ReturnTrip_BreakMinutes end;

set @TaskStartDateTime=DATEADD(MINUTE,-@DefualtBreakInMinits,@MainTrip_NewDateTime);
set @TaskEndDateTime=DATEADD(MINUTE,@MainTrip_RealOccupiedDuration+@ReturnTrip_RealOccupiedDuration,@MainTrip_NewDateTime);
--set @TaskDuration=@DefualtBreakInMinits+@MainTrip_RealOccupiedDuration+@ReturnTrip_RealOccupiedDuration;
--****************************************** Cancel if ( Return time less then main trip time + trip duration + break duration)

if(
@ReturnTrip_NewDateTime <DATEADD(MINUTE ,@MainTrip_RealOccupiedDuration, @MainTrip_NewDateTime))
begin

set @ErrorMessage='Return trip timing is not valid , retrurn trip timing should not be greater then Main trip timing with trip duration ';
set @IsSuccess=0;
set @ReturnTripId=0
rollback;
return;

end


--****************************************** Cancel if ( Vehicle is not available of the timing not valid)
if(@VehicleId is not null)
begin


if(not exists (
select 1 from GetAvalibleVehicleForTrip3 (@TripId)  av
where av.VehicleId=@VehicleId and av.AvailabilityStartTime <= @TaskStartDateTime and av.AvailabilityEndTime >= @TaskEndDateTime
))
begin

set @ErrorMessage='Vehicle is not available for this trip or you need to modify timing to match with avaliability space.';
set @IsSuccess=0;
set @ReturnTripId=0
rollback;
return;

end


select @Seats=v.Seats from Vehicles v where v.VehicleId=@VehicleId


end



--****************************************** update Trips 1.Update Main Trip time , date and status
--													      2.Delete Return Trips if existes , and insert new one

--****************** Check if there is a trip has the same timing and route

if(exists( select 1 from Trips t
where t.Time=@MianTripTime and t.Date=cast(@MainTrip_NewDateTime as date) and t.RouteId=@RouteId and t.Id <>@TripId))
begin

set @ErrorMessage='Main trip timing is not valid , there is another trip has the same timing with the same route.';
set @IsSuccess=0;
set @ReturnTripId=0
rollback;
return;

end;


--****************** Update Main trip and delete return trip if Exists


--****************** Check if there is a trip has the same timing and route of the return trip
if(@ReturnTripId <>0)
begin

if(exists( select 1 from Trips t
where t.Time=@ReturnTripTime and t.Date=cast(@ReturnTrip_NewDateTime as date) and t.RouteId=@ReverseRouteId and t.Id <>@ReturnTripId ))
begin
set @ErrorMessage='Return trip timing is not valid , there is another trip has the same timing with the same route.';
set @IsSuccess=0;
set @ReturnTripId=0
rollback;
return;
end

end
else
begin

if(exists( select 1 from Trips t
where t.Time=@ReturnTripTime and t.Date=cast(@ReturnTrip_NewDateTime as date) and t.RouteId=@ReverseRouteId  ))
begin
set @ErrorMessage='Return trip timing is not valid , there is another trip has the same timing with the same route.';
set @IsSuccess=0;
set @ReturnTripId=0
rollback;
return;
end

end


--****************** Insert New Return Trip ...


--delete from Trips 
--where MainTripId=@TripId;

if(@ReturnTripId =0)
begin

insert into Trips(Date,Time,status,	RouteId,Seats,HasBookedSeat,IsVehicleMoving,HasBreak,StationOrderNextToBreak,StationStopMinutes,
BreakMinutes,ArrivedStationOrder,MainTripId)
values( cast(@ReturnTrip_NewDateTime as datetime) ,cast(@ReturnTrip_NewDateTime as time),
case when @VehicleId is not null then 0 else -1 end 

,@ReverseRouteId,@Seats,@ReturnTrip_HasBookedSeats
,0,@ReturnTrip_HasBreak
,@ReturnTrip_StationOrderNextToBreak,@ReturnTrip_StationStopMinutes,@ReturnTrip_BreakMinutes,0,@TripId )

set @ReturnTripId=SCOPE_IDENTITY();

end
else
begin

update Trips 
set Time=cast(@ReturnTrip_NewDateTime as time) ,Date=@ReturnTrip_NewDateTime ,Seats=@Seats, 

status=case when @VehicleId is not null then 0 else -1 end ,

HasBreak=@ReturnTrip_HasBreak,StationOrderNextToBreak=@ReturnTrip_StationOrderNextToBreak,StationStopMinutes=@ReturnTrip_StationStopMinutes,
BreakMinutes=@ReturnTrip_BreakMinutes,
MainTripId=@TripId
where Id=@ReturnTripId;

end


update Trips 
set Time=cast(@MainTrip_NewDateTime as time) ,Date=@MainTrip_NewDateTime ,Seats=@Seats, 

status=case when @VehicleId is not null then 0 else -1 end ,

HasBreak=@MainTrip_HasBreak,StationOrderNextToBreak=@MainTrip_StationOrderNextToBreak,StationStopMinutes=@MainTrip_StationStopMinutes,
BreakMinutes=@MainTrip_BreakMinutes,
ReturnTripId=@ReturnTripId
where Id=@TripId;


--****************************************** Insert new assigmen for Main and Return Trips ...
delete from TripAssignments
where TripId in (@TripId , @ReturnTripId);

with cte
as
(


select t.Id,t.Date,t.Time ,r.FirstStationId, r.LastStationId,r.EstimatedTime from Trips t inner join Routes r on t.RouteId=r.RouteId
where t.Id in (@TripId , @ReturnTripId)


)
insert into TripAssignments(TripId,VehicleId,DepartureStationId,StopedStationId,StartDateAndTime,EndDateAndTime)
select c.Id ,@VehicleId ,c.FirstStationId ,c.LastStationId ,cast(c.Date as datetime)+cast(c.Time as datetime),
DATEADD(MINUTE,
case when c.Id=@TripId then @MainTrip_RealOccupiedDuration 
else @ReturnTrip_RealOccupiedDuration end
,cast(c.Date as datetime)+cast(c.Time as datetime))
from cte c;


----****************************************** Set Trips Pattern ...


exec sp_SetTripPattern
                 @RouteId = @RouteId,
				 @Time = @MianTripTime;

exec sp_SetTripPattern
                 @RouteId = @ReverseRouteId,
				 @Time = @ReturnTripTime;



if( @MainTrip_OldDateTime <> @MainTrip_NewDateTime)
begin

declare @Time1 time=cast(@MainTrip_OldDateTime as time);
exec sp_SetTripPattern
                 @RouteId = @RouteId,
				 @Time = @Time1;
end



if(@ReturnTrip_OldDateTime is not null and @ReturnTrip_OldDateTime <> @ReturnTrip_NewDateTime)
begin
declare @Time2 time = cast (@ReturnTrip_OldDateTime as time);
exec sp_SetTripPattern
                 @RouteId = @ReverseRouteId,
				 @Time = @Time2;
end



set @ErrorMessage='No Error.';
set @IsSuccess=1;
commit;
return;

end try
begin catch



rollback;
return;

end catch




end