using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Delete_ImageSvgBase64_From_CountryFlag_Lookup_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSvgBase64",
                table: "CountryFlags_Lookup");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageSvgBase64",
                table: "CountryFlags_Lookup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
