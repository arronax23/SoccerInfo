using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_PlayerStatistics_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age_Years = table.Column<int>(type: "int", nullable: false),
                    Age_Months = table.Column<int>(type: "int", nullable: false),
                    Age_Days = table.Column<int>(type: "int", nullable: false),
                    TotalGoals = table.Column<int>(type: "int", nullable: true),
                    TotalGoalsAndAssists = table.Column<int>(type: "int", nullable: true),
                    TotalAssists = table.Column<int>(type: "int", nullable: true),
                    TotalGoalsConceded = table.Column<int>(type: "int", nullable: true),
                    TotalCleanSheets = table.Column<int>(type: "int", nullable: true),
                    MarketValueNormalized = table.Column<float>(type: "real", nullable: true),
                    LastMarkeValueProgress = table.Column<float>(type: "real", nullable: true),
                    Height = table.Column<float>(type: "real", nullable: true),
                    ContractPeriod_Years = table.Column<int>(type: "int", nullable: true),
                    ContractPeriod_Months = table.Column<int>(type: "int", nullable: true),
                    ContractPeriod_Days = table.Column<int>(type: "int", nullable: true),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerStatistics_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics",
                column: "PlayerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerStatistics");
        }
    }
}
