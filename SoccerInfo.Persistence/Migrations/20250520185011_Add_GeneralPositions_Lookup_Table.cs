using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_GeneralPositions_Lookup_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeneralPositionId",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GeneralPositions_Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralPositions_Lookup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_GeneralPositionId",
                table: "Players",
                column: "GeneralPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GeneralPositions_Lookup_GeneralPositionId",
                table: "Players",
                column: "GeneralPositionId",
                principalTable: "GeneralPositions_Lookup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GeneralPositions_Lookup_GeneralPositionId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "GeneralPositions_Lookup");

            migrationBuilder.DropIndex(
                name: "IX_Players_GeneralPositionId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GeneralPositionId",
                table: "Players");
        }
    }
}
