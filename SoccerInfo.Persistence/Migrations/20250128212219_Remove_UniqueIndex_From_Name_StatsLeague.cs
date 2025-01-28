using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Remove_UniqueIndex_From_Name_StatsLeague : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StatsLeagues_Name",
                table: "StatsLeagues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StatsLeagues_Name",
                table: "StatsLeagues",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
