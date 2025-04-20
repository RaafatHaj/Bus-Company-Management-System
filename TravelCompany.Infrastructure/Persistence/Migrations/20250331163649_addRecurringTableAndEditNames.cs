using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addRecurringTableAndEditNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Travels_TravelId",
                table: "Days");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ScheduledTravels_ScheduledTravelId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelStations_ScheduledTravels_ScheduledTravelId",
                table: "TravelStations");

            migrationBuilder.DropTable(
                name: "ScheduledTravels");

            migrationBuilder.DropTable(
                name: "Travels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Days",
                table: "Days");

            migrationBuilder.RenameColumn(
                name: "TravelId",
                table: "Days",
                newName: "RecurringId");

            migrationBuilder.AlterColumn<int>(
                name: "day",
                table: "Days",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Days",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Days",
                table: "Days",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    TripTime = table.Column<TimeSpan>(type: "time(7)", nullable: false),
                    ScheduleDuration = table.Column<int>(type: "int", nullable: true),
                    Reschedule = table.Column<bool>(type: "bit", nullable: false),
                    HasIrregularTrip = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_Trips_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recurrings",
                columns: table => new
                {
                    RecurringId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    RecurringType = table.Column<int>(type: "int", nullable: false),
                    FirstTripDate = table.Column<DateTime>(type: "date", nullable: false),
                    LastTripDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recurrings", x => x.RecurringId);
                    table.ForeignKey(
                        name: "FK_Recurrings_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledTrips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    HasBookedSeat = table.Column<bool>(type: "bit", nullable: false),
                    StatusCode = table.Column<long>(type: "bigint", nullable: false),
                    IsIrregular = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledTrips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledTrips_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Days_RecurringId",
                table: "Days",
                column: "RecurringId");

            migrationBuilder.CreateIndex(
                name: "IX_Recurrings_TripId",
                table: "Recurrings",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTrips_TripId",
                table: "ScheduledTrips",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_RouteId",
                table: "Trips",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Recurrings_RecurringId",
                table: "Days",
                column: "RecurringId",
                principalTable: "Recurrings",
                principalColumn: "RecurringId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ScheduledTrips_ScheduledTravelId",
                table: "Reservations",
                column: "ScheduledTravelId",
                principalTable: "ScheduledTrips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelStations_ScheduledTrips_ScheduledTravelId",
                table: "TravelStations",
                column: "ScheduledTravelId",
                principalTable: "ScheduledTrips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Recurrings_RecurringId",
                table: "Days");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ScheduledTrips_ScheduledTravelId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelStations_ScheduledTrips_ScheduledTravelId",
                table: "TravelStations");

            migrationBuilder.DropTable(
                name: "Recurrings");

            migrationBuilder.DropTable(
                name: "ScheduledTrips");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Days",
                table: "Days");

            migrationBuilder.DropIndex(
                name: "IX_Days_RecurringId",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Days");

            migrationBuilder.RenameColumn(
                name: "RecurringId",
                table: "Days",
                newName: "TravelId");

            migrationBuilder.AlterColumn<byte>(
                name: "day",
                table: "Days",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Days",
                table: "Days",
                columns: new[] { "TravelId", "day" });

            migrationBuilder.CreateTable(
                name: "Travels",
                columns: table => new
                {
                    TravelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    ReSchedule = table.Column<bool>(type: "bit", nullable: false),
                    ScheduleDuration = table.Column<int>(type: "int", nullable: true),
                    ScheduleEndingDate = table.Column<DateTime>(type: "date", nullable: true),
                    ScheduleType = table.Column<int>(type: "int", nullable: false),
                    TravelTime = table.Column<TimeSpan>(type: "time(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travels", x => x.TravelId);
                    table.ForeignKey(
                        name: "FK_Travels_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledTravels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TravelId = table.Column<int>(type: "int", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasBookedSeat = table.Column<bool>(type: "bit", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    StatusCode = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledTravels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledTravels_Travels_TravelId",
                        column: x => x.TravelId,
                        principalTable: "Travels",
                        principalColumn: "TravelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTravels_TravelId",
                table: "ScheduledTravels",
                column: "TravelId");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_RouteId",
                table: "Travels",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Travels_TravelId",
                table: "Days",
                column: "TravelId",
                principalTable: "Travels",
                principalColumn: "TravelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ScheduledTravels_ScheduledTravelId",
                table: "Reservations",
                column: "ScheduledTravelId",
                principalTable: "ScheduledTravels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelStations_ScheduledTravels_ScheduledTravelId",
                table: "TravelStations",
                column: "ScheduledTravelId",
                principalTable: "ScheduledTravels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
