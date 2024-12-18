using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fix_columns_in_NationalityImagePlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NationalityImagePlayer_NationalityImageId",
                table: "NationalityImagePlayer",
                column: "NationalityImageId");

            migrationBuilder.CreateIndex(
                name: "IX_NationalityImagePlayer_PlayerId",
                table: "NationalityImagePlayer",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityImagePlayer_NationalityImages_NationalityImageId",
                table: "NationalityImagePlayer",
                column: "NationalityImageId",
                principalTable: "NationalityImages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityImagePlayer_Players_PlayerId",
                table: "NationalityImagePlayer",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
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

            migrationBuilder.DropIndex(
                name: "IX_NationalityImagePlayer_NationalityImageId",
                table: "NationalityImagePlayer");

            migrationBuilder.DropIndex(
                name: "IX_NationalityImagePlayer_PlayerId",
                table: "NationalityImagePlayer");
        }
    }
}
