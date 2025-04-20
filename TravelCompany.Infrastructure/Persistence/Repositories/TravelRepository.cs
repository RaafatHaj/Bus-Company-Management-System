using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using System.Data;
using TravelCompany.Application.Common.Interfaces.Repositories;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Eums;
using TravelCompany.Domain.Settings;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TravelCompany.Infrastructure.Persistence.Repositories
{


    public class TravelRepository: BaseRepository<Trip>, ITravelRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly string _connectionString;

        public TravelRepository(ApplicationDbContext context,string connectionStrings) : base(context)
        {
            _context = context;
            _connectionString = connectionStrings;
        }

		//public IQueryable<Travel> GetQueryable()
		//{
		//	return base.GetQueryable();
		//}

		public async Task<int> CreateTravelAsync(TravelScheduleDTO schedule)
		{
			
			//  procedure[dbo].[sp_AddNewTravel]

			//  @RouteId int,
			//  @TravelTime time,
			//  @Seats int,
			//  @ReScheule bit,
			//  @ScheduleDuration tinyint,
			//  @ScheduleType tinyint,

			//  @TravelId int output
			//  return 1,0 

			int travelId = -1;

			using (var connection=new SqlConnection(_connectionString))
			{

				using (var command=new SqlCommand("sp_AddNewTravel", connection))
				{

					//// Return parameter
					// SqlParameter returnParam = new SqlParameter();
					// returnParam.Direction = ParameterDirection.ReturnValue;
					// cmd.Parameters.Add(returnParam);


					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@RouteId", schedule.RouteId);
					command.Parameters.AddWithValue("@TravelTime", schedule.TravelTime);
					//command.Parameters.AddWithValue("@Seats", schedule.Seats);
					command.Parameters.AddWithValue("@ReScheule", schedule.ReSchedule);

					if(schedule.ScheduleDuration!=null) command.Parameters.AddWithValue("@ScheduleDuration", (byte)schedule.ScheduleDuration);
					else command.Parameters.AddWithValue("@ScheduleDuration", DBNull.Value);

					command.Parameters.AddWithValue("@ScheduleType", (byte)schedule.SelectedScheduleType);

					command.Parameters.Add(new SqlParameter("@TravelId", SqlDbType.Int) { Direction = ParameterDirection.Output });

					var returnedParam = new SqlParameter();
					returnedParam.Direction = ParameterDirection.ReturnValue;
					command.Parameters.Add(returnedParam);

					try
					{
						 await connection.OpenAsync();

						 await command.ExecuteNonQueryAsync();

						if ((int)returnedParam.Value==1)
						{
							travelId = (int)command.Parameters["@TravelId"].Value;

						}
						


					}
					catch
					{
						return travelId;
					}


				}


			}

			return travelId;
		}

		public async Task<Trip?> GetTravelAsync(TimeSpan travelTime, int routeId)
		{
			

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_FindTravel", connection))
				{

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@TravelTime", travelTime);
					command.Parameters.AddWithValue("@RouteId", routeId);

					command.Parameters.Add(new SqlParameter("@TravelId", SqlDbType.Int) { Direction = ParameterDirection.Output });
					//command.Parameters.Add(new SqlParameter("@Seats", SqlDbType.Int) { Direction = ParameterDirection.Output });
					command.Parameters.Add(new SqlParameter("@ReSchedule", SqlDbType.Bit) { Direction = ParameterDirection.Output });
					command.Parameters.Add(new SqlParameter("@ScheduleDuration", SqlDbType.TinyInt) { Direction = ParameterDirection.Output });
					command.Parameters.Add(new SqlParameter("@ScheduleType", SqlDbType.TinyInt) { Direction = ParameterDirection.Output });
					command.Parameters.Add(new SqlParameter("@ScheduleEndingDate", SqlDbType.Date) { Direction = ParameterDirection.Output });


					//var EffectedRows = new SqlParameter();
					//EffectedRows.Direction = ParameterDirection.ReturnValue;
					//command.Parameters.Add(EffectedRows);


					try
					{
						await connection.OpenAsync();

						await command.ExecuteNonQueryAsync();

						if((int)command.Parameters["@TravelId"].Value >0)
						{
							var travel = new Trip();

							travel.RouteId = routeId;
							travel.TripTime = travelTime;

							travel.TripId = (int)command.Parameters["@TravelId"].Value;
							//travel.Seats = (int)command.Parameters["@Seats"].Value;
							//travel.Reschedule = (bool)command.Parameters["@ReSchedule"].Value;
							//travel.ScheduleDuration = command.Parameters["@ScheduleDuration"].Value != DBNull.Value ?
							//	(ScheduleDuration)Convert.ToByte(command.Parameters["@ScheduleDuration"].Value):null;

							//travel.ScheduleType = (RecurringType)Convert.ToByte(command.Parameters["@ScheduleType"].Value);

							//travel.ScheduleEndingDate = command.Parameters["@ScheduleEndingDate"].Value==DBNull.Value?null:
							//	(DateTime)command.Parameters["@ScheduleEndingDate"].Value;
							
							return travel;

						}

						


					}
					catch
					{
						return null;
					}


				}
			}




			return null;
		}

		public async Task<bool> ScheculeTravelsAsync(TravelScheduleDTO schedule)
		{

			//  procedure[dbo].[sp_ScheduleTravels]

			//  @TravelId int,
			//  @RouteId int ,
			//  @TravelTime time,
			//  @StartingDate date,
			//  @ScheduleDuration tinyint,
			//  @ScheduleType tinyint,
			//  @Seats int,

			//  @Days DaysType readonly ,
			//  @Dates DatesType readonly

			//  retrun EffectedRows


			bool IsScheduled=false;

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_ScheduleTravels", connection))
				{
					// SqlParameter tvpParam = new SqlParameter();
					// tvpParam.ParameterName = "@Route_Details";
					// tvpParam.SqlDbType = SqlDbType.Structured;
					// tvpParam.Value = table;
					// tvpParam.TypeName = "dbo.Route_Details_Type";
					// cmd.Parameters.Add(tvpParam);

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@TravelId", schedule.TravelId);
					command.Parameters.AddWithValue("@RouteId", schedule.RouteId);
					command.Parameters.AddWithValue("@TravelTime", schedule.TravelTime);

					if (schedule.StartingDate!=null) command.Parameters.AddWithValue("@StartingDate", schedule.StartingDate);
					else command.Parameters.AddWithValue("@StartingDate", DBNull.Value);
					if (schedule.ScheduleDuration != null) command.Parameters.AddWithValue("@ScheduleDuration", (byte)schedule.ScheduleDuration);
					else command.Parameters.AddWithValue("@ScheduleDuration", DBNull.Value);

					command.Parameters.AddWithValue("@ScheduleType",(byte) schedule.SelectedScheduleType);
					command.Parameters.AddWithValue("@Seats", schedule.Seats);

					var dates = new SqlParameter();
					dates.ParameterName = "@Dates";
					dates.SqlDbType = SqlDbType.Structured;
					dates.Value = schedule.Dates;
					dates.TypeName = "dbo.DatesType";
					command.Parameters.Add(dates);

					var days = new SqlParameter();
					days.ParameterName = "@Days";
					days.SqlDbType = SqlDbType.Structured;
					days.Value = schedule.Days;
					days.TypeName = "dbo.DaysType";
					command.Parameters.Add(days);



					var EffectedRows = new SqlParameter();
					EffectedRows.Direction = ParameterDirection.ReturnValue;
					command.Parameters.Add(EffectedRows);

					try
					{
						await connection.OpenAsync();

						await command.ExecuteNonQueryAsync();

						IsScheduled=(int)EffectedRows.Value>0;


					}
					catch
					{
						return false;
					}


				}
			}

			return IsScheduled;
		}



	}
}
