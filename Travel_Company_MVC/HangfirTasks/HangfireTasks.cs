using Hangfire.Storage;
using Hangfire;
using TravelCompany.Application.Services.ScheduledTravels;
using TravelCompany.Domain.Entities;

namespace Travel_Company_MVC.HangfirTasks
{
	public class HangfireTasks
	{
		//private readonly string _connectinString;
		//private readonly IScheduledTravelService _scheduledTravels;

		//public HangfireTasks(string connectinString, IScheduledTravelService scheduledTravels)
		//{
		//	_connectinString = connectinString;
		//	_scheduledTravels = scheduledTravels;
		//}



		//public void ScheduleStartedTravelsJobs()
		//{

		//	// create Times table in db
		//	// retrive times from it by the function  GetScheduledTimesFromDatabase()
		//	// remove HangfireTasks to application layer ...


		//	List<string> scheduledTimes = GetScheduledTimesFromDatabase(); 

		//	// Step 1: Get existing jobs from Hangfire
		//	var existingJobs = Hangfire.JobStorage.Current.GetConnection().GetRecurringJobs()
		//		.Select(job => job.Id)
		//		.ToList();

		//	// Step 2: Add missing jobs
		//	foreach (var time in scheduledTimes)
		//	{
		//		string jobId = $"job_{time}";

		//		if (!existingJobs.Contains(jobId))
		//		{
		//			RecurringJob.AddOrUpdate(jobId,
		//				() => _scheduledTravels.SetStartedTravels(),
		//				ConvertToCron(time));

		//			Console.WriteLine($"Scheduled job at {time}");
		//		}
		//	}

		//	// Step 3: Remove jobs that no longer exist in database
		//	foreach (var jobId in existingJobs)
		//	{
		//		string time = jobId.Replace("job_", ""); // Extract time from job ID
		//		if (!scheduledTimes.Contains(time))
		//		{
		//			RecurringJob.RemoveIfExists(jobId);
		//			Console.WriteLine($"Removed job at {time}");
		//		}
		//	}
		//}


		//private string ConvertToCron(string time)
		//{
		//	// Convert "2:00" -> "0 2 * * *" (daily at 2 AM)
		//	string[] parts = time.Split(':');
		//	return $"{parts[1]} {parts[0]} * * *";
		//}

	}
}
