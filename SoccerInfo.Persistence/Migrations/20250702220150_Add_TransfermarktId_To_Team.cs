using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_TransfermarktId_To_Team : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransfermarktId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransfermarktId",
                table: "Teams");
        }
    }
}
