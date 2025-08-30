using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Images_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogoId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FaceImageId",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LogoId",
                table: "Leagues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Base64 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LogoId",
                table: "Teams",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_FaceImageId",
                table: "Players",
                column: "FaceImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_LogoId",
                table: "Leagues",
                column: "LogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Images_LogoId",
                table: "Leagues",
                column: "LogoId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Images_FaceImageId",
                table: "Players",
                column: "FaceImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Images_LogoId",
                table: "Teams",
                column: "LogoId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Images_LogoId",
                table: "Leagues");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Images_FaceImageId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Images_LogoId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Teams_LogoId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Players_FaceImageId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_LogoId",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "FaceImageId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "Leagues");
        }
    }
}
