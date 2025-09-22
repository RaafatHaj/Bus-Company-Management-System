

CREATE function [dbo].[GetTripTrack] (
                        @TripId int
						
)
returns  table 
as 
return
(

with Trip
as
(
select *from Trips where Id=@TripId
)
,TrackResult
as
(
select t.Id ,t.Date ,t.status as TripStatus,t.StationStopMinutes
,rp.PointOrder ,s.StationName ,rp.StationId
,t.RouteId,r.RouteName,r.EstimatedDistance

,dbo.GetStationStatus(rp.PointOrder, t.ArrivedStationOrder, t.IsVehicleMoving ) as Status
,case
	when t.HasBreak = 0
	    then  DATEADD(MINUTE
		      ,sum(p.TimeValue) over (order by rp.PointOrder)+(case when rp.PointOrder-2 <0 then 0 else rp.PointOrder-2 end)*t.StationStopMinutes
              ,(cast(t.Date as datetime)+cast( t.Time as datetime)))
			   --, t.Time)
    else
	    case
	        when rp.PointOrder <t.StationOrderNextToBreak
	            then  DATEADD(MINUTE
		     	,sum(p.TimeValue) over (order by rp.PointOrder)+(case when rp.PointOrder-2 <0 then 0 else rp.PointOrder-2 end)*t.StationStopMinutes
                ,(cast(t.Date as datetime)+cast( t.Time as datetime)))
				 --, t.Time )
	    	else 	
			    DATEADD(MINUTE
				,sum(p.TimeValue) over (order by rp.PointOrder)+(case when rp.PointOrder-2 <0 then 0 else rp.PointOrder-2 end)*t.StationStopMinutes +t.BreakMinutes
                ,(cast(t.Date as datetime)+cast( t.Time as datetime)))
				 --, t.Time )
	     end
end  as PlannedArrivalDateTime

,lag (s.StationName) over (partition by t.Id order by rp.PointOrder) as PreviousStation
,lead (s.StationName) over (partition by t.Id order by rp.PointOrder) as NexttStation

from Trip t inner join Routes r on r.RouteId=t.RouteId 
                        inner join RoutePoints rp on r.RouteId=rp.RouteId
			            inner join Points p on p.PointId=rp.PointId
		                inner join Stations s on rp.StationId=s.StationId

)

select 

r.Id as TripId , r.PointOrder as StationOrder , r.StationId , r.StationName , r.Status , r.PreviousStation , r.NexttStation ,PlannedArrivalDateTime
,DATEADD(MINUTE,case when r.PointOrder =1 then 0 else r.StationStopMinutes end,r.PlannedArrivalDateTime) as PlannedDepartureDateTime
,r.TripStatus ,r.RouteId ,r.RouteName,r.EstimatedDistance
from TrackResult r

);

