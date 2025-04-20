using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameEntityToTravelStation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledTravelDetails");

            migrationBuilder.CreateTable(
                name: "TravelStations",
                columns: table => new
                {
                    ScheduledTravelId = table.Column<int>(type: "int", nullable: false),
                    StationOrder = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ArrvalDateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    BookedSeates = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelStations", x => new { x.ScheduledTravelId, x.StationOrder });
                    table.ForeignKey(
                        name: "FK_TravelStations_ScheduledTravels_ScheduledTravelId",
                        column: x => x.ScheduledTravelId,
                        principalTable: "ScheduledTravels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TravelStations_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelStations_StationId",
                table: "TravelStations",
                column: "StationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelStations");

            migrationBuilder.CreateTable(
                name: "ScheduledTravelDetails",
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
                    table.PrimaryKey("PK_ScheduledTravelDetails", x => new { x.ScheduledTravelId, x.StationOrder });
                    table.ForeignKey(
                        name: "FK_ScheduledTravelDetails_ScheduledTravels_ScheduledTravelId",
                        column: x => x.ScheduledTravelId,
                        principalTable: "ScheduledTravels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledTravelDetails_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTravelDetails_StationId",
                table: "ScheduledTravelDetails",
                column: "StationId");
        }
    }
}
