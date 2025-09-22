

CREATE procedure [dbo].[sp_GetAvaliableSeats]
                      @TripId int,
					  @StationAId int,
					  @StationBId int,
					  @SeatCode bigint output
as
begin

declare @StationAOrder int;
declare @StationBOrder int
declare @RouteId int;

declare @VehicleSeatesNumber int;

select @RouteId =t.RouteId ,@VehicleSeatesNumber=t.Seats from Trips t
where t.Id=@TripId

select @StationAOrder=rp.PointOrder from RoutePoints rp where rp.RouteId=@RouteId and rp.StationId=@StationAId
select @StationBOrder=rp.PointOrder from RoutePoints rp where rp.RouteId=@RouteId and rp.StationId=@StationBId


--declare @VehicleSeatesNumber int;
--declare @SeatCode bigint;

set @SeatCode= dbo.GetSeatCode2( @StationAOrder, @StationBOrder );

--select @VehicleSeatesNumber=t.Seats from trips t
--where t.Id=@TripId;

with 
VehicleSeates 
as 
(

select n as SeatNumber
from dbo.Numbers
where n <= @VehicleSeatesNumber

)
,TripReservations
as
(

select *from Reservations 
where TripId=@TripId

)
,UnbookedSeats
as
(

select vs.SeatNumber , -1 as Status from VehicleSeates vs
where not exists
(select 1 from TripReservations r
where vs.SeatNumber=r.SeatNumber)

)
,BookedSeats
as
(

select tr.SeatNumber , tr.PersonGender as Status from TripReservations tr
where tr.SeatCode & @SeatCode <> 0

)
,EmptySeats 
as
(

select distinct tr.SeatNumber , -1 as Status from TripReservations tr
where not exists
(
select 1 from BookedSeats bs
where bs.SeatNumber = tr.SeatNumber
)
)
,AvaliableSeats
as
(
select *from UnbookedSeats 
union all
select *from BookedSeats
union all
select *from EmptySeats
)
select *from AvaliableSeats
order by SeatNumber


--select *from GetAvaliableSeats (
--                         @TripId ,
--					     @StationAOrder ,
--					     @StationBOrder  )order by SeatNumber 


end