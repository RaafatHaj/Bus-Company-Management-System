

CREATE function [dbo].[GenerateTravelStatusCode](@StationOrder int,@IsArrived bit)
returns Bigint
as 
begin

declare @TravelStatusCode bigint =0;
declare @CurrentStationOrder int =0;

if(@IsArrived =1)
begin
while(@CurrentStationOrder<@StationOrder)
begin

set @TravelStatusCode=@TravelStatusCode |1* power(2,@CurrentStationOrder);
-- 1 * power (2, @CurrentStationOrder) equils 1 << @CurrentStationOrder in c# (shift to lift)


set @CurrentStationOrder=@CurrentStationOrder+1;
end
return @TravelStatusCode;
end

else
begin

while(@CurrentStationOrder<@StationOrder-1)
begin

set @TravelStatusCode=@TravelStatusCode |1* power(2,@CurrentStationOrder);
-- 1 * power (2, @CurrentStationOrder) equils 1 << @CurrentStationOrder in c# (shift to lift)


set @CurrentStationOrder=@CurrentStationOrder+1;
end
return @TravelStatusCode;

end
return @TravelStatusCode;

end