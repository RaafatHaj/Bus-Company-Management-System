
create function dbo.GetFirstStationNameFromRouteId(@RouteId int)
returns nvarchar(100)
as
begin 

declare @PointId int ;
declare @StationId int ;
declare @StationName nvarchar(100);

select @PointId=rp.PointId from RoutePoints rp
where rp.RouteId=@RouteId and rp.PointOrder=1;

select @StationId=p.StationId from Points p
where p.PointId=@PointId;

select @StationName=s.StationName  from Stations s
where s.StationId=@StationId;

return @StationName;

end