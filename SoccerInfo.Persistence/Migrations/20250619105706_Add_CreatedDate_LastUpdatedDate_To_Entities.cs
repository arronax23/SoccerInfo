using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_CreatedDate_LastUpdatedDate_To_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Teams",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Teams",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "StatsLeagues",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "StatsLeagues",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Socials",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Socials",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PlayerStatistics",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "PlayerStatistics",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Players",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Players",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PlayerCharacteristics",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "PlayerCharacteristics",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "OutfieldPlayerStats",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "OutfieldPlayerStats",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Nationalities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Nationalities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MarketValueChanges",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "MarketValueChanges",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Leagues",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Leagues",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "GoalKeeperStats",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "GoalKeeperStats",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "StatsLeagues");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "StatsLeagues");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Socials");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Socials");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PlayerStatistics");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "PlayerStatistics");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PlayerCharacteristics");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "PlayerCharacteristics");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "OutfieldPlayerStats");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "OutfieldPlayerStats");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MarketValueChanges");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "MarketValueChanges");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "GoalKeeperStats");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "GoalKeeperStats");
        }
    }
}
