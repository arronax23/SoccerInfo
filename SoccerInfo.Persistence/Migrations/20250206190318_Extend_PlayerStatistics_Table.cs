using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Extend_PlayerStatistics_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "MarketValue",
                table: "PlayerStatistics",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketValueUnit",
                table: "PlayerStatistics",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketValue",
                table: "PlayerStatistics");

            migrationBuilder.DropColumn(
                name: "MarketValueUnit",
                table: "PlayerStatistics");
        }
    }
}
