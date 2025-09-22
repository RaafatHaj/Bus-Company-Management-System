


CREATE function [dbo].[GetTripPattern]
(
		@RouteId int,
		@Time time ,
		@StartDate date ,
		@EndDate date 
)
returns @ResultTable table(Percentage decimal(5,1) , UnassignedTrips int ,AllTrips int , OccupaiedWeekDaysCode int , StartDate date,
EndDate date ,OccupiedWeeksNumber int , EmptyWeeksNumber int)
as
begin

-- ///////////////////////////////////////////////// Set date of sunday in first week and date of saturday of last week




with GroupedWeeks0
as
(

select  w.OccupaiedWeekDaysCode , count(*) as WeeksNumber , sum(w.UnassignedTrips) as UnassignedTrips,
sum(w.AllTrips)  as AllTrips

from 

dbo.GetTripPatternWeeks(@RouteId,@Time,@StartDate,@EndDate) w
group by w.OccupaiedWeekDaysCode
--where r1.OccupaiedWeekDaysCode <>0
--having w.OccupaiedWeekDaysCode <>0

)
,EmptyWeeksNumber
as
(
select g.WeeksNumber as EmptyWeeksNumber from GroupedWeeks0 g where g.OccupaiedWeekDaysCode=0
)
,TotalWeeksNumber
as
(
select sum(g.WeeksNumber) as TotalWeeksNumber from GroupedWeeks0 g 
)
,GroupedWeeks
as
(
select *from GroupedWeeks0 g where g.OccupaiedWeekDaysCode <>0
)
,MostReapetedPattern
as
(	
select max(g.WeeksNumber) as MostReapeted from GroupedWeeks g

)
,RecurringOfMaxPatterns
as
(
select count(*) as Recurring  from GroupedWeeks g
where g.WeeksNumber=(select m.MostReapeted from MostReapetedPattern  m)
)
,Pattern
as
(
select max(g.WeeksNumber) as MaxWeekNumber ,cast ((cast(max(g.WeeksNumber) as decimal(10,1))/cast (sum( g.WeeksNumber) as decimal(10,1)))*100 as decimal(5,1)) as Percentage 
,sum( g.UnassignedTrips )as UnassignedTrips ,sum( g.AllTrips)  as AllTrips
from GroupedWeeks g 
)

insert into @ResultTable(Percentage,UnassignedTrips,AllTrips,OccupaiedWeekDaysCode , StartDate,EndDate ,OccupiedWeeksNumber, EmptyWeeksNumber)
select top 1 p.Percentage,p .UnassignedTrips ,p.AllTrips ,
case 
when (select r.Recurring from RecurringOfMaxPatterns r)=1 then g.OccupaiedWeekDaysCode
else null

end
,@StartDate,@EndDate,

case 
    when (select e.EmptyWeeksNumber from EmptyWeeksNumber e) is null then (select t.TotalWeeksNumber from TotalWeeksNumber t)
	else (select t.TotalWeeksNumber from TotalWeeksNumber t)-(select e.EmptyWeeksNumber from EmptyWeeksNumber e)
end,

case 
    when (select e.EmptyWeeksNumber from EmptyWeeksNumber e) is null then 0
	else (select e.EmptyWeeksNumber from EmptyWeeksNumber e) 
end
from

Pattern p inner join GroupedWeeks g on p.MaxWeekNumber=g.WeeksNumber
option (MAXRECURSION 0); 






return;

end	