using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GeneralPositionId_ForeignKey_NotNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GeneralPositions_Lookup_GeneralPositionId",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "GeneralPositionId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GeneralPositions_Lookup_GeneralPositionId",
                table: "Players",
                column: "GeneralPositionId",
                principalTable: "GeneralPositions_Lookup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GeneralPositions_Lookup_GeneralPositionId",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "GeneralPositionId",
                table: "Players",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GeneralPositions_Lookup_GeneralPositionId",
                table: "Players",
                column: "GeneralPositionId",
                principalTable: "GeneralPositions_Lookup",
                principalColumn: "Id");
        }
    }
}
