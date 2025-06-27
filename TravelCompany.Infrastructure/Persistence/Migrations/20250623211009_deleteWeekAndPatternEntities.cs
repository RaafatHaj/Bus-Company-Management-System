using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class deleteWeekAndPatternEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weeks");

            migrationBuilder.DropTable(
                name: "Recurrings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recurrings",
                columns: table => new
                {
                    RecurringId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    PatternDays = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RecurringDays = table.Column<int>(type: "int", nullable: false),
                    RecurringPattern = table.Column<int>(type: "int", nullable: false),
                    RecurringType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    TripsNumber = table.Column<int>(type: "int", nullable: false),
                    UnassignedTripsNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recurrings", x => x.RecurringId);
                    table.ForeignKey(
                        name: "FK_Recurrings_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecurringId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecurringDays = table.Column<int>(type: "int", nullable: false),
                    RecurringType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TripsNumber = table.Column<int>(type: "int", nullable: false),
                    UnassignedTripsNumber = table.Column<int>(type: "int", nullable: false),
                    WeekOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weeks_Recurrings_RecurringId",
                        column: x => x.RecurringId,
                        principalTable: "Recurrings",
                        principalColumn: "RecurringId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recurrings_RouteId",
                table: "Recurrings",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Weeks_RecurringId",
                table: "Weeks",
                column: "RecurringId");
        }
    }
}
