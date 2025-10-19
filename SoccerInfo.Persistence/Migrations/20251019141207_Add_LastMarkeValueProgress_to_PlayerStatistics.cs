using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_LastMarkeValueProgress_to_PlayerStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "LastMarkeValueProgressNormalized",
                table: "PlayerStatistics",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastMarkeValueProgressUnit",
                table: "PlayerStatistics",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastMarkeValueProgressNormalized",
                table: "PlayerStatistics");

            migrationBuilder.DropColumn(
                name: "LastMarkeValueProgressUnit",
                table: "PlayerStatistics");
        }
    }
}
