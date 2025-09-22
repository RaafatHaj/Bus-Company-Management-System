

CREATE procedure [dbo].[sp_GetTripTrack]
                 @TripId int
as
begin

-- Table Row :  TripId - StationOrder - StationId - StationName - Status - ArrivalDateTime - DepartureDateTime


declare @TripStatus int ;

select @TripStatus=t.status  from Trips t
where t.Id=@TripId


if(@TripStatus =1)
begin

select a.TripId, a.StationOrder ,a.StationId,a.StationName,a.Status,a.ActualArrivalDateTime as ArrivalDateTime,
a.ActualDepartureDateTime as DepartureDateTime 

from ActiveTripTracks a 
where a.TripId=@TripId
return;
end


if(@TripStatus = 2)
begin

select a.TripId, a.StationOrder ,a.StationId,a.StationName,a.Status,a.ActualArrivalDateTime as ArrivalDateTime,
a.ActualDepartureDateTime as DepartureDateTime 

from CompletedTripTracks a 
where a.TripId=@TripId
return;

end



select a.TripId, a.StationOrder ,a.StationId,a.StationName,a.Status,a.PlannedArrivalDateTime as ArrivalDateTime,
a.PlannedDepartureDateTime as DepartureDateTime 

from dbo.GetTripTrack (@TripId) a
return;


end