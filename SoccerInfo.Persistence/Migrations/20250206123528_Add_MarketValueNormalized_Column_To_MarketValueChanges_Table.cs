using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_MarketValueNormalized_Column_To_MarketValueChanges_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "MarketValueNormalized",
                table: "Players",
                type: "real",
                nullable: true,
                computedColumnSql: "\r\n                CASE \r\n                    WHEN MarketValueUnit IS NULL OR MarketValue IS NULL THEN NULL\r\n                    WHEN MarketValueUnit = 'm' THEN MarketValue * 1000000\r\n                    WHEN MarketValueUnit = 'k' THEN MarketValue * 1000\r\n                    ELSE NULL\r\n                END",
                stored: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldComputedColumnSql: "\r\n                CASE \r\n                    WHEN MarketValueUnit = 'm' THEN MarketValue * 1000000\r\n                    WHEN MarketValueUnit = 'k' THEN MarketValue * 1000\r\n                    ELSE 0\r\n                END",
                oldStored: true);

            migrationBuilder.AddColumn<float>(
                name: "MarketValueNormalized",
                table: "MarketValueChanges",
                type: "real",
                nullable: true,
                computedColumnSql: "\r\n                CASE \r\n                    WHEN MarketValueUnit IS NULL OR MarketValue IS NULL THEN NULL\r\n                    WHEN MarketValueUnit = 'm' THEN MarketValue * 1000000\r\n                    WHEN MarketValueUnit = 'k' THEN MarketValue * 1000\r\n                    ELSE NULL\r\n                END",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketValueNormalized",
                table: "MarketValueChanges");

            migrationBuilder.AlterColumn<float>(
                name: "MarketValueNormalized",
                table: "Players",
                type: "real",
                nullable: false,
                computedColumnSql: "\r\n                CASE \r\n                    WHEN MarketValueUnit = 'm' THEN MarketValue * 1000000\r\n                    WHEN MarketValueUnit = 'k' THEN MarketValue * 1000\r\n                    ELSE 0\r\n                END",
                stored: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true,
                oldComputedColumnSql: "\r\n                CASE \r\n                    WHEN MarketValueUnit IS NULL OR MarketValue IS NULL THEN NULL\r\n                    WHEN MarketValueUnit = 'm' THEN MarketValue * 1000000\r\n                    WHEN MarketValueUnit = 'k' THEN MarketValue * 1000\r\n                    ELSE NULL\r\n                END",
                oldStored: true);
        }
    }
}
