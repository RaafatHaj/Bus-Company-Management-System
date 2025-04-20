using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTravelStationsTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrvelStations_Travels_TravelId",
                table: "TrvelStations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrvelStations",
                table: "TrvelStations");

            migrationBuilder.RenameTable(
                name: "TrvelStations",
                newName: "TravelStations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelStations",
                table: "TravelStations",
                columns: new[] { "TravelId", "StationOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_TravelStations_Travels_TravelId",
                table: "TravelStations",
                column: "TravelId",
                principalTable: "Travels",
                principalColumn: "TravelId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelStations_Travels_TravelId",
                table: "TravelStations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelStations",
                table: "TravelStations");

            migrationBuilder.RenameTable(
                name: "TravelStations",
                newName: "TrvelStations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrvelStations",
                table: "TrvelStations",
                columns: new[] { "TravelId", "StationOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_TrvelStations_Travels_TravelId",
                table: "TrvelStations",
                column: "TravelId",
                principalTable: "Travels",
                principalColumn: "TravelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
