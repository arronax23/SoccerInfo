using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rename_NationalityImagesPlayer_To_NationalityPlayer_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NationalityPlayer_Nationalities_NationalityImageId",
                table: "NationalityPlayer");

            migrationBuilder.RenameColumn(
                name: "NationalityImageId",
                table: "NationalityPlayer",
                newName: "NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityPlayer_Nationalities_NationalityId",
                table: "NationalityPlayer",
                column: "NationalityId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NationalityPlayer_Nationalities_NationalityId",
                table: "NationalityPlayer");

            migrationBuilder.RenameColumn(
                name: "NationalityId",
                table: "NationalityPlayer",
                newName: "NationalityImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityPlayer_Nationalities_NationalityImageId",
                table: "NationalityPlayer",
                column: "NationalityImageId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
