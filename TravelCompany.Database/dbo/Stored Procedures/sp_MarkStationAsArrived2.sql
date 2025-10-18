

CREATE procedure [dbo].[sp_MarkStationAsArrived2]
                 @TripId int,
				 @StationId int,
				 @StationOrder int,
				 @IsMarked bit output,
				 @ErrorMessage nvarchar(max) output
as
begin

declare @RealArrivalDateTIme datetime =GETDATE();
declare @ActualArrivalDateTime datetime ;
declare @PreviousStationStatus int;
declare @TripDateTime datetime;
declare @HasBookedSeas bit;
declare @LastStationOrder int;
declare @TripStatus int;
declare @StationStatus int;
declare @StationLateMinutes int;


select @TripDateTime =(cast(t.Date as datetime)+cast(t.Time as datetime)) 
,@HasBookedSeas=t.HasBookedSeat
, @LastStationOrder=r.StationsNumber 
,@TripStatus=t.status
from Trips t inner join Routes r on t.RouteId=r.RouteId
where t.Id=@TripId




if(@TripStatus =2) -- ***// Trip Completed ...
begin
set @ErrorMessage='The trip has already been completed ,and Station has already been Arrived before.';
set @IsMarked=0;
return;
end

if(@TripStatus =-2)-- ***// Trip Canceled ...
begin
set @ErrorMessage='The trip has been Canceled ,can not mark station as Arrived.';
set @IsMarked=0;
return;
end


if(@TripStatus =-1)-- ***// Trip Unassigned ...
begin
set @ErrorMessage='The trip Unassigned yet.';
set @IsMarked=0;
return;
end


if(@TripStatus =0)-- ***// Trip Peding ...
begin


if(@StationOrder =1 )
begin
set @ErrorMessage='This is the first station, and the vehicle has already Existed at station.';
set @IsMarked=0;
return;
end


set @ErrorMessage='The trip has not started yet.';
set @IsMarked=0;
return;
end


----------------------------------------------------------------------------------------------------------------
select @StationStatus=a.Status ,@ActualArrivalDateTime=a.ActualArrivalDateTime
from ActiveTripTracks a
where a.TripId=@TripId and a.StationOrder=@StationOrder

if(@StationStatus = 1 or @StationStatus =2)-- ***// Station Arrived or Moved ...
begin
set @ErrorMessage='The Station has been already Arrived.';
set @IsMarked=0;
return;
end


begin transaction
begin try


select @PreviousStationStatus=a.Status from ActiveTripTracks a
where a.TripId=@TripId and a.StationOrder=@StationOrder-1


if(@PreviousStationStatus <> 2)--***// Pervious Station Not Moved ...
begin
set @ErrorMessage='The previous station has not arrived yet, so you cannot mark the trip as arrived at this station.';
set @IsMarked=0;
rollback;
return;
end



set @StationLateMinutes=DATEDIFF(minute,@ActualArrivalDateTime,@RealArrivalDateTIme)

if(@StationOrder <> @LastStationOrder)--***// Regular Applying ...
begin

--*** // Update Trip ...
update Trips 
set IsVehicleMoving=0,ArrivedStationOrder=@StationOrder 
where Id=@TripId


--*** // Update Station Status / Actual Time ...
update ActiveTripTracks
set Status = 1 , ActualArrivalDateTime=@RealArrivalDateTIme 
,ActualDepartureDateTime=DATEADD(minute,@StationLateMinutes,ActualDepartureDateTime)
where StationOrder=@StationOrder

--*** // Update Upcomming Stations Actual Time ...
update ActiveTripTracks
set ActualArrivalDateTime=DATEADD(minute,@StationLateMinutes,ActualArrivalDateTime) ,
    ActualDepartureDateTime=DATEADD(minute,@StationLateMinutes,ActualDepartureDateTime)
where TripId=@TripId and StationOrder > @StationOrder



select a.*
,DATEDIFF(minute,a.PlannedArrivalDateTime,a.ActualArrivalDateTime) as ArrivalLateMinutes
, DATEDIFF(minute,a.PlannedDepartureDateTime,a.ActualDepartureDateTime)  as DepartureLateMinutes
from ActiveTripTracks a
where a.TripId=@TripId and a.StationId=@StationId;

set @ErrorMessage='No Error.'
set @IsMarked=1;
commit;	
return;
end


--***// Applay + Set Trip as Completed ...


--*** // Update Trip Status / Late Minutes  ...
--declare @TripLateMinutes int;

--select @TripLateMinutes= DATEDIFF(MINUTE,a.PlannedArrivalDateTime,@RealArrivalDateTIme) from ActiveTripTracks a 
--where a.TripId=@TripId and a.StationOrder=@StationOrder

update Trips 
set status=2,IsVehicleMoving=0,ArrivedStationOrder=@StationOrder
where Id=@TripId


--*** // Update Station Status / Time ***
--*** // Move Trip Track to CompletedTripTracks Table ***
--*** // Delete Trip Track from ActiveTripTracks Table ***

update ActiveTripTracks
set Status = 1 , ActualArrivalDateTime=@RealArrivalDateTIme 
,ActualDepartureDateTime=@RealArrivalDateTIme
where StationOrder=@StationOrder

insert into CompletedTripTracks 
select *from ActiveTripTracks where TripId=@TripId;

delete from ActiveTripTracks
where TripId=@TripId;


select c.TripId ,c.StationOrder,c.StationId,c.StationName,c.Status,c.PreviousStation,c.NexttStation,c.PlannedArrivalDateTime
,c.PlannedDepartureDateTime,c.ActualArrivalDateTime,c.ActualDepartureDateTime , 2 as TripStatus,
c.RouteId,c.RouteName,c.EstimatedDistance
,DATEDIFF(minute,c.PlannedArrivalDateTime,c.ActualArrivalDateTime) as ArrivalLateMinutes
, DATEDIFF(minute,c.PlannedDepartureDateTime,c.ActualDepartureDateTime)  as DepartureLateMinutes
from CompletedTripTracks c
where c.TripId=@TripId and c.StationId=@StationId;

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