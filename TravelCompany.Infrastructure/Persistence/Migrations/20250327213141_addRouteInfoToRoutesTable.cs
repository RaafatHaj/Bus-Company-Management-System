using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addRouteInfoToRoutesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Stations_FirstStationId",
                table: "Travels");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Stations_LastStationId",
                table: "Travels");

            //migrationBuilder.DropIndex(
            //    name: "IX_Travels_FirstStationId",
            //    table: "Travels");

            //migrationBuilder.DropIndex(
            //    name: "IX_Travels_LastStationId",
            //    table: "Travels");

            migrationBuilder.DropColumn(
                name: "FirstStationId",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "LastStationId",
                table: "Travels");

            migrationBuilder.AddColumn<int>(
                name: "FirstStationId",
                table: "Routes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastStationId",
                table: "Routes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StationsNumber",
                table: "Routes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_FirstStationId",
                table: "Routes",
                column: "FirstStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_LastStationId",
                table: "Routes",
                column: "LastStationId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Stations_FirstStationId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Stations_LastStationId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_FirstStationId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_LastStationId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "FirstStationId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "LastStationId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "StationsNumber",
                table: "Routes");

            migrationBuilder.AddColumn<int>(
                name: "FirstStationId",
                table: "Travels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastStationId",
                table: "Travels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Travels_FirstStationId",
                table: "Travels",
                column: "FirstStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_LastStationId",
                table: "Travels",
                column: "LastStationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Stations_FirstStationId",
                table: "Travels",
                column: "FirstStationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Stations_LastStationId",
                table: "Travels",
                column: "LastStationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
