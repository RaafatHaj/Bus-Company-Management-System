using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addHasIrregularToRecurrings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasIrregularTrip",
                table: "Trips");

            migrationBuilder.AddColumn<bool>(
                name: "HasIrregularTrip",
                table: "Recurrings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasIrregularTrip",
                table: "Recurrings");

            migrationBuilder.AddColumn<bool>(
                name: "HasIrregularTrip",
                table: "Trips",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
