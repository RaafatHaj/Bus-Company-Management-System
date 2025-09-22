

--select *from dbo.[GetAvaliableSeats2](10618,2,3)


create function [dbo].[GetAvaliableSeats] (
                      @TripId int,
					  @StationAOrder int,
					  @StationBOrder int
						
)

returns @AvaliableSeats table (SeatNumber int , Status int)
as 
begin

declare @VehicleSeatesNumber int;
declare @SeatCode bigint;

set @SeatCode= dbo.GetSeatCode2( @StationAOrder, @StationBOrder );

select @VehicleSeatesNumber=t.Seats from trips t
where t.Id=@TripId;

with 
VehicleSeates 
as 
(

select n as SeateNumber
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

select vs.SeateNumber , -1 as Status from VehicleSeates vs
where not exists
(select 1 from TripReservations r
where vs.SeateNumber=r.SeatNumber)

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
insert into @AvaliableSeats
select *from AvaliableSeats

return;
end