using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addPercentageTORecurringsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Weeks",
                table: "Weeks");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Weeks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "Recurrings",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weeks",
                table: "Weeks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Weeks_RecurringId",
                table: "Weeks",
                column: "RecurringId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Weeks",
                table: "Weeks");

            migrationBuilder.DropIndex(
                name: "IX_Weeks_RecurringId",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Recurrings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weeks",
                table: "Weeks",
                column: "RecurringId");
        }
    }
}
