using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDaysTableAndUpdateTravels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReSchedule",
                table: "Travels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleDuration",
                table: "Travels",
                type: "tinyint",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleType",
                table: "Travels",
                type: "tinyint",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    day = table.Column<byte>(type: "tinyint", nullable: false),
                    TravelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => new { x.TravelId, x.day });
                    table.ForeignKey(
                        name: "FK_Days_Travels_TravelId",
                        column: x => x.TravelId,
                        principalTable: "Travels",
                        principalColumn: "TravelId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropColumn(
                name: "ReSchedule",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "ScheduleDuration",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "ScheduleType",
                table: "Travels");
        }
    }
}
