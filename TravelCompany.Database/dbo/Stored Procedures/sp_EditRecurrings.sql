

CREATE procedure [dbo].[sp_EditRecurrings]
                 @TripId int,
				 @StartingDate date,
				 @EndingDate date,
				 @RecurringType int,
				 @Reschedule bit,
				 @Days DaysType readonly 
as
begin

--begin transaction
--begin try


----******************** Edit Existed Recurrings Records 

--declare @TempRecurrings table (RecurringId int,RecurringType int,StartingDate date , EndingDate date );

--insert into @TempRecurrings (RecurringId,RecurringType,StartingDate,EndingDate)
--select r.RecurringId,r.RecurringType,r.FirstTripDate,r.LastTripDate from Recurrings r
--where r.TripId=@TripId


--declare @RecordRecurringId int;
--declare @RecordRecurringType int;
--declare @RecordStartingDate date;
--declare @RecordEndingDate date;
----declare @IsUpdated bit=0;
----declare @ExStartingDate date=@StartingDate;
----declare @ExEndingDate date=@EndingDate;

--while(exists (select 1 from @TempRecurrings))
--begin

--select top 1 @RecordRecurringId=t.RecurringId ,
--             @RecordRecurringType=t.RecurringType,
--			 @RecordStartingDate=t.StartingDate,
--			 @RecordEndingDate=t.EndingDate
--from @TempRecurrings t;


--exec sp_EditRecurringRecord
--                 @TripId = @TripId,
--				 @StartingDate = @StartingDate,
--				 @EndingDate = @EndingDate,
--				 @RecurringType = @RecurringType,
--				 @Days = @Days,
--				 @RecordStartingDate = @RecordStartingDate,
--				 @RecordEndingDate = @RecordEndingDate,
--				 @RecordRecurringType = @RecordRecurringType,
--				 @RecordRecurringId = @RecordRecurringId,
--			--	 @IsRecoredEdited = @IsUpdated output,
--				 @ExtndedStartingDate = @StartingDate  output,
--				 @ExtndedEndingDate = @EndingDate  output;



--delete from @TempRecurrings 
--where RecurringId=@RecordRecurringId;

------set @StartingDate=@ExStartingDate;
------set @EndingDate=@ExEndingDate;

----if(@IsUpdated=1)
----begin

----delete from @TempRecurrings ;

----insert into @TempRecurrings (RecurringId,RecurringType,StartingDate,EndingDate)
----select r.RecurringId,r.RecurringType,r.FirstTripDate,r.LastTripDate from Recurrings r
----where r.TripId=@TripId

----end
----else
----begin

----delete from @TempRecurrings 
----where RecurringId=@RecordRecurringId;

----end;

--end




----******************** Add New Recurring 

--declare @NewRecurringId int;

--insert into Recurrings(TripId,RecurringType,FirstTripDate,LastTripDate,IsActive,Reschedule)
--values(@TripId,@RecurringType,@StartingDate,@EndingDate,1,0);
--set @NewRecurringId=SCOPE_IDENTITY();

--insert into Days (RecurringId,day)
--select @NewRecurringId,d.Day  from @Days d

---- ****************** Here will be set reschedule ...

--commit;
--end try
--begin catch
--rollback;
--end catch

return;
end