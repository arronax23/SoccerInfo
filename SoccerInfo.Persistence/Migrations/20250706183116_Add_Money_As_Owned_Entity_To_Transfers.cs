using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Money_As_Owned_Entity_To_Transfers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MarketValue",
                table: "Transfers",
                newName: "MarketValueNormalized");

            migrationBuilder.RenameColumn(
                name: "Fee",
                table: "Transfers",
                newName: "FeeNormalized");

            migrationBuilder.AddColumn<string>(
                name: "Fee_Suffix",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fee_Value",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketValue_Suffix",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketValue_Value",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fee_Suffix",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "Fee_Value",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "MarketValue_Suffix",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "MarketValue_Value",
                table: "Transfers");

            migrationBuilder.RenameColumn(
                name: "MarketValueNormalized",
                table: "Transfers",
                newName: "MarketValue");

            migrationBuilder.RenameColumn(
                name: "FeeNormalized",
                table: "Transfers",
                newName: "Fee");
        }
    }
}
