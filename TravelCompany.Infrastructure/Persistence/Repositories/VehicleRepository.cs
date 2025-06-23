using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces.Repositories;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Infrastructure.Persistence.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {


        private readonly ApplicationDbContext _context;

        private readonly string _connectionString;

        public VehicleRepository(ApplicationDbContext context, string connectionString) : base(context)
        {
            _connectionString = connectionString;
            _context = context;
        }


        public async Task<IEnumerable<AvailableTripVehicleDTO>> GetAvailableVehicles(DateTime tripTime, int departureStationId, int tripSpanInMinits)
        {
            var vehicles = new List<AvailableTripVehicleDTO>();
            using (var connection = new SqlConnection(_connectionString))
            {

                using (var command = new SqlCommand("sp_GetAvailableVehicleForTrip", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TripTime", tripTime);
                    command.Parameters.AddWithValue("@StationId", departureStationId);
                    command.Parameters.AddWithValue("@TripSpanInMinits", tripSpanInMinits);


                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                vehicles.Add(new()
                                {
                                    VehicleId= reader.GetInt32(reader.GetOrdinal("VehicleId")),
                                    VehicleNumber= reader.GetString(reader.GetOrdinal("VehicleNumber")),
                                    VehicleModel= reader.GetString(reader.GetOrdinal("Type")),
                                    IsAvailable = reader.GetInt32(reader.GetOrdinal("IsAvailable")) == 1 ? true : false,
                                    AvailibiltyStartTime = reader.IsDBNull(reader.GetOrdinal("AvalibilityStartTime")) ? null : reader.GetDateTime(reader.GetOrdinal("AvalibilityStartTime")),
                                    AvailibiltyEndTime = reader.IsDBNull(reader.GetOrdinal("AvalibilityEndTime")) ? null : reader.GetDateTime(reader.GetOrdinal("AvalibilityEndTime"))
                                });
                            }

                        }


                    }
                    catch
                    {
                        return vehicles;
                    }


                }


            }


            return vehicles;
        }

        public async Task<(bool Success,int ReturnTrpId , string ErrorMessage)> SetVehicleForTrip(ScheduledTripDTO dto)
        {
            bool isSuccess = false;
            int returnTrpId = 0;
            string errorMesssage = "";

            using (var connection = new SqlConnection(_connectionString))
            {

                using (var command = new SqlCommand("sp_AssignVehicleToTrip", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TripId", dto.TripId);
                    command.Parameters.AddWithValue("@VehicleId", dto.VehicleId);
                    command.Parameters.AddWithValue("@MainTripDateTime", dto.Date + dto.Time);
                    command.Parameters.AddWithValue("@ReturnTripDateTime", dto.ReturnDate + dto.ReturnTime);


                    
                    command.Parameters.Add(new SqlParameter("@IsAssigend", SqlDbType.Bit) { Direction = ParameterDirection.Output });
                    command.Parameters.Add(new SqlParameter("@ReturnTripId", SqlDbType.Int) { Direction = ParameterDirection.Output });
                    command.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar,500) { Direction = ParameterDirection.Output });



                    try
                    {
                        await connection.OpenAsync();

                        await command.ExecuteNonQueryAsync();

                        isSuccess = (bool)command.Parameters["@IsAssigend"].Value;
                        returnTrpId = (int)command.Parameters["@ReturnTripId"].Value;
                        errorMesssage = (string)command.Parameters["@ErrorMessage"].Value;


                    }
                    catch (Exception ex) 
                    {
                        return (isSuccess,0, ex.Message);
                    }


                }


            }


            return (isSuccess, returnTrpId, errorMesssage);
        }
    }
}
