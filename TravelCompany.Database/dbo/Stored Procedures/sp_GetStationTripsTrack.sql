
	

CREATE procedure [dbo].[sp_GetStationTripsTrack]
                 @StationId int
as
begin


--declare @StationId int=2;

with ValidRoutes
as
(
select distinct rp.RouteId from RoutePoints rp
where rp.StationId=@StationId
)
,TripsWithin3Days
as
(
select *from Trips t 
where t.RouteId in (select *from ValidRoutes) 
and t.Date between DATEADD(day,-2,GETDATE()) and  DATEADD(day,1,GETDATE()) 
-- and t.status <> -1
)
,ActiveTripsWithin3Days_IDs
as
(
select t.Id from TripsWithin3Days t where t.status=1
)
,CompletedTripsWithin3Days_IDs
as
(
select t.Id from TripsWithin3Days t where t.status=2

)
,PedingTripsTrackResult
as
(
select t.Id ,t.Date ,t.status as TripStatus,t.StationStopMinutes
,rp.PointOrder ,s.StationName ,rp.StationId
,r.RouteId,r.RouteName,r.EstimatedDistance

,dbo.GetStationStatus(rp.PointOrder, t.ArrivedStationOrder, t.IsVehicleMoving ) as Status

--,p.TimeValue 
--, sum(p.TimeValue) over (order by rp.PointOrder) as TimeValueAdding
--,DATEADD(MINUTE,sum(p.TimeValue) over (order by rp.PointOrder) ,t.Time) as RawTime

,case
	when t.HasBreak = 0
	    then  DATEADD(MINUTE
		      ,sum(p.TimeValue) over (partition by t.Id  order by rp.PointOrder)+(case when rp.PointOrder-2 <0 then 0 else rp.PointOrder-2 end)*t.StationStopMinutes
              ,(cast(t.Date as datetime)+cast( t.Time as datetime)))
			   --, t.Time)
    else
	    case
	        when rp.PointOrder <t.StationOrderNextToBreak
	            then  DATEADD(MINUTE
		     	,sum(p.TimeValue) over (partition by t.Id  order by rp.PointOrder)+(case when rp.PointOrder-2 <0 then 0 else rp.PointOrder-2 end)*t.StationStopMinutes
                ,(cast(t.Date as datetime)+cast( t.Time as datetime)))
				 --, t.Time )
	    	else 	
			    DATEADD(MINUTE
				,sum(p.TimeValue) over (partition by t.Id  order by rp.PointOrder)+(case when rp.PointOrder-2 <0 then 0 else rp.PointOrder-2 end)*t.StationStopMinutes +t.BreakMinutes
                ,(cast(t.Date as datetime)+cast( t.Time as datetime)))
				 --, t.Time )
	     end
end  as ArrivalDateTime

,lag (s.StationName) over (partition by t.Id order by rp.PointOrder) as PreviousStation
,lead (s.StationName) over (partition by t.Id order by rp.PointOrder) as NexttStation

from TripsWithin3Days t inner join Routes r on r.RouteId=t.RouteId 
                        inner join RoutePoints rp on r.RouteId=rp.RouteId
			            inner join Points p on p.PointId=rp.PointId
		                inner join Stations s on rp.StationId=s.StationId
where t.status <> 1 and t.status <> 2
)
,StationPedingTripsTrack 
as
(
select 

r.Id as TripId , r.PointOrder as StationOrder , r.StationId , r.StationName , r.Status , r.PreviousStation , r.NexttStation ,ArrivalDateTime
,DATEADD(MINUTE,case when r.PointOrder =1 then 0 else r.StationStopMinutes end,r.ArrivalDateTime) as DepartureDateTime
,r.TripStatus,r.RouteId ,r.RouteName,r.EstimatedDistance
,0 as ArrivalLateMinutes , 0 as DepartureLateMinutes
,(select count(*)from Reservations rs where rs.TripId=r.Id and rs.StationAId=@StationId) as StationBoarding
from PedingTripsTrackResult r
where r.StationId=@StationId
)
,StationActiveTripsTrack 
as
(
select 
a.TripId , a.StationOrder , a.StationId , a.StationName , a.Status , a.PreviousStation , a.NexttStation , a.ActualArrivalDateTime as ArrivalDateTime
,a.ActualDepartureDateTime as DepartureDateTime , a.TripStatus ,a.RouteId, a.RouteName , a.EstimatedDistance
,DATEDIFF(minute,a.PlannedArrivalDateTime,a.ActualArrivalDateTime) as ArrivalLateMinutes 
, DATEDIFF(minute,a.PlannedDepartureDateTime,a.ActualDepartureDateTime)  as DepartureLateMinutes
,(select count(*)from Reservations rs where rs.TripId=a.TripId and rs.StationAId=@StationId) as StationBoarding
from ActiveTripTracks a 
where a.TripId in (select *from ActiveTripsWithin3Days_IDs)
and a.StationId=@StationId

)
,StationCompletedTripsTrack 
as
(
select 
a.TripId , a.StationOrder , a.StationId , a.StationName , a.Status , a.PreviousStation , a.NexttStation , a.ActualArrivalDateTime as ArrivalDateTime
,a.ActualDepartureDateTime as DepartureDateTime ,2 as TripStatus ,a.RouteId, a.RouteName , a.EstimatedDistance
,DATEDIFF(minute,a.PlannedArrivalDateTime,a.ActualArrivalDateTime) as ArrivalLateMinutes 
, DATEDIFF(minute,a.PlannedDepartureDateTime,a.ActualDepartureDateTime)  as DepartureLateMinutes
,(select count(*)from Reservations rs where rs.TripId=a.TripId and rs.StationAId=@StationId) as StationBoarding
from CompletedTripTracks a 
where a.TripId in (select *from CompletedTripsWithin3Days_IDs)
and a.StationId=@StationId
)

select *from StationPedingTripsTrack
union all
select *from StationActiveTripsTrack
union all
select *from StationCompletedTripsTrack

end