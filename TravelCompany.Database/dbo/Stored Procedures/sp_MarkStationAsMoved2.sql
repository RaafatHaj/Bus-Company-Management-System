

CREATE procedure [dbo].[sp_MarkStationAsMoved2]
                 @TripId int,
				 @StationId int,
				 @StationOrder int,
				 @IsMarked bit output,
				 @ErrorMessage nvarchar(max) output
as
begin



--1. Trip Status : Completed => Cancel 
--2. Trip Status : Unassigned => Cancel
--3. Trip Status : Active 
--	1. Station Status : Moved => Cancel
--	2. Station Status : Pending => Cancel
--	3. Station Status : Arrived => Apply 
--4. Trip Status : Pending => 
--	1. Station Order == 1 => Start Trip
--	2. Station Order != 1  => Cancel

declare @RealDepartureDateTime datetime=GETDATE();

declare @TripDateTime datetime;
declare @HasBookedSeas bit;
declare @LastStationOrder int;
declare @TripStatus int;
declare @CurrentStationStatus int;
declare @StationStatus int;
declare @StationLateMinutes int;


select @TripDateTime =(cast(t.Date as datetime)+cast(t.Time as datetime)) 
,@HasBookedSeas=t.HasBookedSeat
, @LastStationOrder=r.StationsNumber 
,@TripStatus=t.status
from Trips t inner join Routes r on t.RouteId=r.RouteId
where t.Id=@TripId

if(@TripStatus =2)
begin
set @ErrorMessage='The trip has already been completed , Can not be moved further.';
set @IsMarked=0;
return;
end

if(@TripStatus =-2)
begin
set @ErrorMessage='The trip has been Canceled , con not be moved.';
set @IsMarked=0;
return;
end


if(@TripStatus =-1)
begin
set @ErrorMessage='The trip Unassigned yet , can not be start.';
set @IsMarked=0;
return;
end

begin transaction
begin try


if(@TripStatus =1)
begin

select @StationStatus=a.Status from ActiveTripTracks a
where a.TripId=@TripId and a.StationId=@StationId

--	1. Station Status : Moved => Cancel
--	2. Station Status : Pending => Cancel
--	3. Station Status : Arrived => Apply 

--	1. Station Status : Moved => Cancel


if(@StationStatus =2)
begin
set @ErrorMessage='The trip has already moved form this station.';
set @IsMarked=0;
rollback;
return;
end


--	2. Station Status : Pending => Cancel
if(@StationStatus =0)
begin
set @ErrorMessage='The trip has not arrived at the station yet, so you cannot mark it as departed.';
set @IsMarked=0;
rollback;
return;
end


--	3. Station Status : Arrived => Apply 
if(@StationStatus =1)
begin


--******** Calculate Station Late Minutes ...

declare @ActualDepartureDateTime datetime;


select  @ActualDepartureDateTime = a.ActualDepartureDateTime 
from ActiveTripTracks a 
where a.TripId=@TripId and a.StationOrder=@StationOrder

set @StationLateMinutes=DATEDIFF(minute,@ActualDepartureDateTime,@RealDepartureDateTime)



--********** 2. Update the track if every thing ok ...

update Trips 
set IsVehicleMoving=1,ArrivedStationOrder=@StationOrder 
where Id=@TripId

--*** Update Row of Station in ActiveTripTrack table ...
update ActiveTripTracks
set Status = 2 , ActualDepartureDateTime=@RealDepartureDateTime
where StationOrder=@StationOrder


--*** Update Stations Rows after the current station in ActiveTripTrack table ...
update ActiveTripTracks
set ActualArrivalDateTime=DATEADD(minute,@StationLateMinutes,ActualArrivalDateTime) ,
    ActualDepartureDateTime=DATEADD(minute,@StationLateMinutes,ActualDepartureDateTime) 
where TripId=@TripId and StationOrder > @StationOrder


select  a.*
,DATEDIFF(minute,a.PlannedArrivalDateTime,a.ActualArrivalDateTime) as ArrivalLateMinutes
, DATEDIFF(minute,a.PlannedDepartureDateTime,a.ActualDepartureDateTime)  as DepartureLateMinutes
from ActiveTripTracks a
where a.TripId=@TripId and a.StationId=@StationId;

set @ErrorMessage='No Error.';
set @IsMarked=1;
commit;
return;
end


end

if(@TripStatus=0) --***// Trip Peding ...
begin



if(@StationOrder <> 1 )
begin
set @ErrorMessage='The trip has not Departed yet, so you cannot mark the station as Moved.';
set @IsMarked=0;
rollback;
return;
end


--******************************** Check if the trip try to dparture Earliaer or Late


--************* Try to daparture earlier ...

if(@RealDepartureDateTime <@TripDateTime)
begin

if(@HasBookedSeas =1 )
begin
set @ErrorMessage='You must modify the trip’s departure time and notify passengers if you want to depart earlier than the planned time.';
set @IsMarked=0;
rollback;
return;
end
else
begin
set @ErrorMessage='You must modify the trip’s departure time if you want to depart earlier than the planned time.';
set @IsMarked=0;
rollback;
return;
end

end

--************* Try to daparture late ...

if(@RealDepartureDateTime >DATEADD(HOUR,2,@TripDateTime))
begin
set @ErrorMessage='It is more than two hours past the planned departure time; you must modify the trip’s departure time before starting it.';
set @IsMarked=0;
rollback;
return;
end

set @StationLateMinutes=DATEDIFF(minute,@TripDateTime,@RealDepartureDateTime);

update Trips 
set status=1,IsVehicleMoving=1,ArrivedStationOrder=1
where Id=@TripId



insert into ActiveTripTracks
select 
r.TripId,r.StationOrder,r.StationId,r.StationName,r.Status,r.PreviousStation,r.NexttStation,r.PlannedArrivalDateTime,r.PlannedDepartureDateTime
,dateadd(minute , @StationLateMinutes ,r.PlannedArrivalDateTime)
,dateadd(minute , @StationLateMinutes ,r.PlannedDepartureDateTime)
,r.TripStatus ,r.RouteId,r.RouteName,r.EstimatedDistance
from dbo.GetTripTrack(@TripId) r


select  a.*
,DATEDIFF(minute,a.PlannedArrivalDateTime,a.ActualArrivalDateTime) as ArrivalLateMinutes
, DATEDIFF(minute,a.PlannedDepartureDateTime,a.ActualDepartureDateTime)  as DepartureLateMinutes
from ActiveTripTracks a
where a.TripId=@TripId and a.StationId=@StationId;


set @ErrorMessage='No Error.'
set @IsMarked=1;
commit;
return;
end


set @ErrorMessage='No Error.'
set @IsMarked=1;
commit;			
end try
begin catch
set @ErrorMessage='Error has to do with data base.';
set @IsMarked=0;
rollback;
end catch

end