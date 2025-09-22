


CREATE procedure [dbo].[sp_FindSuitableTrip]
          @StationAId int,
		  @StationBId int,
		  @Date date
as
begin 

declare @StationBName nvarchar(200);

select @StationBName=s.StationName from Stations s
where s.StationId=@StationBId;

with ValidRoutes
as
(
select vr.RouteId from dbo.ValidRoutes(@StationAId,@StationBId) vr
)
,TripsAtDate
as
(
select *from Trips t 
where 
t.RouteId in (select *from ValidRoutes) 
and t.Date =@Date
)
,ActiveTripsAtDate_IDs
as
(
select t.Id from TripsAtDate t where t.status=1
)
,PedingTripsTrack
as
(
select t.Id ,t.Date ,t.status as TripStatus,t.StationStopMinutes
,rp.PointOrder ,s.StationName ,rp.StationId,
r.RouteName,r.EstimatedDistance

,dbo.GetStationStatus(rp.PointOrder, t.ArrivedStationOrder, t.IsVehicleMoving ) as Status

,case
	when t.HasBreak = 0 or rp.PointOrder <t.StationOrderNextToBreak
	    then  DATEADD(MINUTE
		      ,sum(p.TimeValue) over (partition by t.Id order by rp.PointOrder)+(case when rp.PointOrder-2 <0 then 0 else rp.PointOrder-2 end)*t.StationStopMinutes
              ,(cast(t.Date as datetime)+cast( t.Time as datetime)))
			
    else

	 DATEADD(MINUTE
				,sum(p.TimeValue) over (partition by t.Id order by rp.PointOrder)+(case when rp.PointOrder-2 <0 then 0 else rp.PointOrder-2 end)*t.StationStopMinutes +t.BreakMinutes
                ,(cast(t.Date as datetime)+cast( t.Time as datetime)))
			

end  as ArrivalDateTime

from TripsAtDate t inner join Routes r on r.RouteId=t.RouteId 
                        inner join RoutePoints rp on r.RouteId=rp.RouteId
			            inner join Points p on p.PointId=rp.PointId
		                inner join Stations s on rp.StationId=s.StationId
where t.status =0
)
--select *from PedingTripsTrackResult

,PedingTripsResult
as
(
select 

r.Id as TripId , r.PointOrder as StationOrder , r.StationId , r.StationName , r.Status  ,ArrivalDateTime
,DATEADD(MINUTE,case when r.PointOrder =1 then 0 else r.StationStopMinutes end,r.ArrivalDateTime) as DepartureDateTime
,(select r1.ArrivalDateTime from PedingTripsTrack r1 where r1.Id=r.Id and r1.StationId=@StationBId) as StationBArrivalTime
,r.TripStatus ,r.RouteName,r.EstimatedDistance ,@StationBName as StationBName  , @StationBId as StationBId
from PedingTripsTrack r
--group by r.Id
where r.StationId=@StationAId
)
--select *from StationPedingTripsTrack
,ActiveTripsResult
as
(
select 
a.TripId , a.StationOrder , a.StationId , a.StationName , a.Status , a.ActualArrivalDateTime as ArrivalDateTime
,a.ActualDepartureDateTime as DepartureDateTime 
,(select a1.PlannedArrivalDateTime from ActiveTripTracks a1  where a1.TripId=a.TripId and a1.StationId=@StationBId) as StationBArrivalTime
, a.TripStatus , a.RouteName , a.EstimatedDistance ,@StationBName as StationBName , @StationBId as StationBId
from ActiveTripTracks a 
where a.TripId in (select *from ActiveTripsAtDate_IDs)
and a.StationId=@StationAId and a.Status=0

)
,Result 
as
(
select *from PedingTripsResult
union all
select *from ActiveTripsResult
)
select *from Result
order by DepartureDateTime



end

