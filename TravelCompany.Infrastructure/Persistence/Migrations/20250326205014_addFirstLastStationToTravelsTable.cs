using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addFirstLastStationToTravelsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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


            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Stations_FirstStationId",
                table: "Travels",
                column: "FirstStationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Stations_LastStationId",
                table: "Travels",
                column: "LastStationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Stations_FirstStationId",
                table: "Travels");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Stations_LastStationId",
                table: "Travels");

            migrationBuilder.DropIndex(
                name: "IX_Travels_FirstStationId",
                table: "Travels");

            migrationBuilder.DropIndex(
                name: "IX_Travels_LastStationId",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "FirstStationId",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "LastStationId",
                table: "Travels");
        }
    }
}
