using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Old_Image_Columns_Regarding_Player_Team_League : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamImageBase64",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "FaceImageBase64",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LeagueImageBase64",
                table: "Leagues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamImageBase64",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaceImageBase64",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeagueImageBase64",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
