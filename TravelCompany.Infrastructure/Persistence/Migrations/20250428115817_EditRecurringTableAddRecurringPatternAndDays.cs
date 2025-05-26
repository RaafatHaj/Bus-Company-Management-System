using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditRecurringTableAddRecurringPatternAndDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IrregularTripsNumber",
                table: "Recurrings",
                newName: "UnassignedTripsNumber");

            migrationBuilder.AddColumn<int>(
                name: "PatternDays",
                table: "Recurrings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecurringDays",
                table: "Recurrings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecurringPattern",
                table: "Recurrings",
                type: "int",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatternDays",
                table: "Recurrings");

            migrationBuilder.DropColumn(
                name: "RecurringDays",
                table: "Recurrings");

            migrationBuilder.DropColumn(
                name: "RecurringPattern",
                table: "Recurrings");

            migrationBuilder.RenameColumn(
                name: "UnassignedTripsNumber",
                table: "Recurrings",
                newName: "IrregularTripsNumber");
        }
    }
}
