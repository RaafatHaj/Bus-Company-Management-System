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
                                    //IsAvailable= reader.GetInt32(reader.GetOrdinal("IsAvailable"))==1?true:false,
                                    //AvailibiltyStartTime = reader.IsDBNull(reader.GetOrdinal("AvailabilityStartTime"))?null: reader.GetDateTime(reader.GetOrdinal("AvailabilityStartTime")),
                                    //AvailibiltyEndTime = reader.IsDBNull(reader.GetOrdinal("AvailabilityStartTime")) ? null : reader.GetDateTime(reader.GetOrdinal("AvailabilityEndTime")) 
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
    }
}
