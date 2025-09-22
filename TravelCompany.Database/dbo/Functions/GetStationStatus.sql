CREATE	 function [dbo].[GetStationStatus](@StationOrder int , @ArrivedStationOrder int 
                                         , @IsVehicleMoving bit)
returns int
as 
begin

--declare @StationStatusCode bigint=power(2,@StationOrder-1);
if(@StationOrder<@ArrivedStationOrder)
begin
return 2;
end

if(@StationOrder > @ArrivedStationOrder )
begin
return 0;
end

if(@IsVehicleMoving = 1 )
begin
return 2;
end

return 1;

end