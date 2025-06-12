using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addStopedStationIdToAssigmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StopedStationId",
                table: "TripAssignments",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_TripAssignments_StopedStationId",
                table: "TripAssignments",
                column: "StopedStationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripAssignments_Stations_StopedStationId",
                table: "TripAssignments",
                column: "StopedStationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripAssignments_Stations_StopedStationId",
                table: "TripAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TripAssignments_StopedStationId",
                table: "TripAssignments");

            migrationBuilder.DropColumn(
                name: "StopedStationId",
                table: "TripAssignments");
        }
    }
}
