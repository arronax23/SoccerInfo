using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Extend_DateRange_Owned_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age_TotalDays",
                table: "PlayerStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContractPeriod_TotalDays",
                table: "PlayerStatistics",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age_TotalDays",
                table: "PlayerStatistics");

            migrationBuilder.DropColumn(
                name: "ContractPeriod_TotalDays",
                table: "PlayerStatistics");
        }
    }
}
