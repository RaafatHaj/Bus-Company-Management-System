using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateAndTimeInScheduledTravels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "ScheduledTravels");

            migrationBuilder.DropColumn(
                name: "TravelTime",
                table: "ScheduledTravels");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "ScheduledTravelDetails");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "ScheduledTravelDetails",
                newName: "ArrvalDateAndTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAndTime",
                table: "ScheduledTravels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAndTime",
                table: "ScheduledTravels");

            migrationBuilder.RenameColumn(
                name: "ArrvalDateAndTime",
                table: "ScheduledTravelDetails",
                newName: "Date");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ScheduledTravels",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TravelTime",
                table: "ScheduledTravels",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ArrivalTime",
                table: "ScheduledTravelDetails",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
