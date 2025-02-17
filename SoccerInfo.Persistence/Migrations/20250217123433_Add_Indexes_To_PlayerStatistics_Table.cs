using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Indexes_To_PlayerStatistics_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_Age_TotalDays",
                table: "PlayerStatistics",
                column: "Age_TotalDays");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_ContractPeriod_TotalDays",
                table: "PlayerStatistics",
                column: "ContractPeriod_TotalDays");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_Height",
                table: "PlayerStatistics",
                column: "Height");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_MarketValueNormalized",
                table: "PlayerStatistics",
                column: "MarketValueNormalized");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_TotalAssists",
                table: "PlayerStatistics",
                column: "TotalAssists");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_TotalCleanSheets",
                table: "PlayerStatistics",
                column: "TotalCleanSheets");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_TotalGoals",
                table: "PlayerStatistics",
                column: "TotalGoals");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_TotalGoalsAndAssists",
                table: "PlayerStatistics",
                column: "TotalGoalsAndAssists");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_TotalGoalsConceded",
                table: "PlayerStatistics",
                column: "TotalGoalsConceded");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_Age_TotalDays",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_ContractPeriod_TotalDays",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_Height",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_MarketValueNormalized",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_TotalAssists",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_TotalCleanSheets",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_TotalGoals",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_TotalGoalsAndAssists",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_TotalGoalsConceded",
                table: "PlayerStatistics");
        }
    }
}
