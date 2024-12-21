using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rename_NationalityImagesPlayer_To_NationalityPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NationalityImagePlayer_Nationalities_NationalityImageId",
                table: "NationalityImagePlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_NationalityImagePlayer_Players_PlayerId",
                table: "NationalityImagePlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NationalityImagePlayer",
                table: "NationalityImagePlayer");

            migrationBuilder.RenameTable(
                name: "NationalityImagePlayer",
                newName: "NationalityImage");

            migrationBuilder.RenameIndex(
                name: "IX_NationalityImagePlayer_PlayerId",
                table: "NationalityImage",
                newName: "IX_NationalityImage_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NationalityImage",
                table: "NationalityImage",
                columns: new[] { "NationalityImageId", "PlayerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityImage_Nationalities_NationalityImageId",
                table: "NationalityImage",
                column: "NationalityImageId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityImage_Players_PlayerId",
                table: "NationalityImage",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NationalityImage_Nationalities_NationalityImageId",
                table: "NationalityImage");

            migrationBuilder.DropForeignKey(
                name: "FK_NationalityImage_Players_PlayerId",
                table: "NationalityImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NationalityImage",
                table: "NationalityImage");

            migrationBuilder.RenameTable(
                name: "NationalityImage",
                newName: "NationalityImagePlayer");

            migrationBuilder.RenameIndex(
                name: "IX_NationalityImage_PlayerId",
                table: "NationalityImagePlayer",
                newName: "IX_NationalityImagePlayer_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NationalityImagePlayer",
                table: "NationalityImagePlayer",
                columns: new[] { "NationalityImageId", "PlayerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityImagePlayer_Nationalities_NationalityImageId",
                table: "NationalityImagePlayer",
                column: "NationalityImageId",
                principalTable: "Nationalities",
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
    }
}
