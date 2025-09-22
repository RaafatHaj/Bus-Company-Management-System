




CREATE procedure [dbo].[sp_EditRecurringRecord]
			     @TripId int,
				 @StartingDate date,
				 @EndingDate date,
				 @RecurringType int,
				 @Days DaysType readonly,
				 @RecordStartingDate date,
				 @RecordEndingDate date,
				 @RecordRecurringType int,
				 @RecordRecurringId int,
			--	 @IsRecoredEdited bit output,
				 @ExtndedStartingDate date  output,
				 @ExtndedEndingDate date  output
as
begin
--set @IsRecoredEdited=0;
--declare @RecordDays table (day int);

--set @ExtndedStartingDate = @StartingDate;
--set @ExtndedEndingDate = @EndingDate;

--if(@StartingDate <= @RecordStartingDate  and @EndingDate >= @RecordEndingDate) -- New (Contain or Equil) Existed
--begin


--delete from Days
--where RecurringId=@RecordRecurringId;

--delete from Recurrings
--where RecurringId=@RecordRecurringId;

----set @IsRecoredEdited=0;


--return;
--end

--if(@StartingDate <= @RecordStartingDate and @EndingDate < @RecordEndingDate and @EndingDate >@RecordStartingDate) -- New (Cut from Left) Existed
--begin 

--if(@RecurringType = @RecordRecurringType)
--begin


--insert into @RecordDays
--select d.day from Days d 
--where d.RecurringId=@RecordRecurringId

--if(not exists(select 1 from @Days where Day not in(select *from @RecordDays)) and
--not exists(select 1 from @RecordDays where day not in(select *from @Days)))
--begin

--set @ExtndedEndingDate = @RecordEndingDate;
----set @IsRecoredEdited=1;
--delete from Days
--where RecurringId=@RecordRecurringId;

--delete from Recurrings
--where RecurringId=@RecordRecurringId;

--return;

--end
--end

--update Recurrings
--set FirstTripDate=@EndingDate
--where RecurringId=@RecordRecurringId;

----set @IsRecoredEdited=0;

--return;
--end

--if(@StartingDate > @RecordStartingDate and @StartingDate < @RecordEndingDate and @EndingDate >= @RecordEndingDate) -- New (Cut from Right) Existed
--begin



--if(@RecurringType = @RecordRecurringType)
--begin


--insert into @RecordDays
--select d.day from Days d 
--where d.RecurringId=@RecordRecurringId

--if(not exists(select 1 from @Days where Day not in(select *from @RecordDays)) and
--not exists(select 1 from @RecordDays where day not in(select *from @Days)))
--begin

--set @ExtndedStartingDate =@RecordStartingDate;
----set @IsRecoredEdited=1;
--delete from Days
--where RecurringId=@RecordRecurringId;

--delete from Recurrings
--where RecurringId=@RecordRecurringId;


--return;

--end
--end



--update Recurrings
--set LastTripDate=@StartingDate
--where RecurringId=@RecordRecurringId;

----set @IsRecoredEdited=0;

--return;
--end

--if(@StartingDate > @RecordStartingDate and @EndingDate < @RecordEndingDate) -- Existed (Contain) New 
--begin


--if(@RecurringType = @RecordRecurringType)
--begin


--insert into @RecordDays
--select d.day from Days d 
--where d.RecurringId=@RecordRecurringId

--if(not exists(select 1 from @Days where Day not in(select *from @RecordDays)) and
--not exists(select 1 from @RecordDays where day not in(select *from @Days)))
--begin

--set @ExtndedStartingDate =@RecordStartingDate;
--set @ExtndedEndingDate =@RecordEndingDate;

--delete from Days
--where RecurringId=@RecordRecurringId;

--delete from Recurrings
--where RecurringId=@RecordRecurringId;


----set @IsRecoredEdited=1;

--return;
--end
--end


--declare @NewRecurringId int;
--declare @RecurringDays table(day int);

--insert into @RecurringDays 
--select d.day from Days d
--where d.RecurringId=@RecordRecurringId;

---- *********** Remove Exsited 

--delete from Days
--where RecurringId=@RecordRecurringId;

--delete from Recurrings
--where RecurringId=@RecordRecurringId;





---- *********** Add Left Part of Exsited 

--insert into Recurrings(TripId,RecurringType,FirstTripDate,LastTripDate,IsActive,Reschedule)
--values(@TripId,@RecordRecurringType,@RecordStartingDate,@StartingDate,1,0);
--set @NewRecurringId=SCOPE_IDENTITY();

--insert into Days (RecurringId,day)
--select @NewRecurringId,d.day  from @RecurringDays d



---- *********** Add Right Part of Exsited 

--insert into Recurrings(TripId,RecurringType,FirstTripDate,LastTripDate,IsActive,Reschedule)
--values(@TripId,@RecordRecurringType,@EndingDate,@RecordEndingDate,1,0);
--set @NewRecurringId=SCOPE_IDENTITY();

--insert into Days (RecurringId,day)
--select @NewRecurringId,d.day  from @RecurringDays d


----set @IsRecoredEdited=1;

return;
--end

end