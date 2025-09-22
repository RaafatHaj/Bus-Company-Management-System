

-- Function gives the SeatCode (Bigint) of seat by passing Station1 order , Station2 order and Number of all Stations
-- Check data integrity ==>> if SeatCode and(&) other SeatCodes of recoreds where ScheduledTravelId and SeatNumber are the same 
--                           equils 0  => return true (insert the new Reservation) , else return false (cansel the Reservation)


CREATE function [dbo].[GetSeatCode](@SAOrder int , @SBOrder int , @RouteStationsNumber int)
returns Bigint
as 
begin


declare @SeatCode bigint =0;
declare @CurrentStationOrder int =@SAOrder-1;

while(@CurrentStationOrder<@SBOrder)
begin

set @SeatCode=@SeatCode |1* power(2,@CurrentStationOrder);
-- 1 * power (2, @CurrentStationOrder) equils 1 << @CurrentStationOrder in c# (shift to lift)


set @CurrentStationOrder=@CurrentStationOrder+1;
end

return @SeatCode;


end


-- Testing ...



--declare @testCode int ;

--select @testCode=dbo.GetSeatCode(1,3,4);

--print @testCode;

--select *from Reservations

--select* from(
--select 3 & r.SeatCode as Code from Reservations r where r.ScheduledTravelId=2945 and r.SeatNumber=7)r1
--where r1.Code <>0;

--select *from ScheduledTravels
--select *from ScheduledTravelDetails
---- ScheduledTravelId = 2945
---- SeatNumberes = 14
---- StationsNumber = 4
---- order   station Id
---- 1         1
---- 2         2
---- 3         4
---- 4         3
--insert into Reservations(ScheduledTravelId,StationAId,StationBId,SeatNumber,SeatCode,PersonId,PersonName,PersonPhone)
--values
--	  (2945,1,2,8,3,8,'test8','test8')