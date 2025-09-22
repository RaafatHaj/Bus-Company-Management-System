


CREATE procedure [dbo].[sp_GetStationPassengersBoarding]
                  @TripId int,
				  @StationId int
as
begin


select *from Reservations r
where r.TripId=@TripId and r.StationAId=@StationId

--declare @StationOrder int;

--select @StationOrder=rp.PointOrder
--from 
--Trips t inner join Routes r on t.RouteId = r.RouteId
--inner join RoutePoints rp on r.RouteId =rp.RouteId
--where t.Id=@TripId and rp.StationId=@StationId

--declare @StationCode int;
--declare @PreviousStatonsCode int=0;

--set @StationCode = POWER(2,@StationOrder-1);


--select @PreviousStatonsCode = @PreviousStatonsCode | power(2,n.n-1) from Numbers n 
--where n.n < @StationOrder

--print @StationCode
--print @PreviousStatonsCode

--select *from Reservations r 
--where r.TripId =@TripId 
--and r.SeatCode & @StationCode = @StationCode 
--and r.SeatCode & @PreviousStatonsCode=0;

end
