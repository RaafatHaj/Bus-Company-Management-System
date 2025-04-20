using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addHomeStationToDriversAndVehicles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StationId",
                table: "Vehicles",
                type: "int",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "StationId",
                table: "Drivers",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_StationId",
                table: "Vehicles",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_StationId",
                table: "Drivers",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Stations_StationId",
                table: "Drivers",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Stations_StationId",
                table: "Vehicles",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Stations_StationId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Stations_StationId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_StationId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_StationId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "StationId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "StationId",
                table: "Drivers");
        }
    }
}
