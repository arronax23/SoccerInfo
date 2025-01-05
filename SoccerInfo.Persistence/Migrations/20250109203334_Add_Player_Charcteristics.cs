using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Player_Charcteristics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerCharacteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrithPlace_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrithPlace_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalTeam_Caps = table.Column<int>(type: "int", nullable: true),
                    NationalTeam_Goals = table.Column<int>(type: "int", nullable: true),
                    NationalTeam_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeadingFoot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<float>(type: "real", nullable: false),
                    ClubJoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerCharacteristics_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalKeeperStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalsConceded = table.Column<int>(type: "int", nullable: true),
                    CleanSheets = table.Column<int>(type: "int", nullable: true),
                    PlayerCharacteristicId = table.Column<int>(type: "int", nullable: false),
                    League = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeagueBase64Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatchesPlayed = table.Column<int>(type: "int", nullable: true),
                    MinutesPlayed = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalKeeperStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoalKeeperStats_PlayerCharacteristics_PlayerCharacteristicId",
                        column: x => x.PlayerCharacteristicId,
                        principalTable: "PlayerCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutfieldPlayerStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Goals = table.Column<int>(type: "int", nullable: true),
                    Assists = table.Column<int>(type: "int", nullable: true),
                    PlayerCharacteristicId = table.Column<int>(type: "int", nullable: false),
                    League = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeagueBase64Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatchesPlayed = table.Column<int>(type: "int", nullable: true),
                    MinutesPlayed = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutfieldPlayerStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutfieldPlayerStats_PlayerCharacteristics_PlayerCharacteristicId",
                        column: x => x.PlayerCharacteristicId,
                        principalTable: "PlayerCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Socials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerCharacteristicId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Socials_PlayerCharacteristics_PlayerCharacteristicId",
                        column: x => x.PlayerCharacteristicId,
                        principalTable: "PlayerCharacteristics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoalKeeperStats_PlayerCharacteristicId",
                table: "GoalKeeperStats",
                column: "PlayerCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_OutfieldPlayerStats_PlayerCharacteristicId",
                table: "OutfieldPlayerStats",
                column: "PlayerCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacteristics_PlayerId",
                table: "PlayerCharacteristics",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Socials_PlayerCharacteristicId",
                table: "Socials",
                column: "PlayerCharacteristicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoalKeeperStats");

            migrationBuilder.DropTable(
                name: "OutfieldPlayerStats");

            migrationBuilder.DropTable(
                name: "Socials");

            migrationBuilder.DropTable(
                name: "PlayerCharacteristics");
        }
    }
}
