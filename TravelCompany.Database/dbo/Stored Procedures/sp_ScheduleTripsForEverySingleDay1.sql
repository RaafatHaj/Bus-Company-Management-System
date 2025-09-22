

CREATE procedure [dbo].[sp_ScheduleTripsForEverySingleDay1]
				 @RouteId int,
				 @Time time,
				 --@ReverseTripTime time,
				 --@Seats int,
				 @StartDate date,
				 @EndDate date = null,
				 @Duration int = 3,
				 @IsEditMode bit =0,
				 @HasBreak bit,
				 @StationOrderNextToBreak int =null,
				 @BreakMinutes int =null,
				 @StationStopMinutes int
as
begin

begin transaction
begin try

--declare @InsertedTrips table (Date date ,Time time,TripId int);
--declare @MinutesBetween int
--declare @ReversRouteId int;

--select  @ReversRouteId=r.ReverseRouteId from Routes r where r.RouteId=@RouteId;

if(@EndDate is null)
begin
set @EndDate =EOMONTH( DATEADD(Month,@Duration,@StartDate));
end;



--****************************************** Delete Exsting Patteren if (EditMode is True)

if(@IsEditMode=1)
begin

delete from trips
where RouteId=@RouteId and Time=@Time and (Date between @StartDate and @EndDate) and HasBookedSeat=0 and ReturnTripId is null
and MainTripId is null;


declare  @DeletedReturnTrips table (TripId int);

with cte
as
(
select t.Id from Trips t
where t.RouteId=@RouteId and t.Time=@Time and (t.Date between @StartDate and @EndDate) and t.HasBookedSeat=0 and MainTripId is null
)

delete from Trips
output deleted.MainTripId into @DeletedReturnTrips
where MainTripId in (select *from cte) and HasBookedSeat=0;

delete from Trips
where Id in (select *from @DeletedReturnTrips)

end;


--****************************************** Insert New Trips / ( Added )

--with Dates as 
--(
--	select @StartDate as Date
--	union all
--	select DATEADD(day,1,d.Date) from Dates d where d.Date<@EndDate

--)
WITH Dates AS (
    SELECT DATEADD(DAY, n-1, @StartDate) AS Date
    FROM dbo.Numbers
    WHERE n <= DATEDIFF(DAY, @StartDate, @EndDate)+1
)
,
ExistedTripsDates
as
(

select t.Date
from 
Trips t

where t.RouteId=@RouteId and t.Time =@Time
and t.Date between @StartDate and @EndDate 


)
insert into Trips (Date,Time,status,RouteId,HasBookedSeat,IsVehicleMoving,HasBreak,StationOrderNextToBreak,StationStopMinutes,
BreakMinutes,ArrivedStationOrder)
--output inserted.Date,inserted.Time,inserted.Id into @InsertedTrips
select 
d.Date,@Time,-1,@RouteId,0,0,@HasBreak,@StationOrderNextToBreak,@StationStopMinutes,@BreakMinutes,0
from Dates d 
--where d.Date not in (select *from ExistedTripsDates)
where (not exists ( select 1 from ExistedTripsDates e where e.Date=d.Date))
--option (MAXRECURSION 0); 


--****************************************** Set Trips Pattern ..
exec sp_SetTripPattern
                 @RouteId =@RouteId,
				 @Time =@Time;


--****************************************** Insert Reverse Trips ..

--with
--ExistedReversTripsDates
--as
--(

--select t.Date From Trips t
--where t.RouteId = @ReversRouteId and t.Time=@ReverseTripTime and
--t.Date between @StartDate and @EndDate

--)
--insert into Trips (Date,Time,status,RouteId,Seats,HasBookedSeat,StatusCode,MainTripId)
--select 
--    CASE 
--        WHEN @ReverseTripTime < @Time THEN DATEADD(day, 1, i.Date)
--        ELSE i.Date
--    END
--,@ReverseTripTime,-1,@ReversRouteId,@Seats,0,0,i.TripId
--from @InsertedTrips i 
----where
----    CASE 
----        WHEN @ReverseTripTime < @Time THEN DATEADD(day, 1, i.Date)
----        ELSE i.Date
----    END not in (select * from ExistedReversTripsDates)
--where (not exists (select 1 from ExistedReversTripsDates e 
--where 
--case 
--  WHEN @ReverseTripTime < @Time THEN DATEADD(day, 1, i.Date)
--  ELSE i.Date
--end = e.Date
--))


--************************************************ Result

with cte
as
(

select * from Trips t 
where t.RouteId=@RouteId and t.Time=@Time and t.Date between @StartDate and @EndDate

--union all

--select 
--t.*

--from Trips t  inner join cte c on t.MainTripId=c.Id


)
,cte2
as
(
select c.* ,r.EstimatedDistance,r.EstimatedTime,r.FirstStationId,r.RouteName from cte c 
        inner join Routes r on c.RouteId=r.RouteId
)
,cte3
as
(
select c.* , ta.VehicleId from cte2 c 
      
		left join TripAssignments ta on c.Id=ta.TripId
)
select c.* ,v.VehicleNumber , v.Type from cte3 c 
      
		left join Vehicles v  on c.VehicleId=v.VehicleId
        
order by c.Date , c.Time;


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