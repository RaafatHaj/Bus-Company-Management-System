create function [dbo].[GetStationStatusCode](@StationOrder int)
returns Bigint
as 
begin

return 0 |1* power(2,@StationOrder-1);

end