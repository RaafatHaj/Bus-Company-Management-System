


CREATE procedure [dbo].[sp_ChangeRecurringType]
                 @TripId int,
				 @StartingDate date,
				-- @EndingDate date,
				 @RecurringType int,
				 @Days DaysType readonly 
as
begin

begin transaction
begin try

--declare @OldRecurringTyps int;
--declare @OldDays DaysType;
--declare @TripTime time;
--declare @StartingDateAndTime datetime;
--declare @EndingDateAndTime datetime;
--declare @Dates table(date datetime);

--select @TripTime = t.TripTime from Trips t where t.TripId=@TripId;
--select @OldRecurringTyps = r.RecurringType from Recurrings r where r.TripId=@TripId-- and r.IsActive=1;

--set @StartingDateAndTime=cast (@StartingDate as datetime)+cast (@TripTime as datetime);
----set @EndingDateAndTime=cast (@EndingDate as datetime)+cast (@TripTime as datetime);

--with dates as 
--(
--	select @StartingDateAndTime as Date
--	union all
--	select DATEADD(day,1,d.Date) from Dates d where d.Date<@EndingDateAndTime

--)
--insert into @Dates(date)
--select d.Date from dates d
--option (MAXRECURSION 0); 
----*********************************************************

--if(@OldRecurringTyps=1)-- Every single day
--begin

--if(@RecurringType=2)
--begin



--delete from ScheduledTrips
--where TripId=@TripId and DateAndTime>=@StartingDateAndTime
--and HasBookedSeat=0;



--with RemainTripsDates
--as
--(
--select st.DateAndTime from ScheduledTrips st where st.TripId=@TripId and st.DateAndTime>=@StartingDateAndTime
--)
--insert into ScheduledTrips (DateAndTime,status,TripId,Seats,HasBookedSeat,StatusCode,IsIrregular)
--select d.date,0,@TripId,10,0,0,0 from @Dates d
--where  DATEPART(WEEKDAY, d.date ) in (select *from @Days)
--and d.date not in (select *from RemainTripsDates)
--option (MAXRECURSION 0); 


--update ScheduledTrips 
--set IsIrregular=1
--where DateAndTime>=@StartingDateAndTime and TripId=@TripId and  DATEPART(WEEKDAY,DateAndTime) not in (select *from @Days); 

--commit;		
--return;

--end
--if(@RecurringType=3)
--begin

--delete from ScheduledTrips
--where TripId=@TripId and DateAndTime>=@StartingDateAndTime
--and HasBookedSeat=0;


--with RemainTripsDates
--as
--(
--select st.DateAndTime from ScheduledTrips st where st.TripId=@TripId and st.DateAndTime>=@StartingDateAndTime
--)
--insert into ScheduledTrips (DateAndTime,status,TripId,Seats,HasBookedSeat,StatusCode,IsIrregular)
--select d.date,0,@TripId,10,0,0,0 from @Dates d
--where day(d.date) in (select *from @Days)
--and d.date not in (select *from RemainTripsDates)
--option (MAXRECURSION 0); 


--update ScheduledTrips 
--set IsIrregular=1
--where DateAndTime>=@StartingDateAndTime and TripId=@TripId and day(DateAndTime) not in (select *from @Days); 

--commit;		
--return;
--end

--end

----********************************************************

--if(@OldRecurringTyps=2)-- Days in weak
--begin

--if(@RecurringType=1)
--begin

--with ExistedTripDates
--as
--(
--select st.DateAndTime from ScheduledTrips st where st.TripId=@TripId and st.DateAndTime>=@StartingDateAndTime
--)
--insert into ScheduledTrips(DateAndTime,status,TripId,Seats,HasBookedSeat,StatusCode,IsIrregular)
--select d.date,0,@TripId,10,0,0,0 from @Dates d 
--where  d.Date  not in (select *from ExistedTripDates) 
--option (MAXRECURSION 0); 

--commit;		
--return;

--end
--if(@RecurringType=3)
--begin

--delete from ScheduledTrips
--where TripId=@TripId and DateAndTime>=@StartingDateAndTime
--and HasBookedSeat=0;


--with RemainTripsDates
--as
--(
--select st.DateAndTime from ScheduledTrips st where st.TripId=@TripId and st.DateAndTime>=@StartingDateAndTime
--)
--insert into ScheduledTrips (DateAndTime,status,TripId,Seats,HasBookedSeat,StatusCode,IsIrregular)
--select d.date,0,@TripId,10,0,0,0 from @Dates d
--where day(d.date) in (select *from @Days)
--and d.date not in (select *from RemainTripsDates)
--option (MAXRECURSION 0); 


--update ScheduledTrips 
--set IsIrregular=1
--where DateAndTime>=@StartingDateAndTime and TripId=@TripId and day(DateAndTime) not in (select *from @Days); 



--commit;		
--return;

--end

--if(@RecurringType=2)
--begin




--delete from ScheduledTrips
--where TripId=@TripId and DateAndTime>=@StartingDateAndTime
--and HasBookedSeat=0;


--with RemainTripsDates
--as
--(
--select st.DateAndTime from ScheduledTrips st where st.TripId=@TripId and st.DateAndTime>=@StartingDateAndTime
--)
--insert into ScheduledTrips (DateAndTime,status,TripId,Seats,HasBookedSeat,StatusCode,IsIrregular)
--select d.date,0,@TripId,10,0,0,0 from @Dates d
--where  DATEPART(WEEKDAY, d.date ) in (select *from @Days)
--and d.date not in (select *from RemainTripsDates)
--option (MAXRECURSION 0); 


--update ScheduledTrips 
--set IsIrregular=1
--where DateAndTime>=@StartingDateAndTime and TripId=@TripId and  DATEPART(WEEKDAY,DateAndTime) not in (select *from @Days); 



--commit;		
--return;



--end

--end

----*********************************************************

--if(@OldRecurringTyps=3)-- Days in month
--begin

--if(@RecurringType=1)
--begin


--with ExistedTripDates
--as
--(
--select st.DateAndTime from ScheduledTrips st where st.TripId=@TripId and st.DateAndTime>=@StartingDateAndTime
--)
--insert into ScheduledTrips(DateAndTime,status,TripId,Seats,HasBookedSeat,StatusCode,IsIrregular)
--select d.date,0,@TripId,10,0,0,0 from @Dates d 
--where  d.Date  not in (select *from ExistedTripDates) 
--option (MAXRECURSION 0); 

--commit;		
--return;

--end
--if(@RecurringType=2)
--begin



--delete from ScheduledTrips
--where TripId=@TripId and DateAndTime>=@StartingDateAndTime
--and HasBookedSeat=0;


--with RemainTripsDates
--as
--(
--select st.DateAndTime from ScheduledTrips st where st.TripId=@TripId and st.DateAndTime>=@StartingDateAndTime
--)
--insert into ScheduledTrips (DateAndTime,status,TripId,Seats,HasBookedSeat,StatusCode,IsIrregular)
--select d.date,0,@TripId,10,0,0,0 from @Dates d
--where  DATEPART(WEEKDAY, d.date ) in (select *from @Days)
--and d.date not in (select *from RemainTripsDates)
--option (MAXRECURSION 0); 


--update ScheduledTrips 
--set IsIrregular=1
--where DateAndTime>=@StartingDateAndTime and TripId=@TripId and  DATEPART(WEEKDAY,DateAndTime) not in (select *from @Days); 



--commit;		
--return;

--end

--if(@RecurringType=3)
--begin


--delete from ScheduledTrips
--where TripId=@TripId and DateAndTime>=@StartingDateAndTime
--and HasBookedSeat=0;


--with RemainTripsDates
--as
--(
--select st.DateAndTime from ScheduledTrips st where st.TripId=@TripId and st.DateAndTime>=@StartingDateAndTime
--)
--insert into ScheduledTrips (DateAndTime,status,TripId,Seats,HasBookedSeat,StatusCode,IsIrregular)
--select d.date,0,@TripId,10,0,0,0 from @Dates d
--where day(d.date) in (select *from @Days)
--and d.date not in (select *from RemainTripsDates)
--option (MAXRECURSION 0); 


--update ScheduledTrips 
--set IsIrregular=1
--where DateAndTime>=@StartingDateAndTime and TripId=@TripId and day(DateAndTime) not in (select *from @Days); 




commit;		
return;

--end


--end

--commit;
end try
begin catch

rollback;
end catch

end
