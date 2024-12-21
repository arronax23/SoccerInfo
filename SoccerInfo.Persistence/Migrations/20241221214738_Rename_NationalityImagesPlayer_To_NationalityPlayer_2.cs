using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rename_NationalityImagesPlayer_To_NationalityPlayer_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "NationalityPlayer");

            migrationBuilder.RenameIndex(
                name: "IX_NationalityImage_PlayerId",
                table: "NationalityPlayer",
                newName: "IX_NationalityPlayer_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NationalityPlayer",
                table: "NationalityPlayer",
                columns: new[] { "NationalityImageId", "PlayerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityPlayer_Nationalities_NationalityImageId",
                table: "NationalityPlayer",
                column: "NationalityImageId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityPlayer_Players_PlayerId",
                table: "NationalityPlayer",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NationalityPlayer_Nationalities_NationalityImageId",
                table: "NationalityPlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_NationalityPlayer_Players_PlayerId",
                table: "NationalityPlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NationalityPlayer",
                table: "NationalityPlayer");

            migrationBuilder.RenameTable(
                name: "NationalityPlayer",
                newName: "NationalityImage");

            migrationBuilder.RenameIndex(
                name: "IX_NationalityPlayer_PlayerId",
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
    }
}
