using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Application.Common.Interfaces.Repositories;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Eums;
using TravelCompany.Infrastructure.Persistence.Migrations;

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
							    	ArrivedStationOrder = reader.GetInt32(reader.GetOrdinal("StatusCode")),
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

        public async Task<IEnumerable<PatternWeekDTO>> GetPatternWeeks(PatternWeeksRequestDTO dto)
        {
            var trips = new List<PatternWeekDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {

                using (var command = new SqlCommand("sp_GetPatternWeeks", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RouteId", dto.RouteId);
                    command.Parameters.AddWithValue("@Time", dto.Time);
                    command.Parameters.AddWithValue("@StartDate", dto.StartDate);
                    command.Parameters.AddWithValue("@EndDate", dto.EndDate);

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                trips.Add(new()
                                {
                                    WeekOrder = reader.GetInt32(reader.GetOrdinal("WeekOrder")),
                                    StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                                    EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                                    OccupiedWeekDaysCode = reader.GetInt32(reader.GetOrdinal("OccupaiedWeekDaysCode")),
                                    UnassignedTrips = reader.GetInt32(reader.GetOrdinal("UnassignedTrips")),
                                    AllTrips = reader.GetInt32(reader.GetOrdinal("AllTrips")),



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

        public async Task<IEnumerable<ScheduledTripBaseDTO>> ScheduleTripsForDaysInWeekAsync(ScheduleDTO dto)
		{
			var trips = new List<ScheduledTripBaseDTO>();

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_ScheduleTripsForDaysInWeek1", connection))
				{

					command.CommandType = CommandType.StoredProcedure;


					command.Parameters.AddWithValue("@RouteId", dto.RouteId);
					command.Parameters.AddWithValue("@Time", dto.DepartureTime);
					//command.Parameters.AddWithValue("@ReverseTripTime", dto.ReturnTime);
					//command.Parameters.AddWithValue("@Seats", dto.Seats);
					command.Parameters.AddWithValue("@StartDate", dto.StartDate);
					command.Parameters.AddWithValue("@EndDate", dto.EndDate);
					command.Parameters.AddWithValue("@Duration", dto.ScheduleDuration);
					command.Parameters.AddWithValue("@IsEditMode", dto.IsEditStatus);

					command.Parameters.AddWithValue("@HasBreak", dto.HasBreak);
					command.Parameters.AddWithValue("@StationStopMinutes", dto.StationStopMinutes);

					if (dto.HasBreak)
					{
						command.Parameters.AddWithValue("@StationOrderNextToBreak", dto.StationOrderNextToBreak);
						command.Parameters.AddWithValue("@BreakMinutes", dto.BreakMinutes);
					}





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
									TripId = reader.GetInt32(reader.GetOrdinal("Id")),
									Date = reader.GetDateTime(reader.GetOrdinal("Date")),
									Time = reader.GetTimeSpan(reader.GetOrdinal("Time")),
									Status = (TripStatus)reader.GetInt32(reader.GetOrdinal("status")),
									RouteId = reader.GetInt32(reader.GetOrdinal("RouteId")),
									Seats = reader.IsDBNull(reader.GetOrdinal("Seats")) ? null : reader.GetInt32(reader.GetOrdinal("Seats")),
									HasBookedSeat = reader.GetBoolean(reader.GetOrdinal("HasBookedSeat")),
									ArrivedStationOrder = reader.GetInt32(reader.GetOrdinal("ArrivedStationOrder")),
									MainTripId = reader.IsDBNull(reader.GetOrdinal("MainTripId")) ? null : reader.GetInt32(reader.GetOrdinal("MainTripId")),
									RouteName = reader.GetString(reader.GetOrdinal("RouteName")),

									DepartureStationId = reader.GetInt32(reader.GetOrdinal("FirstStationId")),
									TripTimeSpanInMInits = reader.GetInt32(reader.GetOrdinal("EstimatedTime")),
									VehicleId = reader.IsDBNull(reader.GetOrdinal("VehicleId")) ? null : reader.GetInt32(reader.GetOrdinal("VehicleId")),
									VehicleNumber = reader.IsDBNull(reader.GetOrdinal("VehicleNumber")) ? null : reader.GetString(reader.GetOrdinal("VehicleNumber")),
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

		public async Task<IEnumerable<ScheduledTripBaseDTO>> ScheduleTripsForEverySingleDayAsync(ScheduleDTO dto)
		{
			var trips = new List<ScheduledTripBaseDTO>();

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_ScheduleTripsForEverySingleDay1", connection))
				{

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@RouteId", dto.RouteId);
					command.Parameters.AddWithValue("@Time", dto.DepartureTime);
					//command.Parameters.AddWithValue("@ReverseTripTime", dto.ReturnTime);
					//command.Parameters.AddWithValue("@Seats", dto.Seats);
					command.Parameters.AddWithValue("@StartDate", dto.StartDate);
					command.Parameters.AddWithValue("@EndDate", dto.EndDate);
					command.Parameters.AddWithValue("@Duration", dto.ScheduleDuration);
					command.Parameters.AddWithValue("@IsEditMode", dto.IsEditStatus);

					command.Parameters.AddWithValue("@HasBreak", dto.HasBreak);
					command.Parameters.AddWithValue("@StationStopMinutes", dto.StationStopMinutes);

					if(dto.HasBreak)
					{
						command.Parameters.AddWithValue("@StationOrderNextToBreak", dto.StationOrderNextToBreak);
						command.Parameters.AddWithValue("@BreakMinutes", dto.BreakMinutes);
					}
		


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
									Seats = reader.IsDBNull(reader.GetOrdinal("Seats"))?null: reader.GetInt32(reader.GetOrdinal("Seats")) ,
									HasBookedSeat = reader.GetBoolean(reader.GetOrdinal("HasBookedSeat")),
									ArrivedStationOrder = reader.GetInt32(reader.GetOrdinal("ArrivedStationOrder")),
									MainTripId = reader.IsDBNull(reader.GetOrdinal("MainTripId")) ? null : reader.GetInt32(reader.GetOrdinal("MainTripId")),
									RouteName = reader.GetString(reader.GetOrdinal("RouteName")),

									DepartureStationId = reader.GetInt32(reader.GetOrdinal("FirstStationId")),
									TripTimeSpanInMInits= reader.GetInt32(reader.GetOrdinal("EstimatedTime")),
									VehicleId = reader.IsDBNull(reader.GetOrdinal("VehicleId")) ? null : reader.GetInt32(reader.GetOrdinal("VehicleId")),
									VehicleNumber = reader.IsDBNull(reader.GetOrdinal("VehicleNumber")) ? null : reader.GetString(reader.GetOrdinal("VehicleNumber")),
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

		public async Task<IEnumerable<ScheduledTripBaseDTO>> ScheduleTripsForSpecificDatesAsync(ScheduleDTO dto)
		{
			var trips = new List<ScheduledTripBaseDTO>();

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_ScheduleTripsForSpecificDates3", connection))
				{

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@RouteId", dto.RouteId);
					command.Parameters.AddWithValue("@Time", dto.DepartureTime);
					//command.Parameters.AddWithValue("@ReverseTripTime", dto.ReturnTime);
					//command.Parameters.AddWithValue("@Seats", dto.Seats);

					command.Parameters.AddWithValue("@HasBreak", dto.HasBreak);
					command.Parameters.AddWithValue("@StationStopMinutes", dto.StationStopMinutes);

					if (dto.HasBreak)
					{
						command.Parameters.AddWithValue("@StationOrderNextToBreak", dto.StationOrderNextToBreak);
						command.Parameters.AddWithValue("@BreakMinutes", dto.BreakMinutes);
					}


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
									TripId = reader.GetInt32(reader.GetOrdinal("Id")),
									Date = reader.GetDateTime(reader.GetOrdinal("Date")),
									Time = reader.GetTimeSpan(reader.GetOrdinal("Time")),
									Status = (TripStatus)reader.GetInt32(reader.GetOrdinal("status")),
									RouteId = reader.GetInt32(reader.GetOrdinal("RouteId")),
									Seats = reader.IsDBNull(reader.GetOrdinal("Seats")) ? null : reader.GetInt32(reader.GetOrdinal("Seats")),
									HasBookedSeat = reader.GetBoolean(reader.GetOrdinal("HasBookedSeat")),
									ArrivedStationOrder = reader.GetInt32(reader.GetOrdinal("ArrivedStationOrder")),
									MainTripId = reader.IsDBNull(reader.GetOrdinal("MainTripId")) ? null : reader.GetInt32(reader.GetOrdinal("MainTripId")),
									RouteName = reader.GetString(reader.GetOrdinal("RouteName")),


                                    DepartureStationId = reader.GetInt32(reader.GetOrdinal("FirstStationId")),
									TripTimeSpanInMInits = reader.GetInt32(reader.GetOrdinal("EstimatedTime")),
									VehicleId = reader.IsDBNull(reader.GetOrdinal("VehicleId")) ? null : reader.GetInt32(reader.GetOrdinal("VehicleId")),
									VehicleNumber = reader.IsDBNull(reader.GetOrdinal("VehicleNumber")) ? null : reader.GetString(reader.GetOrdinal("VehicleNumber")),
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

		public async Task<IEnumerable<StationTrackDTO>> GetStationTripSTrack(int stationId)
		{
			var tracks = new List<StationTrackDTO>();

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_GetStationTripsTrack", connection))
				{

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@StationId", stationId);

					try
					{
						await connection.OpenAsync();
						using (var reader = await command.ExecuteReaderAsync())
						{
							while (reader.Read())
							{
								tracks.Add(new()
								{
									TripId = reader.GetInt32(reader.GetOrdinal("TripId")),
									StationOrder = reader.GetInt32(reader.GetOrdinal("StationOrder")),
									StationId= reader.GetInt32(reader.GetOrdinal("StationId")),
									StationName= reader.GetString(reader.GetOrdinal("StationName")),
									Status = (StationStatus)reader.GetInt32(reader.GetOrdinal("Status")),
									PreviousStation = reader.IsDBNull(reader.GetOrdinal("PreviousStation")) ? null : reader.GetString(reader.GetOrdinal("PreviousStation")),
									NexttStation = reader.IsDBNull(reader.GetOrdinal("NexttStation")) ? null : reader.GetString(reader.GetOrdinal("NexttStation")),
									ArrivalDateTime = reader.GetDateTime(reader.GetOrdinal("ArrivalDateTime")),
									DepartureDateTime = reader.GetDateTime(reader.GetOrdinal("DepartureDateTime")),
									TripStatus = (TripStatus)reader.GetInt32(reader.GetOrdinal("TripStatus")),
									RouteName = reader.GetString(reader.GetOrdinal("RouteName")),
									EstimatedDistance = reader.GetInt32(reader.GetOrdinal("EstimatedDistance"))


								});
							}

						}


					}
					catch
					{
						return tracks;
					}


				}


			}


			return tracks;

		}

        public async Task<IEnumerable<SuitableTripDTO>> GetSuitableTripsAsync(int stationAId, int stationBId, DateTime tripDate)
        {
            var travels = new List<SuitableTripDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {

                using (var command = new SqlCommand("sp_FindSuitableTrip", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StationAId", stationAId);
                    command.Parameters.AddWithValue("@StationBId", stationBId);
                    command.Parameters.Add("@Date", SqlDbType.Date).Value = tripDate.Date;

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                travels.Add(new()
                                {

                                    TripId = reader.GetInt32(reader.GetOrdinal("TripId")),
                                    StationStatus = (StationStatus)reader.GetInt32(reader.GetOrdinal("Status")),

                                    StationAOrder = reader.GetInt32(reader.GetOrdinal("StationOrder")),
									StationAId= reader.GetInt32(reader.GetOrdinal("StationId")),
									StationAName = reader.GetString(reader.GetOrdinal("StationName")),
                                    DateAndTime = reader.GetDateTime(reader.GetOrdinal("DepartureDateTime")),
									ArrivalDateAndTime= reader.GetDateTime(reader.GetOrdinal("StationBArrivalTime")),

									StationBName = reader.GetString(reader.GetOrdinal("StationBName")),
									StationBId= reader.GetInt32(reader.GetOrdinal("StationBId")),
									RouteName = reader.GetString(reader.GetOrdinal("RouteName"))




                                });
                            }

                        }


                    }
                    catch
                    {
                        return travels;
                    }


                }


            }


            return travels;

        }

		public async Task<(long SeatCode , IList<BookedSeatsDTO> AvalibleSeats)> GetAvaliableSeatsAsync(int tripId, int stationAId, int stationBId)
		{
			var avaliableSeats = new List<BookedSeatsDTO>();
			long seatCode = 0;

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_GetAvaliableSeats", connection))
				{

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@TripId", tripId);
					command.Parameters.AddWithValue("@StationAId", stationAId);
					command.Parameters.AddWithValue("@StationBId", stationBId);

					command.Parameters.Add(new SqlParameter("@SeatCode", SqlDbType.BigInt) { Direction = ParameterDirection.Output });


					try
					{
						await connection.OpenAsync();
						using (var reader = await command.ExecuteReaderAsync())
						{
							while (reader.Read())
							{
								avaliableSeats.Add(new(){

								 SeatNumber=reader.GetInt32(reader.GetOrdinal("SeatNumber")),
								 Status =(SeatStatus)reader.GetInt32(reader.GetOrdinal("Status"))
								
								});
							}


						}
							seatCode= (long)command.Parameters["@SeatCode"].Value;


					}
					catch
					{
						return (seatCode,avaliableSeats);
					}


				}


			}


			return (seatCode, avaliableSeats);


		}

		public async Task<(bool Success, int ReturnTrpId, string Message)> EditTrip(EditScheduledTripDTO dto)
		{
			bool isSuccess = false;
			int returnTrpId = 0;
			string message = "";

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_EditTrip", connection))
				{

					command.CommandType = CommandType.StoredProcedure;



					command.Parameters.AddWithValue("@TripId", dto.TripId);
					command.Parameters.AddWithValue("@VehicleId", dto.VehicleId ?? (object)DBNull.Value);
					command.Parameters.AddWithValue("@MainTrip_OldDateTime", dto.MainTripOldDateTime);
					command.Parameters.AddWithValue("@ReturnTrip_OldDateTime", dto.ReturnTripOldDateTime ?? (object)DBNull.Value);
					command.Parameters.AddWithValue("@MainTrip_NewDateTime", dto.MainTripNewDate.Add(dto.MainTripNewTime));
					command.Parameters.AddWithValue("@ReturnTrip_NewDateTime", dto.ReturnTripNewDate.Add(dto.ReturnTripNewTime));
					command.Parameters.AddWithValue("@MainTrip_HasBookedSeats", dto.MainTripHasBookedSeats);
					command.Parameters.AddWithValue("@MainTrip_StationStopMinutes", dto.MainTripStationStopMinutes);
					command.Parameters.AddWithValue("@MainTrip_HasBreak", dto.MainTripHasBreak);
					command.Parameters.AddWithValue("@MainTrip_BreakMinutes", dto.MainTripBreakMinutes ?? (object)DBNull.Value);
					command.Parameters.AddWithValue("@MainTrip_StationOrderNextToBreak", dto.MainTripStationOrderNextToBreak??(object)DBNull.Value);
					command.Parameters.AddWithValue("@ReturnTrip_HasBookedSeats", dto.ReturnTripHasBookedSeats);
					command.Parameters.AddWithValue("@ReturnTrip_StationStopMinutes", dto.ReturnTripStationStopMinutes);
					command.Parameters.AddWithValue("@ReturnTrip_HasBreak", dto.ReturnTripHasBreak);
					command.Parameters.AddWithValue("@ReturnTrip_BreakMinutes", dto.ReturnTripBreakMinutes?? (object)DBNull.Value);
					command.Parameters.AddWithValue("@ReturnTrip_StationOrderNextToBreak", dto.ReturnTripStationOrderNextToBreak?? (object)DBNull.Value);
				

					command.Parameters.Add(new SqlParameter("@IsSuccess", SqlDbType.Bit) { Direction = ParameterDirection.Output });
					command.Parameters.Add(new SqlParameter("@ReturnTripId", SqlDbType.Int) { Direction = ParameterDirection.Output });
					command.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output });



					try
					{
						await connection.OpenAsync();

						await command.ExecuteNonQueryAsync();

						isSuccess = (bool)command.Parameters["@IsSuccess"].Value;
						returnTrpId = (int)command.Parameters["@ReturnTripId"].Value;
						message = (string)command.Parameters["@ErrorMessage"].Value;


					}
					catch (Exception ex)
					{
						return (isSuccess, 0, ex.Message);
					}


				}


			}


			return (isSuccess, returnTrpId, message);
		}

		public async Task<IEnumerable<TripTrackDTO>> GetTripTrackAsunc(int tripId)
		{
			var trips = new List<TripTrackDTO>();

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_GetTripTrack", connection))
				{

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@TripId", tripId);
					

					try
					{
						await connection.OpenAsync();
						using (var reader = await command.ExecuteReaderAsync())
						{
							while (reader.Read())
							{
								trips.Add(new()
								{
									TripId = reader.GetInt32(reader.GetOrdinal("TripId")),
									StationOrder = reader.GetInt32(reader.GetOrdinal("StationOrder")),
									StationId = reader.GetInt32(reader.GetOrdinal("StationId")),
									StationName = reader.GetString(reader.GetOrdinal("StationName")),
									Status = (StationStatus)reader.GetInt32(reader.GetOrdinal("Status")),
									ArrivalDateTime = reader.GetDateTime(reader.GetOrdinal("ArrivalDateTime")),
									DepartureDateTime = reader.GetDateTime(reader.GetOrdinal("DepartureDateTime")),

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
