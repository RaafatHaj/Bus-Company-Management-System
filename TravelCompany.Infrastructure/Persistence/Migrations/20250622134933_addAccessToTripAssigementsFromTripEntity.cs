using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addAccessToTripAssigementsFromTripEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripAssignments_Trips_ScheduledTripId",
                table: "TripAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TripAssignments_ScheduledTripId",
                table: "TripAssignments");

            migrationBuilder.RenameColumn(
                name: "ScheduledTripId",
                table: "TripAssignments",
                newName: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripAssignments_TripId",
                table: "TripAssignments",
                column: "TripId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TripAssignments_Trips_TripId",
                table: "TripAssignments",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripAssignments_Trips_TripId",
                table: "TripAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TripAssignments_TripId",
                table: "TripAssignments");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "TripAssignments",
                newName: "ScheduledTripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripAssignments_ScheduledTripId",
                table: "TripAssignments",
                column: "ScheduledTripId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripAssignments_Trips_ScheduledTripId",
                table: "TripAssignments",
                column: "ScheduledTripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
