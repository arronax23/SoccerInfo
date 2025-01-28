using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_HashCode_Column_To_StatsLeagues_With_UniqeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_HashCode",
                table: "StatsLeagues",
                type: "nvarchar(450)",
                nullable: false,
                computedColumnSql: "CONVERT(varchar(64), HASHBYTES('SHA2_256', CONCAT(Name, Base64Image)), 1)");

            migrationBuilder.CreateIndex(
                name: "IX_StatsLeagues__HashCode",
                table: "StatsLeagues",
                column: "_HashCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StatsLeagues__HashCode",
                table: "StatsLeagues");

            migrationBuilder.DropColumn(
                name: "_HashCode",
                table: "StatsLeagues");
        }
    }
}
