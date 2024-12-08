using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Columns_NationalityImagePlayer_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NationalityImagesId",
                table: "NationalityImagePlayer",
                newName: "NationalityImageId");

            migrationBuilder.RenameColumn(
                name: "PlayersId",
                table: "NationalityImagePlayer",
                newName: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NationalityImageId",
                table: "NationalityImages",
                newName: "NationalityImagesId");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "NationalityImages",
                newName: "PlayersId");
        }
    }
}
