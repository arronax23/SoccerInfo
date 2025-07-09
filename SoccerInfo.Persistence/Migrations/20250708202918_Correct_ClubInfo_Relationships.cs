using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Correct_ClubInfo_Relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClubsInfos_ClubId",
                table: "ClubsInfos");

            migrationBuilder.DropIndex(
                name: "IX_ClubsInfos_TeamId",
                table: "ClubsInfos");

            migrationBuilder.CreateIndex(
                name: "IX_ClubsInfos_ClubId",
                table: "ClubsInfos",
                column: "ClubId",
                unique: true,
                filter: "[ClubId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClubsInfos_TeamId",
                table: "ClubsInfos",
                column: "TeamId",
                unique: true,
                filter: "[TeamId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClubsInfos_ClubId",
                table: "ClubsInfos");

            migrationBuilder.DropIndex(
                name: "IX_ClubsInfos_TeamId",
                table: "ClubsInfos");

            migrationBuilder.CreateIndex(
                name: "IX_ClubsInfos_ClubId",
                table: "ClubsInfos",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubsInfos_TeamId",
                table: "ClubsInfos",
                column: "TeamId");
        }
    }
}
