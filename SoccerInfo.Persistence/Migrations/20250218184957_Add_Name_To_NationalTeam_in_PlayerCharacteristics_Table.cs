using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Name_To_NationalTeam_in_PlayerCharacteristics_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalTeam_Name",
                table: "PlayerCharacteristics",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalTeam_Name",
                table: "PlayerCharacteristics");
        }
    }
}
