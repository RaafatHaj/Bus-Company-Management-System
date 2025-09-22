

CREATE function GetAvailableSeats(@StationAAvailableSeats int , @StationBAvailablleSeats int)
returns int
as
begin

declare @AvailableSeats int;

if(@StationAAvailableSeats=@StationBAvailablleSeats)
begin 
set @AvailableSeats= @StationBAvailablleSeats;
end
else 
begin 



   if(@StationAAvailableSeats<@StationBAvailablleSeats)
   begin 
   set @AvailableSeats= @StationAAvailableSeats;
   end

   if(@StationAAvailableSeats>@StationBAvailablleSeats)
   begin 
   set @AvailableSeats= @StationBAvailablleSeats;
   end


end


return @AvailableSeats;
end