using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addActiveTripsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelStations");

            migrationBuilder.DropTable(
                name: "Weeks");

            migrationBuilder.CreateTable(
                name: "ActiveTrips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false),
                    StationOrder = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StationStatus = table.Column<int>(type: "int", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualDepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveTrips", x => new { x.TripId, x.StationOrder });
                    table.ForeignKey(
                        name: "FK_ActiveTrips_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveTrips");

            migrationBuilder.CreateTable(
                name: "TravelStations",
                columns: table => new
                {
                    ScheduledTravelId = table.Column<int>(type: "int", nullable: false),
                    StationOrder = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    ArrvalDateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    BookedSeates = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelStations", x => new { x.ScheduledTravelId, x.StationOrder });
                    table.ForeignKey(
                        name: "FK_TravelStations_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelStations_Trips_ScheduledTravelId",
                        column: x => x.ScheduledTravelId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripPatternId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OccupiedDaysCode = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TripsNumber = table.Column<int>(type: "int", nullable: false),
                    UnassignedTripsNumber = table.Column<int>(type: "int", nullable: false),
                    WeekOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weeks_TripPatterns_TripPatternId",
                        column: x => x.TripPatternId,
                        principalTable: "TripPatterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelStations_StationId",
                table: "TravelStations",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Weeks_TripPatternId",
                table: "Weeks",
                column: "TripPatternId");
        }
    }
}
