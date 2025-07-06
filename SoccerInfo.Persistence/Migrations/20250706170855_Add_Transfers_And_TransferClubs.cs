using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Transfers_And_TransferClubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferClubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransfermarktId = table.Column<int>(type: "int", nullable: false),
                    ClubImageBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryImageBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferClubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerTransferMarktId = table.Column<int>(type: "int", nullable: false),
                    From_ClubTransfermarktId = table.Column<int>(type: "int", nullable: false),
                    From_TeamId = table.Column<int>(type: "int", nullable: true),
                    From_ClubId = table.Column<int>(type: "int", nullable: true),
                    To_ClubTransfermarktId = table.Column<int>(type: "int", nullable: false),
                    To_TeamId = table.Column<int>(type: "int", nullable: true),
                    To_ClubId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Season = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fee = table.Column<int>(type: "int", nullable: true),
                    MarketValue = table.Column<int>(type: "int", nullable: true),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transfers_Teams_From_TeamId",
                        column: x => x.From_TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transfers_Teams_To_TeamId",
                        column: x => x.To_TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transfers_TransferClubs_From_ClubId",
                        column: x => x.From_ClubId,
                        principalTable: "TransferClubs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transfers_TransferClubs_To_ClubId",
                        column: x => x.To_ClubId,
                        principalTable: "TransferClubs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_From_ClubId",
                table: "Transfers",
                column: "From_ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_From_TeamId",
                table: "Transfers",
                column: "From_TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_PlayerId",
                table: "Transfers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_To_ClubId",
                table: "Transfers",
                column: "To_ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_To_TeamId",
                table: "Transfers",
                column: "To_TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "TransferClubs");
        }
    }
}
