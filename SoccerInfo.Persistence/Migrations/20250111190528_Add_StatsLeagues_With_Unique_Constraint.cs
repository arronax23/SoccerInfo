using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_StatsLeagues_With_Unique_Constraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "League",
                table: "OutfieldPlayerStats");

            migrationBuilder.DropColumn(
                name: "LeagueBase64Image",
                table: "OutfieldPlayerStats");

            migrationBuilder.DropColumn(
                name: "League",
                table: "GoalKeeperStats");

            migrationBuilder.DropColumn(
                name: "LeagueBase64Image",
                table: "GoalKeeperStats");

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "OutfieldPlayerStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "GoalKeeperStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StatsLeagues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Base64Image = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsLeagues", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutfieldPlayerStats_LeagueId",
                table: "OutfieldPlayerStats",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalKeeperStats_LeagueId",
                table: "GoalKeeperStats",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_StatsLeagues_Name_Base64Image",
                table: "StatsLeagues",
                columns: new[] { "Name", "Base64Image" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [Base64Image] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalKeeperStats_StatsLeagues_LeagueId",
                table: "GoalKeeperStats",
                column: "LeagueId",
                principalTable: "StatsLeagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutfieldPlayerStats_StatsLeagues_LeagueId",
                table: "OutfieldPlayerStats",
                column: "LeagueId",
                principalTable: "StatsLeagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoalKeeperStats_StatsLeagues_LeagueId",
                table: "GoalKeeperStats");

            migrationBuilder.DropForeignKey(
                name: "FK_OutfieldPlayerStats_StatsLeagues_LeagueId",
                table: "OutfieldPlayerStats");

            migrationBuilder.DropTable(
                name: "StatsLeagues");

            migrationBuilder.DropIndex(
                name: "IX_OutfieldPlayerStats_LeagueId",
                table: "OutfieldPlayerStats");

            migrationBuilder.DropIndex(
                name: "IX_GoalKeeperStats_LeagueId",
                table: "GoalKeeperStats");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "OutfieldPlayerStats");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "GoalKeeperStats");

            migrationBuilder.AddColumn<string>(
                name: "League",
                table: "OutfieldPlayerStats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeagueBase64Image",
                table: "OutfieldPlayerStats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "League",
                table: "GoalKeeperStats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeagueBase64Image",
                table: "GoalKeeperStats",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
