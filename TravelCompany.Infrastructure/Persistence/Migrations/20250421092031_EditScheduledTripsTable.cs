using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditScheduledTripsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTrips_Trips_TripId",
                table: "ScheduledTrips");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "ScheduledTrips",
                newName: "RouteId");

            migrationBuilder.RenameColumn(
                name: "ReverseScheduledTripId",
                table: "ScheduledTrips",
                newName: "MainTripId");

			//migrationBuilder.RenameColumn(
			//    name: "DateAndTime",
			//    table: "ScheduledTrips",
			//    newName: "Date");

			migrationBuilder.DropColumn(
	            name: "DateAndTime",
	            table: "ScheduledTrips");

			migrationBuilder.RenameIndex(
                name: "IX_ScheduledTrips_TripId",
                table: "ScheduledTrips",
                newName: "IX_ScheduledTrips_RouteId");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "ScheduledTrips",
                type: "time",
                nullable: false);

			migrationBuilder.AddColumn<DateTime>(
			    name: "Date",
			    table: "ScheduledTrips",
			    type: "date",
			    nullable: false);

			migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTrips_Routes_RouteId",
                table: "ScheduledTrips",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "RouteId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTrips_Routes_RouteId",
                table: "ScheduledTrips");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "ScheduledTrips");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "ScheduledTrips",
                newName: "TripId");

            migrationBuilder.RenameColumn(
                name: "MainTripId",
                table: "ScheduledTrips",
                newName: "ReverseScheduledTripId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "ScheduledTrips",
                newName: "DateAndTime");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledTrips_RouteId",
                table: "ScheduledTrips",
                newName: "IX_ScheduledTrips_TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTrips_Trips_TripId",
                table: "ScheduledTrips",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
