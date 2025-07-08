using Microsoft.Data.SqlClient;
using System.Data;
using TravelCompany.Application.Common.Interfaces.Repositories;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

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


        public async Task<IEnumerable<AvailableTripVehicleDTO>> GetAvailableVehicles(int tripId)
        {
            var vehicles = new List<AvailableTripVehicleDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {

                using (var command = new SqlCommand("sp_GetAvailableVehicleForTrip3", connection))
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
                                vehicles.Add(new()
                                {
                                    VehicleId= reader.GetInt32(reader.GetOrdinal("VehicleId")),                                   
                                    HomeStation= reader.GetString(reader.GetOrdinal("HomeStation")),
                                    VehicleNumber= reader.GetString(reader.GetOrdinal("VehicleNumber")),
                                    VehicleModel= reader.GetString(reader.GetOrdinal("Type")),
                                    AvailibiltyStartTime =reader.GetDateTime(reader.GetOrdinal("AvailabilityStartDate")),
                                    AvailibiltyEndTime =reader.GetDateTime(reader.GetOrdinal("AvailabilityEndDate"))
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

        public async Task<(bool Success,int ReturnTrpId , string ErrorMessage)> SetVehicleForTrip(AssignVehicleDTO dto)
        {
            bool isSuccess = false;
            int returnTrpId = 0;
            string errorMesssage = "";

            using (var connection = new SqlConnection(_connectionString))
            {

                using (var command = new SqlCommand("sp_AssignVehicleToTrip3", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@TripId", dto.TripId);
                    command.Parameters.AddWithValue("@VehicleId", dto.VehicleId);

                    command.Parameters.AddWithValue("@MainTripDateTime", dto.MainTripDateTime);
                    command.Parameters.AddWithValue("@MainTripCurrentDateTime", dto.MainTripNewDateTime);

					command.Parameters.AddWithValue("@ReturnTripDateTime", dto.ReturnTripDateTime ?? (object)DBNull.Value);
					command.Parameters.AddWithValue("@ReturnTripCurrentDateTime", dto.ReturnTripNewDateTime);



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
