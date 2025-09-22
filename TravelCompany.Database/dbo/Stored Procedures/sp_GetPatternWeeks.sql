

CREATE procedure [dbo].[sp_GetPatternWeeks]
              @RouteId int,
			  @Time time,
			  @StartDate date ,
			  @EndDate date
as
begin

select *From dbo.GetTripPatternWeeks(@RouteId,@Time,@StartDate,@EndDate) g
where g.OccupaiedWeekDaysCode <>0


end