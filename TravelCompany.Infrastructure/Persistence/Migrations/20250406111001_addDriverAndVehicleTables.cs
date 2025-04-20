using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addDriverAndVehicleTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "ScheduledTrips",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

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

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTrips_Drivers_DriverId",
                table: "ScheduledTrips");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTrips_Vehicles_VehicleId",
                table: "ScheduledTrips");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTrips_DriverId",
                table: "ScheduledTrips");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTrips_VehicleId",
                table: "ScheduledTrips");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "ScheduledTrips");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "ScheduledTrips");

            migrationBuilder.AlterColumn<byte>(
                name: "status",
                table: "ScheduledTrips",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
