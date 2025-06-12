using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editStartDateAndTimeTypeInAssigmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropColumn(
             name: "StartDateAndTime",
             table: "TripAssignments");



			migrationBuilder.AddColumn<DateTime>(
				name: "StartDateAndTime",
				table: "TripAssignments",
				type: "datetime2",
				nullable: false);
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
