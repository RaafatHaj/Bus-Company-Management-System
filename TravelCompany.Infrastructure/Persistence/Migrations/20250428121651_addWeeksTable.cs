using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addWeeksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weeks",
                columns: table => new
                {
                    RecurringId = table.Column<int>(type: "int", nullable: false),
                    RecurringType = table.Column<int>(type: "int", nullable: false),
                    RecurringDays = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TripsNumber = table.Column<int>(type: "int", nullable: false),
                    UnassignedTripsNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeks", x => x.RecurringId);
                    table.ForeignKey(
                        name: "FK_Weeks_Recurrings_RecurringId",
                        column: x => x.RecurringId,
                        principalTable: "Recurrings",
                        principalColumn: "RecurringId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weeks");
        }
    }
}
