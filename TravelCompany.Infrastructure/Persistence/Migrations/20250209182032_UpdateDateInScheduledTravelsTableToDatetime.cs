using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateInScheduledTravelsTableToDatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
             name: "Date",
             table: "ScheduledTravels",
             type: "datetime2",
             nullable: false,
             oldClrType: typeof(DateTime),
             oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
          name: "Date",
          table: "ScheduledTravels",
          type: "date",
          nullable: false,
          oldClrType: typeof(DateTime),
          oldType: "datetime2");
        }
    }
}
