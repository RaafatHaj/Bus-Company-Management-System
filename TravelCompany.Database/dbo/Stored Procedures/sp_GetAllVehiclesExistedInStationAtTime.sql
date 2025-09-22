
CREATE procedure sp_GetAllVehiclesExistedInStationAtTime
					 @TripDateAndTime datetime ,
                     @StationId int
as
begin



-->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
-- \\\ Get all departure station vehicles ...

with StationVehicles 
as
(
select v.VehicleId from Vehicles v
where v.StationId=@StationId
)

-->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
-- \\\ Get all vehicles stoped in the station last 10 days ...

,TemprarlyStopedVehicles
as
(
select distinct ta.VehicleId from TripAssignments ta 
where ta.EndDateAndTime<=@TripDateAndTime and ta.EndDateAndTime >DATEADD(day,-10,@TripDateAndTime) and ta.StopedStationId=@StationId
)

-->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
-- \\\ Combine all potential vehicles existed in the station at that time ...

,AllExistedVehicles
as
(
select *from StationVehicles
union
select *from TemprarlyStopedVehicles
)

-->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
-- \\\ Exclude active vehicles (in an active trip) and get available vehicles ...

,AvailableVehicles
as
(

select sv.VehicleId from AllExistedVehicles sv
where (not exists
(select 1 from TripAssignments ta
where ta.EndDateAndTime>@TripDateAndTime and ta.StartDateAndTime<=@TripDateAndTime  and  ta.VehicleId=sv.VehicleId
))

)

-->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
-- \\\ Get the station of each available vehicle at @TripTime ...

,AvailableVehiclesLastStopedStations1 AS (
    select av.VehicleId,ta.StopedStationId,
           ROW_NUMBER() over (partition by ta.VehicleId order by ta.EndDateAndTime desc) as RowNumber
    from TripAssignments ta
    inner join AvailableVehicles av on ta.VehicleId = av.VehicleId
    where ta.EndDateAndTime <= @TripDateAndTime and ta.EndDateAndTime >DATEADD(day,-10,@TripDateAndTime)
),
AvailableVehiclesLastStopedStations12
as
(
select vs.VehicleId , vs.StopedStationId from AvailableVehiclesLastStopedStations1
vs where vs.RowNumber=1
)

-->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
-- \\\ Get vehicles that existed in the station at @TripTime ( FInal result ) ...


, FinalResult
as(

select av.VehicleId,isnull( s.StopedStationId,@StationId) as StationId
from 
AvailableVehicles av left join AvailableVehiclesLastStopedStations12 s
on av.VehicleId=s.VehicleId)

select *from FinalResult fr
where fr.StationId=@StationId



end