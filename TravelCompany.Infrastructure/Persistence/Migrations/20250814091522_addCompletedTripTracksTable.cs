using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addCompletedTripTracksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveTrips");

            migrationBuilder.CreateTable(
                name: "ActiveTripTracks",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false),
                    StationOrder = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousStation = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    NexttStation = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PlannedArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlannedDepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualDepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LateMinutes = table.Column<int>(type: "int", nullable: false),
                    TripStatus = table.Column<int>(type: "int", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    RouteName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    EstimatedDistance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveTripTracks", x => new { x.TripId, x.StationOrder });
                    table.ForeignKey(
                        name: "FK_ActiveTripTracks_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActiveTripTracks_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompletedTripTracks",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false),
                    StationOrder = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousStation = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    NexttStation = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PlannedArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlannedDepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualDepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LateMinutes = table.Column<int>(type: "int", nullable: false),
                    TripStatus = table.Column<int>(type: "int", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    RouteName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    EstimatedDistance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedTripTracks", x => new { x.TripId, x.StationOrder });
                    table.ForeignKey(
                        name: "FK_CompletedTripTracks_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompletedTripTracks_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveTripTracks_RouteId",
                table: "ActiveTripTracks",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedTripTracks_RouteId",
                table: "CompletedTripTracks",
                column: "RouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveTripTracks");

            migrationBuilder.DropTable(
                name: "CompletedTripTracks");

            migrationBuilder.CreateTable(
                name: "ActiveTrips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false),
                    StationOrder = table.Column<int>(type: "int", nullable: false),
                    ActualArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualDepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedDistance = table.Column<int>(type: "int", nullable: false),
                    NexttStation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousStation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TripStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveTrips", x => new { x.TripId, x.StationOrder });
                    table.ForeignKey(
                        name: "FK_ActiveTrips_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
