using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fix_NationalityImagePlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NationalityImagePlayer_NationalityImages_NationalityImagesId",
                table: "NationalityImagePlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_NationalityImagePlayer_Players_PlayersId",
                table: "NationalityImagePlayer");

            migrationBuilder.RenameColumn(
                name: "PlayersId",
                table: "NationalityImagePlayer",
                newName: "PlayerId");

            migrationBuilder.RenameColumn(
                name: "NationalityImagesId",
                table: "NationalityImagePlayer",
                newName: "NationalityImageId");

            migrationBuilder.RenameIndex(
                name: "IX_NationalityImagePlayer_PlayersId",
                table: "NationalityImagePlayer",
                newName: "IX_NationalityImagePlayer_PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityImagePlayer_NationalityImages_NationalityImageId",
                table: "NationalityImagePlayer",
                column: "NationalityImageId",
                principalTable: "NationalityImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityImagePlayer_Players_PlayerId",
                table: "NationalityImagePlayer",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NationalityImagePlayer_NationalityImages_NationalityImageId",
                table: "NationalityImagePlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_NationalityImagePlayer_Players_PlayerId",
                table: "NationalityImagePlayer");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "NationalityImagePlayer",
                newName: "PlayersId");

            migrationBuilder.RenameColumn(
                name: "NationalityImageId",
                table: "NationalityImagePlayer",
                newName: "NationalityImagesId");

            migrationBuilder.RenameIndex(
                name: "IX_NationalityImagePlayer_PlayerId",
                table: "NationalityImagePlayer",
                newName: "IX_NationalityImagePlayer_PlayersId");

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityImagePlayer_NationalityImages_NationalityImagesId",
                table: "NationalityImagePlayer",
                column: "NationalityImagesId",
                principalTable: "NationalityImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityImagePlayer_Players_PlayersId",
                table: "NationalityImagePlayer",
                column: "PlayersId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
