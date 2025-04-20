using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addAssigmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTrips_Drivers_DriverId",
                table: "ScheduledTrips");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTrips_Vehicles_VehicleId",
                table: "ScheduledTrips");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTrips_DriverId",
                table: "ScheduledTrips");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTrips_VehicleId",
                table: "ScheduledTrips");

            migrationBuilder.DropColumn(
                name: "Reschedule",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "ScheduledTrips");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "ScheduledTrips");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "Vehicles",
                type: "int",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Vehicles",
                type: "nvarchar(100)",
                nullable: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasUnsignedTrip",
                table: "Recurrings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TripAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduledTripId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripAssignments_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "DriverId");
                    table.ForeignKey(
                        name: "FK_TripAssignments_ScheduledTrips_ScheduledTripId",
                        column: x => x.ScheduledTripId,
                        principalTable: "ScheduledTrips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripAssignments_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripAssignments_DriverId",
                table: "TripAssignments",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_TripAssignments_ScheduledTripId",
                table: "TripAssignments",
                column: "ScheduledTripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripAssignments_VehicleId",
                table: "TripAssignments",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripAssignments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "HasUnsignedTrip",
                table: "Recurrings");

            migrationBuilder.AddColumn<bool>(
                name: "Reschedule",
                table: "Trips",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "ScheduledTrips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "ScheduledTrips",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTrips_DriverId",
                table: "ScheduledTrips",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTrips_VehicleId",
                table: "ScheduledTrips",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTrips_Drivers_DriverId",
                table: "ScheduledTrips",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTrips_Vehicles_VehicleId",
                table: "ScheduledTrips",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId");
        }
    }
}
