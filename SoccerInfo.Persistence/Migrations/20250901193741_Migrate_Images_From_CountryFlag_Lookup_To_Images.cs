using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Migrate_Images_From_CountryFlag_Lookup_To_Images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "CountryFlags_Lookup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CountryFlags_Lookup_ImageId",
                table: "CountryFlags_Lookup",
                column: "ImageId");

            migrationBuilder.Sql(@"
               INSERT INTO Images ([Base64], MimeType)
                SELECT ImageSvgBase64, 'image/svg+xml'
                FROM CountryFlags_Lookup");

            migrationBuilder.Sql(@"
                   UPDATE L
                   SET ImageId = I.Id
                   FROM CountryFlags_Lookup L
                   INNER JOIN Images I
                      ON I.Base64 = L.ImageSvgBase64");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryFlags_Lookup_Images_ImageId",
                table: "CountryFlags_Lookup",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryFlags_Lookup_Images_ImageId",
                table: "CountryFlags_Lookup");

            migrationBuilder.DropIndex(
                name: "IX_CountryFlags_Lookup_ImageId",
                table: "CountryFlags_Lookup");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "CountryFlags_Lookup");
        }
    }
}
