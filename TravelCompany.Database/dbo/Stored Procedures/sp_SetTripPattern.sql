

CREATE procedure [dbo].[sp_SetTripPattern]
                 @RouteId int,
				 @Time time
as
begin

declare @PatternStartDate date;
declare @PatternEndDate date;
--declare @TodayDate date GetDate();
select @PatternStartDate = min (t.Date) , @PatternEndDate=max(t.Date) from Trips t 
where t.RouteId =@RouteId and t.Time=@Time and t.Date >= cast (GETDATE() as date);

SET @PatternStartDate = DATEADD(DAY, -DATEPART(WEEKDAY, @PatternStartDate) + 1, @PatternStartDate)
SET @PatternEndDate   = DATEADD(DAY, 7 - DATEPART(WEEKDAY, @PatternEndDate), @PatternEndDate)

delete from TripPatterns	
where RouteId=@RouteId and Time=@Time


insert into TripPatterns(RouteId,Time,PatternType,Percentage,OccupiedWeekDaysCode,StartDate,EndDate,TripsNumber,UnassignedTripsNumber
,OccupiedWeeksNumber,EmptyWeeksNumber)

select @RouteId,@Time,
case
when g.OccupaiedWeekDaysCode=127 then 1
when g.OccupaiedWeekDaysCode is null then 4
else 2
end, g.Percentage,g.OccupaiedWeekDaysCode,g.StartDate,g.EndDate,g.AllTrips,g.UnassignedTrips
,g.OccupiedWeeksNumber,g.EmptyWeeksNumber

From dbo.GetTripPattern(@RouteId,@Time,@PatternStartDate,@PatternEndDate) g;

end