

CREATE procedure [dbo].[sp_BookSeat]
				 @TripId int,
				 @SeatCode bigint,
				 @StationAId int,
				 @StationBId int,
				 @PersonPhone nvarchar(50),
				 @PersonEmail nvarchar(50),
				 @UserId nvarchar(450),
				 @TripDepartureDateTime datetime,
				 @IsBooked bit output,
				 @BookedSeatsInfo BookedSeatType readonly

as
begin

declare @ReservationsIDs table ( ReservationId int);

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; 
BEGIN TRANSACTION;
begin try

--select *from Reservations

with 
TripReservations
as
(
select *from Reservations r where r.TripId=@TripId
)
insert into Reservations (TripId,StationAId,StationBId,SeatNumber,SeatCode,PersonId,PersonName,PersonPhone,PersonEmail,PersonGender,CreatedById , CreatedOn ,TripDepartureDateTime)
output inserted.Id into @ReservationsIDs
select 
@TripId , @StationAId , @StationBId , b.SeatNumber , @SeatCode , b.PersonId , b.PersonName , @PersonPhone , @PersonEmail , b.PersonGender,@UserId , GETDATE() , @TripDepartureDateTime
from @BookedSeatsInfo b
where not exists
(
select 1 from TripReservations r where r.SeatNumber=b.SeatNumber and r.SeatCode & @SeatCode <>0
)

select *from @ReservationsIDs


set @IsBooked=1;
COMMIT; 
end try
begin catch
set @IsBooked=0;
ROLLBACK; 
end catch

end