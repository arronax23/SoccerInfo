using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_MarketValueNormalized_Column_To_Players_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "MarketValueNormalized",
                table: "Players",
                type: "real",
                nullable: false,
                computedColumnSql: "\r\n                CASE \r\n                    WHEN MarketValueUnit = 'm' THEN MarketValue * 1000000\r\n                    WHEN MarketValueUnit = 'k' THEN MarketValue * 1000\r\n                    ELSE 0\r\n                END",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketValueNormalized",
                table: "Players");
        }
    }
}
