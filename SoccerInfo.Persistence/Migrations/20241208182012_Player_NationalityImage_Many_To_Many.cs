using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Player_NationalityImage_Many_To_Many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NationalityImages_Players_PlayerId",
                table: "NationalityImages");

            migrationBuilder.DropIndex(
                name: "IX_NationalityImages_PlayerId",
                table: "NationalityImages");

            migrationBuilder.AddColumn<int>(
                name: "NationalityImageId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NationalityImagePlayer",
                columns: table => new
                {
                    NationalityImagesId = table.Column<int>(type: "int", nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalityImagePlayer", x => new { x.NationalityImagesId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_NationalityImagePlayer_NationalityImages_NationalityImagesId",
                        column: x => x.NationalityImagesId,
                        principalTable: "NationalityImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NationalityImagePlayer_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NationalityImagePlayer_PlayersId",
                table: "NationalityImagePlayer",
                column: "PlayersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NationalityImagePlayer");

            migrationBuilder.DropColumn(
                name: "NationalityImageId",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_NationalityImages_PlayerId",
                table: "NationalityImages",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityImages_Players_PlayerId",
                table: "NationalityImages",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
