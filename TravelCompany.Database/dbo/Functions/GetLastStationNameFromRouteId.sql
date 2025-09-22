
create function dbo.GetLastStationNameFromRouteId(@RouteId int)
returns nvarchar(100)
as
begin 

declare @PointOrder int ;
declare @PointId int ;
declare @StationId int ;
declare @StationName nvarchar(100);

select top(1) @PointOrder=  PointOrder from RoutePoints where RouteId=@RouteId order by PointOrder desc

select @PointId=rp.PointId from RoutePoints rp
where rp.RouteId=@RouteId and rp.PointOrder=@PointOrder;

select @StationId=p.StationId from Points p
where p.PointId=@PointId;

select @StationName=s.StationName  from Stations s
where s.StationId=@StationId;

return @StationName;

end
