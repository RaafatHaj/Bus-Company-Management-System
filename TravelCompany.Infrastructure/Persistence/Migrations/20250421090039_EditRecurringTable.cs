using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditRecurringTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recurrings_Trips_TripId",
                table: "Recurrings");

            migrationBuilder.DropIndex(
                name: "IX_Recurrings_TripId",
                table: "Recurrings");

            migrationBuilder.DropColumn(
                name: "HasIrregularTrip",
                table: "Recurrings");

            migrationBuilder.DropColumn(
                name: "HasUnsignedTrip",
                table: "Recurrings");

			migrationBuilder.DropColumn(
			  name: "TripId",
			  table: "Recurrings");

			//migrationBuilder.RenameColumn(
   //             name: "TripId",
   //             table: "Recurrings",
   //             newName: "TripsNumber");

            migrationBuilder.RenameColumn(
                name: "Reschedule",
                table: "Recurrings",
                newName: "IsRecurring");

            migrationBuilder.RenameColumn(
                name: "LastTripDate",
                table: "Recurrings",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "FirstTripDate",
                table: "Recurrings",
                newName: "EndDate");

            migrationBuilder.AddColumn<int>(
                name: "IrregularTripsNumber",
                table: "Recurrings",
                type: "int",
                nullable: false,
                defaultValue: 0);

			migrationBuilder.AddColumn<int>(
            	name: "TripsNumber",
            	table: "Recurrings",
            	type: "int",
            	nullable: false);
            
			migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Recurrings",
                type: "int",
                nullable: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Recurrings",
                type: "time",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Recurrings_RouteId",
                table: "Recurrings",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recurrings_Routes_RouteId",
                table: "Recurrings",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "RouteId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recurrings_Routes_RouteId",
                table: "Recurrings");

            migrationBuilder.DropIndex(
                name: "IX_Recurrings_RouteId",
                table: "Recurrings");

            migrationBuilder.DropColumn(
                name: "IrregularTripsNumber",
                table: "Recurrings");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Recurrings");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Recurrings");

            migrationBuilder.RenameColumn(
                name: "TripsNumber",
                table: "Recurrings",
                newName: "TripId");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Recurrings",
                newName: "LastTripDate");

            migrationBuilder.RenameColumn(
                name: "IsRecurring",
                table: "Recurrings",
                newName: "Reschedule");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Recurrings",
                newName: "FirstTripDate");

            migrationBuilder.AddColumn<bool>(
                name: "HasIrregularTrip",
                table: "Recurrings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasUnsignedTrip",
                table: "Recurrings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Recurrings_TripId",
                table: "Recurrings",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recurrings_Trips_TripId",
                table: "Recurrings",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
