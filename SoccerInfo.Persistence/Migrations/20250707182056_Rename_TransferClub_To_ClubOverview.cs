using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rename_TransferClub_To_ClubOverview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_TransferClubs_From_ClubId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_TransferClubs_To_ClubId",
                table: "Transfers");

            migrationBuilder.DropTable(
                name: "TransferClubs");

            migrationBuilder.CreateTable(
                name: "ClubsOverviews",
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
                    table.PrimaryKey("PK_ClubsOverviews", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_ClubsOverviews_From_ClubId",
                table: "Transfers",
                column: "From_ClubId",
                principalTable: "ClubsOverviews",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_ClubsOverviews_To_ClubId",
                table: "Transfers",
                column: "To_ClubId",
                principalTable: "ClubsOverviews",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_ClubsOverviews_From_ClubId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_ClubsOverviews_To_ClubId",
                table: "Transfers");

            migrationBuilder.DropTable(
                name: "ClubsOverviews");

            migrationBuilder.CreateTable(
                name: "TransferClubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubImageBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryImageBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransfermarktId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferClubs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_TransferClubs_From_ClubId",
                table: "Transfers",
                column: "From_ClubId",
                principalTable: "TransferClubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_TransferClubs_To_ClubId",
                table: "Transfers",
                column: "To_ClubId",
                principalTable: "TransferClubs",
                principalColumn: "Id");
        }
    }
}
