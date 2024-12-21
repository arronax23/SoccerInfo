using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_CountryFlag_Lookup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NationalityImagePlayer_NationalityImages_NationalityImageId",
                table: "NationalityImagePlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NationalityImages",
                table: "NationalityImages");

            migrationBuilder.RenameTable(
                name: "NationalityImages",
                newName: "Nationalities");

            migrationBuilder.AddColumn<int>(
                name: "CountryFlagId",
                table: "Nationalities",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nationalities",
                table: "Nationalities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CountryFlags_Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TwoLetterISOCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageSvgBase64 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryFlags_Lookup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nationalities_CountryFlagId",
                table: "Nationalities",
                column: "CountryFlagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nationalities_CountryFlags_Lookup_CountryFlagId",
                table: "Nationalities",
                column: "CountryFlagId",
                principalTable: "CountryFlags_Lookup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityImagePlayer_Nationalities_NationalityImageId",
                table: "NationalityImagePlayer",
                column: "NationalityImageId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nationalities_CountryFlags_Lookup_CountryFlagId",
                table: "Nationalities");

            migrationBuilder.DropForeignKey(
                name: "FK_NationalityImagePlayer_Nationalities_NationalityImageId",
                table: "NationalityImagePlayer");

            migrationBuilder.DropTable(
                name: "CountryFlags_Lookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nationalities",
                table: "Nationalities");

            migrationBuilder.DropIndex(
                name: "IX_Nationalities_CountryFlagId",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "CountryFlagId",
                table: "Nationalities");

            migrationBuilder.RenameTable(
                name: "Nationalities",
                newName: "NationalityImages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NationalityImages",
                table: "NationalityImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NationalityImagePlayer_NationalityImages_NationalityImageId",
                table: "NationalityImagePlayer",
                column: "NationalityImageId",
                principalTable: "NationalityImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
