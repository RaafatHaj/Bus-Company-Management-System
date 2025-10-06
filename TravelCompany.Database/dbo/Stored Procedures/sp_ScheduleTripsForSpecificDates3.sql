

CREATE procedure [dbo].[sp_ScheduleTripsForSpecificDates3]
				 @RouteId int,
				 @Time time,
				 --@ReverseTripTime time,
				 --@Seats int,
				 @HasBreak bit,
				 @StationOrderNextToBreak int =null,
				 @BreakMinutes int =null,
				 @StationStopMinutes int,
				 @Dates DatesType readonly
as
begin

begin transaction
begin try

--declare @InsertedTrips table (Date date ,Time time,TripId int);
--declare @MinutesBetween int
--declare @ReversRouteId int;

--select  @ReversRouteId=r.ReverseRouteId from Routes r where r.RouteId=@RouteId;


--****************************************** Insert New Trips / ( Added )

with 
ExistedTripsDates
as
(

select t.Date
from 
Trips t

where t.RouteId=@RouteId and t.Time =@Time
and t.Date in (select *from @Dates)


)
insert into Trips (Date,Time,status,RouteId,HasBookedSeat,IsVehicleMoving,HasBreak,StationOrderNextToBreak,StationStopMinutes,
BreakMinutes,ArrivedStationOrder)
--output inserted.Date,inserted.Time,inserted.Id into @InsertedTrips
select 
d.Date,@Time,-1,@RouteId,0,0,@HasBreak,@StationOrderNextToBreak,@StationStopMinutes,@BreakMinutes,0
from @Dates d 
where d.Date not in (select *from ExistedTripsDates)
--option (MAXRECURSION 0); 


--****************************************** Set Trips Pattern ..

exec sp_SetTripPattern
                 @RouteId =@RouteId,
				 @Time =@Time;

--****************************************** Insert Reverse Trips 

--select @ReversTripTime=t.TripTime from Trips t where t.TripId=@ReverseTripId
--set @MinutesBetween=
--CASE 
--        WHEN @ReverseTripTime >= @Time 
--            THEN DATEDIFF(MINUTE, @Time, @ReverseTripTime)
--        ELSE 
--            DATEDIFF(MINUTE, @Time, '24:00:00') + DATEDIFF(MINUTE, '00:00:00', @ReverseTripTime)
--END ;

--with
--ExistedReversTripsDates
--as
--(

--select t.Date From Trips t
--where t.RouteId = @ReversRouteId and t.Time=@ReverseTripTime and
--t.Date in (
--        case
--         WHEN @ReverseTripTime < @Time THEN (select DATEADD(day, 1, d.Date) from @Dates d )
--         else (select *from @Dates)
--        end
--          )

--)
--insert into Trips (Date,Time,status,RouteId,Seats,HasBookedSeat,StatusCode,MainTripId)
--select 
--    CASE 
--        WHEN @ReverseTripTime < @Time THEN DATEADD(day, 1, i.Date)
--        ELSE i.Date
--    END
--,@ReverseTripTime,-1,@ReversRouteId,@Seats,0,0,i.TripId
--from @InsertedTrips i 
--where
--    CASE 
--        WHEN @ReverseTripTime < @Time THEN DATEADD(day, 1, i.Date)
--        ELSE i.Date
--    END not in (select * from ExistedReversTripsDates);

--where  CAST(DATEADD(MINUTE, @MinutesBetween, CAST(i.Date as datetime)+CAST(@Time as datetime)) as date) not in (select *from ExistedReversTripsDates);


--************************************************ Result


with RouteTrips
as
(

select * from Trips t 
where t.RouteId=@RouteId and t.Time=@Time and   t.Date in (select *from @Dates)

)
,ReverseTrips
as(

select 
t.*

from Trips t  inner join RouteTrips c on t.MainTripId=c.Id or t.ReturnTripId=c.Id
)
,AllTrips
as
(
select *from RouteTrips
union all 
select *from ReverseTrips
)
,cte
as
(
select c.* ,r.EstimatedDistance,r.EstimatedTime,r.FirstStationId ,r.RouteName from AllTrips c 
        inner join Routes r on c.RouteId=r.RouteId
)
,cte1
as
(
select c.* , ta.VehicleId from cte c 
      
		left join TripAssignments ta on c.Id=ta.TripId
)
select c.* ,v.VehicleNumber , v.Type from cte1 c 
      
		left join Vehicles v  on c.VehicleId=v.VehicleId
        
order by c.Date , c.Time;


--with cte
--as
--(

--select * from Trips t 
--where t.RouteId=@RouteId and t.Time=@Time and t.Date in (select *from @Dates)

--union all

--select 
--t.*

--from Trips t  inner join cte c on t.MainTripId=c.Id or t.ReturnTripId=c.Id


--)
--,cte2
--as
--(
--select c.*,r.EstimatedDistance ,r.EstimatedTime,r.FirstStationId , r.RouteName from cte c 
--        inner join Routes r on c.RouteId=r.RouteId
--)
--,cte3
--as
--(
--select c.* , ta.VehicleId from cte2 c 
      
--		left join TripAssignments ta on c.Id=ta.TripId
--)
--select c.* ,v.VehicleNumber , v.Type from cte3 c 
      
--		left join Vehicles v  on c.VehicleId=v.VehicleId
        
--order by c.Date , c.Time;


commit;		
end try

begin catch
   --DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
   -- DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
   -- DECLARE @ErrorState INT = ERROR_STATE();
    
   -- -- Log the error before rolling back
   -- PRINT 'Error occurred: ' + @ErrorMessage;
   -- PRINT 'Error severity: ' + CAST(@ErrorSeverity AS NVARCHAR(10));
   -- PRINT 'Error state: ' + CAST(@ErrorState AS NVARCHAR(10));


rollback;
end catch





end