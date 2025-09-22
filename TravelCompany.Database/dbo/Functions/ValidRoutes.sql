
create function [dbo].[ValidRoutes] (@StationAId int,@StationBId int)

returns  table 
as 
return 
(
with RouteStations as
(
select rp.RouteId, rp.PointId,s.StationId ,rp.PointOrder
      from 
	  RoutePoints rp 
	  inner join Points p on rp.PointId=p.PointId
	  inner join Stations s on s.StationId=p.StationId
)
,
StationARoutes as
(
select rs.RouteId from RouteStations rs where rs.StationId=@StationAId
)
,
StationsBRoutes as 
(
select rs.RouteId from RouteStations rs where rs.StationId=@StationBId
)
,
PosiableRoutes as
(
select sa.RouteId from StationARoutes sa inner join StationsBRoutes sb on sa.RouteId=sb.RouteId
)
,
ComparisonTable as
(
select rs.RouteId,
@StationAId as StationAId,
(select PointOrder from RouteStations where StationId=@StationAId and RouteId=rs.RouteId) as StationAOrder
,@StationBId as StationBId,
(select PointOrder from RouteStations where StationId=@StationBId and RouteId=rs.RouteId) as StationBOrder

from RouteStations rs where rs.RouteId in (select *from PosiableRoutes)
group by rs.RouteId
)
select ct.RouteId from ComparisonTable ct
where ct.StationAOrder<ct.StationBOrder

)