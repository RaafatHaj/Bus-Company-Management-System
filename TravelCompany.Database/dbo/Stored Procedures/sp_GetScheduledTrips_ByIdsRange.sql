


CREATE procedure [dbo].[sp_GetScheduledTrips_ByIdsRange]
				 @Ids IdsType readonly
as
begin

with cte
as
(
select 
t.* , (select count(*) from Reservations r where r.TripId=t.Id) as BookingsCount
From Trips t 
where exists (select 1 from @Ids d where d.Id=t.Id)


)
,cte2
as
(
select c.* ,r.EstimatedDistance,r.EstimatedTime,r.FirstStationId ,r.RouteName from cte c 
        inner join Routes r on c.RouteId=r.RouteId
)
,cte3
as
(
select c.* , ta.VehicleId from cte2 c 
      
		left join TripAssignments ta on c.Id=ta.TripId
)
select c.* ,v.VehicleNumber , v.Type from cte3 c 
      
		left join Vehicles v  on c.VehicleId=v.VehicleId
        
order by c.Date , c.Time;





end