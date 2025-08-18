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
	public class StationRepository : BaseRepository<Station>, IStationRepository
	{

		private readonly ApplicationDbContext _context;

		private readonly string _connectionString;

		public StationRepository(ApplicationDbContext context, string connectionString) : base(context)
		{
			_connectionString = connectionString;
			_context = context;
		}

		public async Task<(bool Success ,string Message,StationTrackDTO  Data)> SetStationAsArrived(int tripId, int stationId, int stationOrder)
		{

			var data = new StationTrackDTO();
			var success = false;
			var message = "";

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_MarkStationAsArrived2", connection))
				{

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@TripId", tripId);
					command.Parameters.AddWithValue("@StationId", stationId);
					command.Parameters.AddWithValue("@StationOrder", stationOrder);

					command.Parameters.Add(new SqlParameter("@IsMarked", SqlDbType.Bit) { Direction = ParameterDirection.Output });
					command.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar,-1) { Direction = ParameterDirection.Output });


					try
					{
						await connection.OpenAsync();
						using (var reader = await command.ExecuteReaderAsync())
						{
							while (reader.Read())
							{

								data.TripId = reader.GetInt32(reader.GetOrdinal("TripId"));
								data.StationOrder = reader.GetInt32(reader.GetOrdinal("StationOrder"));
								data.StationId = reader.GetInt32(reader.GetOrdinal("StationId"));
								data.EstimatedDistance = reader.GetInt32(reader.GetOrdinal("EstimatedDistance"));
								data.StationName = reader.GetString(reader.GetOrdinal("StationName"));
								data.PreviousStation = reader.IsDBNull(reader.GetOrdinal("PreviousStation")) ? null : reader.GetString(reader.GetOrdinal("PreviousStation"));
								data.NexttStation = reader.IsDBNull(reader.GetOrdinal("NexttStation")) ? null : reader.GetString(reader.GetOrdinal("NexttStation"));
								data.RouteName = reader.GetString(reader.GetOrdinal("RouteName"));
								data.ArrivalDateTime = reader.GetDateTime(reader.GetOrdinal("ActualArrivalDateTime"));
								data.DepartureDateTime = reader.GetDateTime(reader.GetOrdinal("ActualDepartureDateTime"));
								data.Status = (StationStatus)reader.GetInt32(reader.GetOrdinal("Status"));
								data.TripStatus = (TripStatus)reader.GetInt32(reader.GetOrdinal("TripStatus"));
								data.ArrivelLateMinutes = reader.GetInt32(reader.GetOrdinal("ArrivalLateMinutes"));
								data.DepartureLateMunutes = reader.GetInt32(reader.GetOrdinal("DepartureLateMinutes"));



							}


						}
						success = (bool)command.Parameters["@IsMarked"].Value;
						message = (string)command.Parameters["@ErrorMessage"].Value;


					}
					catch
					{
						return (success,message, data);
					}


				}


			}


			return (success,message, data);



		}

		public async Task<(bool Success, string Message, StationTrackDTO Data)> SetStationAsMoved(int tripId, int stationId, int stationOrder)
		{
			var data = new StationTrackDTO();
			bool success = false;
			var message = "";

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_MarkStationAsMoved2", connection))
				{

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@TripId", tripId);
					command.Parameters.AddWithValue("@StationId", stationId);
					command.Parameters.AddWithValue("@StationOrder", stationOrder);

					command.Parameters.Add(new SqlParameter("@IsMarked", SqlDbType.Bit) { Direction = ParameterDirection.Output });
					command.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar , -1) { Direction = ParameterDirection.Output });

					try
					{
						await connection.OpenAsync();
						using (var reader = await command.ExecuteReaderAsync())
						{
							while (reader.Read())
							{
								data.TripId = reader.GetInt32(reader.GetOrdinal("TripId"));
								data.StationOrder = reader.GetInt32(reader.GetOrdinal("StationOrder"));
								data.StationId = reader.GetInt32(reader.GetOrdinal("StationId"));
								data.EstimatedDistance = reader.GetInt32(reader.GetOrdinal("EstimatedDistance"));
								data.StationName = reader.GetString(reader.GetOrdinal("StationName"));
								data.PreviousStation = reader.IsDBNull(reader.GetOrdinal("PreviousStation")) ? null : reader.GetString(reader.GetOrdinal("PreviousStation"));
								data.NexttStation = reader.IsDBNull(reader.GetOrdinal("NexttStation")) ? null : reader.GetString(reader.GetOrdinal("NexttStation"));
								data.RouteName = reader.GetString(reader.GetOrdinal("RouteName"));
								data.ArrivalDateTime = reader.GetDateTime(reader.GetOrdinal("ActualArrivalDateTime"));
								data.DepartureDateTime = reader.GetDateTime(reader.GetOrdinal("ActualDepartureDateTime"));
								data.Status = (StationStatus)reader.GetInt32(reader.GetOrdinal("Status"));
								data.TripStatus = (TripStatus)reader.GetInt32(reader.GetOrdinal("TripStatus"));
								data.ArrivelLateMinutes = reader.GetInt32(reader.GetOrdinal("ArrivalLateMinutes"));
								data.DepartureLateMunutes = reader.GetInt32(reader.GetOrdinal("DepartureLateMinutes"));


							}


						}
						success = (bool)command.Parameters["@IsMarked"].Value;
						message = (string)command.Parameters["@ErrorMessage"].Value;


					}
					catch
					{
						return (success, message, data);
					}


				}


			}


			return (success, message, data);


		}
	}
}
