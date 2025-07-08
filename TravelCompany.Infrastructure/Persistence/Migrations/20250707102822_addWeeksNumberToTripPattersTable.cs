using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addWeeksNumberToTripPattersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmptyWeeksNumber",
                table: "TripPatterns",
                type: "int",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "OccupiedWeeksNumber",
                table: "TripPatterns",
                type: "int",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmptyWeeksNumber",
                table: "TripPatterns");

            migrationBuilder.DropColumn(
                name: "OccupiedWeeksNumber",
                table: "TripPatterns");
        }
    }
}
