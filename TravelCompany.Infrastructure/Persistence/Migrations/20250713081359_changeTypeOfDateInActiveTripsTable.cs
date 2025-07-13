using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeTypeOfDateInActiveTripsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualArrivalDateTime",
                table: "ActiveTrips");

            migrationBuilder.DropColumn(
                name: "ActualDepartureDateTime",
                table: "ActiveTrips");

            migrationBuilder.DropColumn(
                name: "ArrivalDateTime",
                table: "ActiveTrips");

            migrationBuilder.DropColumn(
                name: "DepartureDateTime",
                table: "ActiveTrips");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ActualArrivalTime",
                table: "ActiveTrips",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ActualDepartureTime",
                table: "ActiveTrips",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ArrivalTime",
                table: "ActiveTrips",
                type: "time",
                nullable: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DepartureTime",
                table: "ActiveTrips",
                type: "time",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualArrivalTime",
                table: "ActiveTrips");

            migrationBuilder.DropColumn(
                name: "ActualDepartureTime",
                table: "ActiveTrips");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "ActiveTrips");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "ActiveTrips");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualArrivalDateTime",
                table: "ActiveTrips",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualDepartureDateTime",
                table: "ActiveTrips",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalDateTime",
                table: "ActiveTrips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureDateTime",
                table: "ActiveTrips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
