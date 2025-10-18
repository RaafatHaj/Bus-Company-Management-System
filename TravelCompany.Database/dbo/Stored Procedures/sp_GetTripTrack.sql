

CREATE procedure [dbo].[sp_GetTripTrack]
                 @TripId int
as
begin

-- Table Row :  TripId - StationOrder - StationId - StationName - Status - ArrivalDateTime - DepartureDateTime


declare @TripStatus int ;

select @TripStatus=t.status  from Trips t
where t.Id=@TripId


if(@TripStatus =1) -- *** Trip Active ...
begin

select a.TripId, a.StationOrder ,a.StationId,a.StationName,a.Status,a.ActualArrivalDateTime as ArrivalDateTime,
a.ActualDepartureDateTime as DepartureDateTime ,
DATEDIFF(minute,a.PlannedArrivalDateTime,a.ActualArrivalDateTime) as ArrivalLateMinutes,
DATEDIFF(minute,a.PlannedDepartureDateTime,a.ActualDepartureDateTime) as DepartureLateMinutes

from ActiveTripTracks a 
where a.TripId=@TripId
return;
end


if(@TripStatus = 2) -- *** Trip Compelte ...
begin

select a.TripId, a.StationOrder ,a.StationId,a.StationName,a.Status,a.ActualArrivalDateTime as ArrivalDateTime,
a.ActualDepartureDateTime as DepartureDateTime ,
DATEDIFF(minute,a.PlannedArrivalDateTime,a.ActualArrivalDateTime) as ArrivalLateMinutes,
DATEDIFF(minute,a.PlannedDepartureDateTime,a.ActualDepartureDateTime) as DepartureLateMinutes

from CompletedTripTracks a 
where a.TripId=@TripId
return;

end



select a.TripId, a.StationOrder ,a.StationId,a.StationName,a.Status,a.PlannedArrivalDateTime as ArrivalDateTime,
a.PlannedDepartureDateTime as DepartureDateTime ,
0 as ArrivalLateMinutes,
0 as DepartureLateMinutes

from dbo.GetTripTrack (@TripId) a
return;


end