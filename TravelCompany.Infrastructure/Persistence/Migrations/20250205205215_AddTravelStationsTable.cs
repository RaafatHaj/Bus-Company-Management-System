using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTravelStationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTravels_Routes_RouteId",
                table: "ScheduledTravels");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTravels_RouteId",
                table: "ScheduledTravels");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "ScheduledTravels");

            migrationBuilder.CreateTable(
                name: "TrvelStations",
                columns: table => new
                {
                    TravelId = table.Column<int>(type: "int", nullable: false),
                    StationOrder = table.Column<int>(type: "int", nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DefaultArrivalTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrvelStations", x => new { x.TravelId, x.StationOrder });
                    table.ForeignKey(
                        name: "FK_TrvelStations_Travels_TravelId",
                        column: x => x.TravelId,
                        principalTable: "Travels",
                        principalColumn: "TravelId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrvelStations");

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "ScheduledTravels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTravels_RouteId",
                table: "ScheduledTravels",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTravels_Routes_RouteId",
                table: "ScheduledTravels",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "RouteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
