using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addDeprtureStationIdToTripAssigmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Stations_FirstStationId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Stations_LastStationId",
                table: "Routes");

            migrationBuilder.AddColumn<int>(
                name: "DepartureStationId",
                table: "TripAssignments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LastStationId",
                table: "Routes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FirstStationId",
                table: "Routes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Stations_FirstStationId",
                table: "Routes",
                column: "FirstStationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Stations_LastStationId",
                table: "Routes",
                column: "LastStationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Stations_FirstStationId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Stations_LastStationId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "DepartureStationId",
                table: "TripAssignments");

            migrationBuilder.AlterColumn<int>(
                name: "LastStationId",
                table: "Routes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FirstStationId",
                table: "Routes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Stations_FirstStationId",
                table: "Routes",
                column: "FirstStationId",
                principalTable: "Stations",
                principalColumn: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Stations_LastStationId",
                table: "Routes",
                column: "LastStationId",
                principalTable: "Stations",
                principalColumn: "StationId");
        }
    }
}
