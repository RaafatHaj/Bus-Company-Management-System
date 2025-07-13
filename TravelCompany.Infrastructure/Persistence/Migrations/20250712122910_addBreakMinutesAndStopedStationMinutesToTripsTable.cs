using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addBreakMinutesAndStopedStationMinutesToTripsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBreak",
                table: "Trips",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StationNextToBreak",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StationStopMinutes",
                table: "Trips",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBreak",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "StationNextToBreak",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "StationStopMinutes",
                table: "Trips");
        }
    }
}
