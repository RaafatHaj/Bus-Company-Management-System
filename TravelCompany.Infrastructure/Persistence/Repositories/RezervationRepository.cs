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
using TravelCompany.Domain.Settings;

namespace TravelCompany.Infrastructure.Persistence.Repositories
{
    internal class RezervationRepository :BaseRepository<Reservation> , IRezervationRepository
	{

		private readonly ApplicationDbContext _context;

		private readonly string _connectionString;

		public RezervationRepository(ApplicationDbContext context, string connectionString):base(context) 
		{
			_context = context;
			_connectionString = connectionString;
		}

		public async Task<(bool IsBooked , IEnumerable<int> ReservationIDs)> BookSeatAsync(BookTicketDTO dto)
		{
			var isBooked = false;
			var reservationIDs = new List<int>();

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_BookSeat", connection))
				{

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@TripId", dto.TripId);
					command.Parameters.AddWithValue("@SeatCode", dto.SeatCode);
					command.Parameters.AddWithValue("@StationAId", dto.StationAId);
					command.Parameters.AddWithValue("@StationBId", dto.StationBId);
					command.Parameters.AddWithValue("@PersonPhone", dto.PersonPhone);
					command.Parameters.AddWithValue("@PersonEmail", dto.PersonEmail);
					command.Parameters.AddWithValue("@UserId", dto.CreatedById);
					command.Parameters.AddWithValue("@TripDepartureDateTime", dto.TripDepatureDateTime);



					command.Parameters.Add(new SqlParameter("@IsBooked", SqlDbType.Bit) { Direction = ParameterDirection.Output });


					var BookedSeats = new SqlParameter
					{
						ParameterName = "@BookedSeatsInfo",
						SqlDbType = SqlDbType.Structured,
						TypeName = "dbo.BookedSeatType",
						Value = dto.BookedSeatsTable
					};

					command.Parameters.Add(BookedSeats);

					try
					{
						await connection.OpenAsync();

						using (var reader = await command.ExecuteReaderAsync())
						{
							while (reader.Read())
							{
								reservationIDs.Add(reader.GetInt32(reader.GetOrdinal("ReservationId")));
							}


						}
						isBooked = (bool)command.Parameters["@IsBooked"].Value;

					}
					catch
					{
						return (isBooked, reservationIDs);
					}


				}


			}


			return (isBooked, reservationIDs);

		}
	}
}
