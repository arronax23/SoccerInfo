using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StatsLeagues_Image_nvarchar_no_limit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StatsLeagues_Name_Base64Image",
                table: "StatsLeagues");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StatsLeagues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Base64Image",
                table: "StatsLeagues",
                type: "nvarchar(max)",
                maxLength: -1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatsLeagues_Name",
                table: "StatsLeagues",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StatsLeagues_Name",
                table: "StatsLeagues");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StatsLeagues",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Base64Image",
                table: "StatsLeagues",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: -1,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatsLeagues_Name_Base64Image",
                table: "StatsLeagues",
                columns: new[] { "Name", "Base64Image" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [Base64Image] IS NOT NULL");
        }
    }
}
