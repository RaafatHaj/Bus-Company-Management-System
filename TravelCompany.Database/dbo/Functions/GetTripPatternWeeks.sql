
CREATE function [dbo].[GetTripPatternWeeks]
(
		@RouteId int,
		@Time time ,
		@StartDate date ,
		@EndDate date 
)
returns @ResultTable table(WeekOrder int , StartDate date ,EndDate date , OccupaiedWeekDaysCode int , UnassignedTrips int , AllTrips int 
                          )
as
begin

-- ///////////////////////////////////////////////// Set date of sunday in first week and date of saturday of last week

--declare @FirstSundayDate date ;
--declare @LastSaturdayDate date ;


--select  @FirstSundayDate =min( t.Date) , @LastSaturdayDate=max(t.Date) from Trips t 
--where t.RouteId=@RouteId and t.Time=@Time and t.Date between @StartDate and @EndDate;

SET @StartDate = DATEADD(DAY, -DATEPART(WEEKDAY, @StartDate) + 1, @StartDate);
SET @EndDate   = DATEADD(DAY, 7 - DATEPART(WEEKDAY, @EndDate), @EndDate);



--while( DATENAME(WEEKDAY, @StartDate) <>'Sunday')
--begin

--set @StartDate=DATEADD(day,-1, @StartDate)
--end

--while( DATENAME(WEEKDAY, @EndDate) <>'Saturday')
--begin

--set @EndDate=DATEADD(day,1, @EndDate)
--end;

-- ///////////////////////////////////////////////// Main function Logic ...


--******* 1.All dates from sunday in first week to saturday in last week ..

-- Not : in sql server 2022 there is a builtin function GENERATE_SERIES() to generate series of numbers ...


--with DatesRange
--as
--(
--select  @StartDate as Date 
--union all
--select
--DATEADD(day,1,r.Date) from DatesRange r
--where r.Date<@EndDate
--)


--WITH Numbers AS (
--    SELECT TOP (DATEDIFF(DAY, @StartDate, @EndDate) + 1)
--           ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) - 1 AS n
--    FROM master.dbo.spt_values
--    WHERE type = 'P'  -- filters to a subset of rows
--)
--, DatesRange AS (
--    SELECT DATEADD(DAY, n, @StartDate) AS Date
--    FROM Numbers
--)

WITH DatesRange AS (
    SELECT DATEADD(DAY, n-1, @StartDate) AS Date
    FROM dbo.Numbers
    WHERE n <= DATEDIFF(DAY, @StartDate, @EndDate)+1
)

--******* 2.All dates with the order of the week that belong to ..

,DatesRangeWithWeekOrder
as
(

select 
r.Date
, DATEDIFF(DAY, @StartDate, r.Date) / 7 + 1 as WeekOrder
--,cast ((ROW_NUMBER() over (order by r.Date)-1)/7+1 as int) as WeekOrder
from DatesRange r
)

--******* 3.Get day code in each week from Binary logic * sun ==>> 1 <> 1
--                                                        mon ==>> 2 <> 10
--		                                                  tue ==>> 4 <> 100
--		                                                  wed ==>> 8 <> 1000

,DatesRangeWithWeekDayPower
as
(
select r.WeekOrder , r.Date , 
(ROW_NUMBER() over (partition by r.WeekOrder order by r.Date))-1 as Power from DatesRangeWithWeekOrder r 

)
,
DateRangeWithWeekDaysCode
as
(
select r.WeekOrder,r.Date , power(2,r.Power) as WeekDayCode from DatesRangeWithWeekDayPower r
)

--******* 4.Get Scheduled Trips dates with its status ..

,ScheduledTripsDates
as
(
	select t.Date ,t.status from Trips t 
	where t.RouteId=@RouteId and t.Time=@Time and t.Date between @StartDate and @EndDate
)

--******* 5.Get week day code for Scheduled Trips..

,ScheduledTripsWithWeekDaysCode
as
(
select 
r.WeekOrder,r.Date,
case

when s.Date is null then 0
else r.WeekDayCode

end

as WeekDayCode
,s.status

from DateRangeWithWeekDaysCode r left join ScheduledTripsDates s on r.Date=s.Date
)

--******* 5.Final Resule..

,FinalResult
as
(
--select *from FinalResult 
select 
s.WeekOrder,
 min(s.Date) as StartDate, max(s.Date) as EndDate , sum(s.WeekDayCode) as OccupaiedWeekDaysCode 
,(select count(*) from ScheduledTripsWithWeekDaysCode s1 where s1.WeekOrder=s.WeekOrder and s1.status=-1) as UnassignedTrips
,(select count(*) from ScheduledTripsWithWeekDaysCode s1 where s1.WeekOrder=s.WeekOrder and s1.status is not null) as AllTrips

from ScheduledTripsWithWeekDaysCode s
group by s.WeekOrder
--having  sum(s.WeekDayCode) <>0
)
insert into @ResultTable
select *from FinalResult 
option (MAXRECURSION 0); 






return;

end	