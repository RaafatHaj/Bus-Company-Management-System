using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addNextPrevAssigmentForTripAssigmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NextAssigmenStartDateTime",
                table: "TripAssignments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PervAssigmentEndDateTime",
                table: "TripAssignments",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextAssigmenStartDateTime",
                table: "TripAssignments");

            migrationBuilder.DropColumn(
                name: "PervAssigmentEndDateTime",
                table: "TripAssignments");
        }
    }
}
