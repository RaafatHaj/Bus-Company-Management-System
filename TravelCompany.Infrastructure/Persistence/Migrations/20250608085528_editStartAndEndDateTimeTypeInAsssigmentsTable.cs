using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editStartAndEndDateTimeTypeInAsssigmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TripAssignments");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TripAssignments");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "TripAssignments",
                newName: "StartDateAndTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateAndTime",
                table: "TripAssignments",
                type: "datetime2",
                nullable: false );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDateAndTime",
                table: "TripAssignments");

            migrationBuilder.RenameColumn(
                name: "StartDateAndTime",
                table: "TripAssignments",
                newName: "Date");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "TripAssignments",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "TripAssignments",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
