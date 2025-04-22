using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameScheduledTripsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ScheduledTrips_ScheduledTravelId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelStations_ScheduledTrips_ScheduledTravelId",
                table: "TravelStations");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAssignments_ScheduledTrips_ScheduledTripId",
                table: "TripAssignments");

            migrationBuilder.DropTable(
                name: "ScheduledTrips");

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    HasBookedSeat = table.Column<bool>(type: "bit", nullable: false),
                    StatusCode = table.Column<long>(type: "bigint", nullable: false),
                    IsIrregular = table.Column<bool>(type: "bit", nullable: false),
                    MainTripId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_RouteId",
                table: "Trips",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Trips_ScheduledTravelId",
                table: "Reservations",
                column: "ScheduledTravelId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelStations_Trips_ScheduledTravelId",
                table: "TravelStations",
                column: "ScheduledTravelId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripAssignments_Trips_ScheduledTripId",
                table: "TripAssignments",
                column: "ScheduledTripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Trips_ScheduledTravelId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelStations_Trips_ScheduledTravelId",
                table: "TravelStations");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAssignments_Trips_ScheduledTripId",
                table: "TripAssignments");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.CreateTable(
                name: "ScheduledTrips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasBookedSeat = table.Column<bool>(type: "bit", nullable: false),
                    IsIrregular = table.Column<bool>(type: "bit", nullable: false),
                    MainTripId = table.Column<int>(type: "int", nullable: true),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    StatusCode = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledTrips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledTrips_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTrips_RouteId",
                table: "ScheduledTrips",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ScheduledTrips_ScheduledTravelId",
                table: "Reservations",
                column: "ScheduledTravelId",
                principalTable: "ScheduledTrips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelStations_ScheduledTrips_ScheduledTravelId",
                table: "TravelStations",
                column: "ScheduledTravelId",
                principalTable: "ScheduledTrips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripAssignments_ScheduledTrips_ScheduledTripId",
                table: "TripAssignments",
                column: "ScheduledTripId",
                principalTable: "ScheduledTrips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
