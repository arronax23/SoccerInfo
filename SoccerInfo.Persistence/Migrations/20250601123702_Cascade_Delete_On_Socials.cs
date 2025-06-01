using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Cascade_Delete_On_Socials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Socials_PlayerCharacteristics_PlayerCharacteristicId",
                table: "Socials");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerCharacteristicId",
                table: "Socials",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Socials_PlayerCharacteristics_PlayerCharacteristicId",
                table: "Socials",
                column: "PlayerCharacteristicId",
                principalTable: "PlayerCharacteristics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Socials_PlayerCharacteristics_PlayerCharacteristicId",
                table: "Socials");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerCharacteristicId",
                table: "Socials",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Socials_PlayerCharacteristics_PlayerCharacteristicId",
                table: "Socials",
                column: "PlayerCharacteristicId",
                principalTable: "PlayerCharacteristics",
                principalColumn: "Id");
        }
    }
}
