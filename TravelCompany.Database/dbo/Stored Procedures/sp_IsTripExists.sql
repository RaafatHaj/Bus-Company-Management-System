

create procedure sp_IsTripExists
				 @TripTime time,
				 @TripDate datetime,
				 @RouteId int,
				 @IsExists bit output
as
begin


if(exists( select 1 from Trips t
where t.Time=@TripTime and t.Date=@TripDate and t.RouteId=@RouteId))
begin

set @IsExists=1;
return;

end


set @IsExists=0;
return; 


end