using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransfermarktScraperWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class NullableBase64_MultipleNationalityImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalityImageBase64",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "FaceImageBase64",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NationalityImages",
                columns: table => new
                {
                    NationalityImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Base64Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalityImages", x => x.NationalityImageId);
                    table.ForeignKey(
                        name: "FK_NationalityImages_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NationalityImages_PlayerId",
                table: "NationalityImages",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NationalityImages");

            migrationBuilder.DropColumn(
                name: "FaceImageBase64",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "NationalityImageBase64",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
