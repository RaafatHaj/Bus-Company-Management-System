
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
    public class ScheduledTravelRepository :BaseRepository<Trip>, IScheduledTravelRepository
    {

        private readonly ApplicationDbContext _context;

        private readonly string _connectionString;

        public ScheduledTravelRepository(ApplicationDbContext context,string connectionString):base(context)
        {
            _connectionString = connectionString;
            _context = context;
        }

        public async Task<IEnumerable<ScheduledTravelsMainViewDTO>> GetMainScheduledTravelsAsync()
        {
            var travels=new List<ScheduledTravelsMainViewDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {

                using (var command = new SqlCommand("sp_RetriveSchedueledTravelsMainView", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader =await command.ExecuteReaderAsync())
                        {
                            while(reader.Read())
                            {
                                travels.Add(new()
                                {
    
                                  ScheduledTravelId=reader.GetInt32(reader.GetOrdinal("Id")),
                                  Date=reader.GetDateTime(reader.GetOrdinal("TravelDate")),
                                  TravelTime=reader.GetTimeSpan(reader.GetOrdinal("TravelTime")),
                                  RouteName=reader.GetString(reader.GetOrdinal("RouteName")),
                                  Seats=reader.GetInt32(reader.GetOrdinal("Seats")),
                                  Status=(TripStatus)reader.GetByte(reader.GetOrdinal("status")),
                                  FirstStation=reader.GetString(reader.GetOrdinal("FirstStation")),
                                  LastStation=reader.GetString(reader.GetOrdinal("LastStation")),

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

        public async Task<IEnumerable<TravelStation>> GetScheduledTravelDetailsAsync(int scheduledTravelId)
        {
            var details=new List<TravelStation>();

            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"select *from ScheduledTravelDetails where ScheduledTravelId=@scheduledTravelId";

                using (var command = new SqlCommand(query, connection))
                {

                    // command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@scheduledTravelId", scheduledTravelId);

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                details.Add(new()
                                {

                                    ScheduledTravelId = reader.GetInt32(reader.GetOrdinal("ScheduledTravelId")),
                                    StationOrder = reader.GetInt32(reader.GetOrdinal("StationOrder")),
                                    StationId = reader.GetInt32(reader.GetOrdinal("StationId")),
                                    Status = (StationStatus)reader.GetInt32(reader.GetOrdinal("Status")),
                                  //  ArrivalTime = reader.GetTimeSpan(reader.GetOrdinal("ArrivalTime")),
                                    AvailableSeats = reader.GetInt32(reader.GetOrdinal("AvailableSeats")),
                                    BookedSeates = reader.GetInt32(reader.GetOrdinal("BookedSeates")),

                                });
                            }

                        }


                    }
                    catch
                    {
                        return details;
                    }


                }


            }


            return details;
        }

        public async Task<IEnumerable<SuitableTravelDTO>> GetSuitableTravelsAsync(int stationAId, int StationBId)
        {
            var travels= new List<SuitableTravelDTO>();
            using (var connection = new SqlConnection(_connectionString))
            {

                using (var command = new SqlCommand("sp_FindSuitableTravel2", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StationAId", stationAId);
                    command.Parameters.AddWithValue("@StationBId", StationBId);

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                travels.Add(new()
                                {

                                    ScheduledTravelID= reader.GetInt32(reader.GetOrdinal("ScheduledTravelId")),
                                    Status = (StationStatus)reader.GetInt32(reader.GetOrdinal("Status")),

                                    StationAOrder=reader.GetInt32(reader.GetOrdinal("SAOrder")),
                                    StationAId = reader.GetInt32(reader.GetOrdinal("SAId")),
                                    StationAName = reader.GetString(reader.GetOrdinal("SAName")),
                                    StationAArrivalDateAndTime = reader.GetDateTime(reader.GetOrdinal("SAArrivalDateAndTime")),

                                    StationBOrder = reader.GetInt32(reader.GetOrdinal("SBOrder")),
                                    StationBId = reader.GetInt32(reader.GetOrdinal("SBId")),
                                    StationBName = reader.GetString(reader.GetOrdinal("SBName")),
                                    StationBArrivalDateAndTime = reader.GetDateTime(reader.GetOrdinal("SBArrivalDateAndTime")),


                                    AvailableSeats = reader.GetInt32(reader.GetOrdinal("AvailabelSeats")),
                                    BookedSeats = reader.GetInt32(reader.GetOrdinal("BookedSeats")),
                                    RouteStationsNumber= reader.GetInt32(reader.GetOrdinal("RouteStationsNumber")),
                                    



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

        public async Task<IEnumerable<int>> GetAvaliableSeatsAsync(GetAvaliableSeatsDTO dto)
        {
            var avaliableSeats = new List<int>();
            using (var connection = new SqlConnection(_connectionString))
            {

                using (var command = new SqlCommand("sp_GetAvaliableSeats", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AllSeatsNumber", dto.seatsNumbers);
                    command.Parameters.AddWithValue("@StationAOrder", dto.StationAOrder);
                    command.Parameters.AddWithValue("@StationBOrder", dto.StationBOrder);
                    command.Parameters.AddWithValue("@AllStationsNumber", dto.RouteStationsNumber);
                    command.Parameters.AddWithValue("@ScheduledTravelId", dto.ScheduledTravelId);

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                avaliableSeats.Add(reader.GetInt32(reader.GetOrdinal("SeatNumber")));
                            }

                        }


                    }
                    catch
                    {
                        return avaliableSeats;
                    }


                }


            }


            return avaliableSeats;

        }



        public async Task<bool> SetStationStatusAsLeftAsync(int stationId, int shceduledTravelId)
        {
            var isSuccess = false;

            using (var connection = new SqlConnection(_connectionString))
            {

                using (var command = new SqlCommand("sp_SetStationStatusAsLeft", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StationId", stationId);
                    command.Parameters.AddWithValue("@ScheduledTravelId", shceduledTravelId);
                



                    command.Parameters.Add(new SqlParameter("@IsSuccessed", SqlDbType.Bit) { Direction = ParameterDirection.Output });

                    try
                    {
                        await connection.OpenAsync();

                        await command.ExecuteNonQueryAsync();

                        isSuccess = (bool)command.Parameters["@IsSuccessed"].Value;

                    }
                    catch
                    {
                        return isSuccess;
                    }


                }


            }


            return isSuccess;

        }



    }
}
