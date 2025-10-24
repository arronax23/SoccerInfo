using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ClubInfo_as_OwnedEntity_To_Transfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_ClubsInfos_FromId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_ClubsInfos_ToId",
                table: "Transfers");

            //migrationBuilder.DropTable(
            //    name: "ClubsInfos");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_FromId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_ToId",
                table: "Transfers");

            migrationBuilder.AddColumn<int>(
                name: "From_ClubId",
                table: "Transfers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "From_ClubTransfermarktId",
                table: "Transfers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "From_TeamId",
                table: "Transfers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "To_ClubId",
                table: "Transfers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "To_ClubTransfermarktId",
                table: "Transfers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "To_TeamId",
                table: "Transfers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_From_ClubId",
                table: "Transfers",
                column: "From_ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_From_TeamId",
                table: "Transfers",
                column: "From_TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_To_ClubId",
                table: "Transfers",
                column: "To_ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_To_TeamId",
                table: "Transfers",
                column: "To_TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_ClubsOverviews_From_ClubId",
                table: "Transfers",
                column: "From_ClubId",
                principalTable: "ClubsOverviews",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_ClubsOverviews_To_ClubId",
                table: "Transfers",
                column: "To_ClubId",
                principalTable: "ClubsOverviews",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Teams_From_TeamId",
                table: "Transfers",
                column: "From_TeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Teams_To_TeamId",
                table: "Transfers",
                column: "To_TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_ClubsOverviews_From_ClubId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_ClubsOverviews_To_ClubId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Teams_From_TeamId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Teams_To_TeamId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_From_ClubId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_From_TeamId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_To_ClubId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_To_TeamId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "From_ClubId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "From_ClubTransfermarktId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "From_TeamId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "To_ClubId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "To_ClubTransfermarktId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "To_TeamId",
                table: "Transfers");

            //migrationBuilder.CreateTable(
            //    name: "ClubsInfos",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ClubId = table.Column<int>(type: "int", nullable: true),
            //        TeamId = table.Column<int>(type: "int", nullable: true),
            //        ClubTransfermarktId = table.Column<int>(type: "int", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ClubsInfos", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ClubsInfos_ClubsOverviews_ClubId",
            //            column: x => x.ClubId,
            //            principalTable: "ClubsOverviews",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_ClubsInfos_Teams_TeamId",
            //            column: x => x.TeamId,
            //            principalTable: "Teams",
            //            principalColumn: "Id");
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_FromId",
                table: "Transfers",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ToId",
                table: "Transfers",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubsInfos_ClubId",
                table: "ClubsInfos",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubsInfos_TeamId",
                table: "ClubsInfos",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_ClubsInfos_FromId",
                table: "Transfers",
                column: "FromId",
                principalTable: "ClubsInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_ClubsInfos_ToId",
                table: "Transfers",
                column: "ToId",
                principalTable: "ClubsInfos",
                principalColumn: "Id");
        }
    }
}
