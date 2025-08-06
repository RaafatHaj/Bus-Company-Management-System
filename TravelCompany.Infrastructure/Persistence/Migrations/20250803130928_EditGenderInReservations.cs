using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditGenderInReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Trips_ScheduledTravelId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PersonGendor",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ScheduledTravelId",
                table: "Reservations",
                newName: "TripId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ScheduledTravelId",
                table: "Reservations",
                newName: "IX_Reservations_TripId");

            migrationBuilder.AddColumn<int>(
                name: "PersonGender",
                table: "Reservations",
                type: "int",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Trips_TripId",
                table: "Reservations",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Trips_TripId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PersonGender",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "Reservations",
                newName: "ScheduledTravelId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_TripId",
                table: "Reservations",
                newName: "IX_Reservations_ScheduledTravelId");

            migrationBuilder.AddColumn<bool>(
                name: "PersonGendor",
                table: "Reservations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Trips_ScheduledTravelId",
                table: "Reservations",
                column: "ScheduledTravelId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
