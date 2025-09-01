using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Migrate_CountryFlagBase64_in_Leagues_To_Images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "CountryFlagId",
                table: "Leagues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_CountryFlagId",
                table: "Leagues",
                column: "CountryFlagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Images_CountryFlagId",
                table: "Leagues",
                column: "CountryFlagId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.Sql(@"
              INSERT INTO Images ([Base64], MimeType)
                SELECT CountryFlagBase64, 'image/jpeg'
                FROM Leagues;"
            );

            migrationBuilder.Sql(@"
               UPDATE L
               SET CountryFlagId = I.Id
               FROM Leagues L
               INNER JOIN Images I
                  ON I.Base64 = L.CountryFlagBase64;"
            );

            migrationBuilder.DropColumn(
                name: "CountryFlagBase64",
                table: "Leagues");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Images_CountryFlagId",
                table: "Leagues");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_CountryFlagId",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "CountryFlagId",
                table: "Leagues");

            migrationBuilder.AddColumn<string>(
                name: "CountryFlagBase64",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
