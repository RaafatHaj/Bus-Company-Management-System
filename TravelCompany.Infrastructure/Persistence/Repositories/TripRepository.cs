using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces.Repositories;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Infrastructure.Persistence.Repositories
{
    public class TripRepository :BaseRepository<Trip> , ITripRepository
    {

        private readonly ApplicationDbContext _context;

        private readonly string _connectionString;

        public TripRepository(ApplicationDbContext context, string connectionString) : base(context)
        {
            _connectionString = connectionString;
            _context = context;
        }


		public async Task<IEnumerable<Trip>> GetAllTripsAsync()
		{
			var trips = new List<Trip>();
			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("select *from Trips\r\norder by Date,Time", connection))
				{

					try
					{
						await connection.OpenAsync();
						using (var reader = await command.ExecuteReaderAsync())
						{
							while (reader.Read())
							{
								trips.Add(new()
								{
									Id = reader.GetInt32(reader.GetOrdinal("Id")),
							    	Date = reader.GetDateTime(reader.GetOrdinal("Date")),
							    	Time = reader.GetTimeSpan(reader.GetOrdinal("Time")),
							    	status = (TripStatus)reader.GetInt32(reader.GetOrdinal("status")),
							    	RouteId = reader.GetInt32(reader.GetOrdinal("RouteId")),
							    	Seats = reader.GetInt32(reader.GetOrdinal("Seats")),
							    	HasBookedSeat = reader.GetBoolean(reader.GetOrdinal("HasBookedSeat")),
							    	StatusCode = reader.GetInt64(reader.GetOrdinal("StatusCode")),
									MainTripId = reader.IsDBNull(reader.GetOrdinal("MainTripId")) ? null : reader.GetInt32(reader.GetOrdinal("MainTripId"))

								});
							}

						}


					}
					catch
					{
						return trips;
					}


				}


			}


			return trips;

		}


		public async Task<IEnumerable<Trip>> ScheduleTripsForDaysInWeekAsync(ScheduleDTO dto)
		{
			var trips = new List<Trip>();

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_ScheduleTripsForDaysInWeek", connection))
				{

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@RouteId", dto.RouteId);
					command.Parameters.AddWithValue("@Time", dto.DepartureTime);
					command.Parameters.AddWithValue("@ReverseTripTime", dto.ReturnTime);
					command.Parameters.AddWithValue("@Seats", dto.Seats);
					command.Parameters.AddWithValue("@StartDate", dto.StartDate);
					command.Parameters.AddWithValue("@EndDate", dto.EndDate);
					command.Parameters.AddWithValue("@Duration", dto.ScheduleDuration);


					var weekDays = new SqlParameter
					{
						ParameterName = "@Days",
						SqlDbType = SqlDbType.Structured,
						TypeName = "dbo.DaysType",
						Value = dto.WeekDays
					};

					command.Parameters.Add(weekDays);

					try
					{
						await connection.OpenAsync();
						using (var reader = await command.ExecuteReaderAsync())
						{
							while (reader.Read())
							{
								trips.Add(new()
								{
									Id = reader.GetInt32(reader.GetOrdinal("Id")),
									Date = reader.GetDateTime(reader.GetOrdinal("Date")),
									Time = reader.GetTimeSpan(reader.GetOrdinal("Time")),
									status = (TripStatus)reader.GetInt32(reader.GetOrdinal("status")),
									RouteId = reader.GetInt32(reader.GetOrdinal("RouteId")),
									Seats = reader.GetInt32(reader.GetOrdinal("Seats")),
									HasBookedSeat = reader.GetBoolean(reader.GetOrdinal("HasBookedSeat")),
									StatusCode = reader.GetInt64(reader.GetOrdinal("StatusCode")),
									MainTripId = reader.IsDBNull(reader.GetOrdinal("MainTripId")) ? null : reader.GetInt32(reader.GetOrdinal("MainTripId"))

								});
							}

						}


					}
					catch
					{
						return trips;
					}


				}


			}


			return trips;

		}


		public async Task<IEnumerable<ScheduledTripBaseDTO>> ScheduleTripsForEverySingleDayAsync(ScheduleDTO dto)
		{
			var trips = new List<ScheduledTripBaseDTO>();

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_ScheduleTripsForEverySingleDay", connection))
				{

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@RouteId", dto.RouteId);
					command.Parameters.AddWithValue("@Time", dto.DepartureTime);
					command.Parameters.AddWithValue("@ReverseTripTime", dto.ReturnTime);
					command.Parameters.AddWithValue("@Seats", dto.Seats);
					command.Parameters.AddWithValue("@StartDate", dto.StartDate);
					command.Parameters.AddWithValue("@EndDate", dto.EndDate);
					command.Parameters.AddWithValue("@Duration", dto.ScheduleDuration);


					//var weekDays = new SqlParameter
					//{
					//	ParameterName = "@Days",
					//	SqlDbType = SqlDbType.Structured,
					//	TypeName = "dbo.DaysType",
					//	Value = dto.WeekDays
					//};

					//command.Parameters.Add(weekDays);

					try
					{
						await connection.OpenAsync();
						using (var reader = await command.ExecuteReaderAsync())
						{
							while (reader.Read())
							{
								trips.Add(new()
								{
									TripId = reader.GetInt32(reader.GetOrdinal("Id")),
									Date = reader.GetDateTime(reader.GetOrdinal("Date")),
									Time = reader.GetTimeSpan(reader.GetOrdinal("Time")),
									Status = (TripStatus)reader.GetInt32(reader.GetOrdinal("status")),
									RouteId = reader.GetInt32(reader.GetOrdinal("RouteId")),
									Seats = reader.GetInt32(reader.GetOrdinal("Seats")),
									HasBookedSeat = reader.GetBoolean(reader.GetOrdinal("HasBookedSeat")),
									StatusCode = reader.GetInt64(reader.GetOrdinal("StatusCode")),
									MainTripId = reader.IsDBNull(reader.GetOrdinal("MainTripId")) ? null : reader.GetInt32(reader.GetOrdinal("MainTripId")),
                                    DepartureStationId= reader.GetInt32(reader.GetOrdinal("FirstStationId")),
									TripTimeSpanInMInits= reader.GetInt32(reader.GetOrdinal("EstimatedTime")),
									VehicleId = reader.IsDBNull(reader.GetOrdinal("VehicleId")) ? null : reader.GetInt32(reader.GetOrdinal("VehicleId")),
									VehicleNUmber = reader.IsDBNull(reader.GetOrdinal("VehicleNumber")) ? null : reader.GetString(reader.GetOrdinal("VehicleNumber")),
									VehicleModel = reader.IsDBNull(reader.GetOrdinal("Type")) ? null : reader.GetString(reader.GetOrdinal("Type")),

                                });
							}

						}


					}
					catch
					{
						return trips;
					}


				}


			}


			return trips;

		}

		public async Task<IEnumerable<Trip>> ScheduleTripsForSpecificDatesAsync(ScheduleDTO dto)
		{
			var trips = new List<Trip>();

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("[sp_ScheduleTripsForSpecificDates]", connection))
				{

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@RouteId", dto.RouteId);
					command.Parameters.AddWithValue("@Time", dto.DepartureTime);
					command.Parameters.AddWithValue("@ReverseTripTime", dto.ReturnTime);
					command.Parameters.AddWithValue("@Seats", dto.Seats);


					var weekDays = new SqlParameter
					{
						ParameterName = "@Dates",
						SqlDbType = SqlDbType.Structured,
						TypeName = "dbo.DatesType",
						Value = dto.CustomDates
					};

					command.Parameters.Add(weekDays);

					try
					{
						await connection.OpenAsync();
						using (var reader = await command.ExecuteReaderAsync())
						{
							while (reader.Read())
							{
								trips.Add(new()
								{
									Id = reader.GetInt32(reader.GetOrdinal("Id")),
									Date = reader.GetDateTime(reader.GetOrdinal("Date")),
									Time = reader.GetTimeSpan(reader.GetOrdinal("Time")),
									status = (TripStatus)reader.GetInt32(reader.GetOrdinal("status")),
									RouteId = reader.GetInt32(reader.GetOrdinal("RouteId")),
									Seats = reader.GetInt32(reader.GetOrdinal("Seats")),
									HasBookedSeat = reader.GetBoolean(reader.GetOrdinal("HasBookedSeat")),
									StatusCode = reader.GetInt64(reader.GetOrdinal("StatusCode")),
									MainTripId = reader.IsDBNull(reader.GetOrdinal("MainTripId")) ? null : reader.GetInt32(reader.GetOrdinal("MainTripId"))

								});
							}

						}


					}
					catch
					{
						return trips;
					}


				}


			}


			return trips;

		}



	}
}
