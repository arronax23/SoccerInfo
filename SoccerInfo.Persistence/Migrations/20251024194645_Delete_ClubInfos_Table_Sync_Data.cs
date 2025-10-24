using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Delete_ClubInfos_Table_Sync_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Transfers 
	                SET 
		                From_TeamId =ci.TeamId,
		                From_ClubId = ci.ClubId,
		                From_ClubTransfermarktId = ci.ClubTransfermarktId,
		                To_TeamId = ci2.TeamId,
		                To_ClubId = ci2.ClubId,
		                To_ClubTransfermarktId = ci2.ClubTransfermarktId
                FROM Transfers t
                JOIN ClubsInfos ci on t.FromId = ci.Id
                JOIN ClubsInfos ci2 on t.ToId = ci2.Id                
            ");
            migrationBuilder.DropColumn(
                name: "FromId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "ToId",
                table: "Transfers");

            migrationBuilder.DropTable(
                name: "ClubsInfos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromId",
                table: "Transfers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToId",
                table: "Transfers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClubsInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    ClubTransfermarktId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubsInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubsInfos_ClubsOverviews_ClubId",
                        column: x => x.ClubId,
                        principalTable: "ClubsOverviews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClubsInfos_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });
        }
    }
}
