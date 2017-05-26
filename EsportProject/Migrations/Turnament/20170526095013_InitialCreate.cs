using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EsportProject.Migrations.Turnament
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Name = table.Column<string>(nullable: true),
                    ShorntenedName = table.Column<string>(nullable: true),
                    Spillere = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamID);
                });

            migrationBuilder.CreateTable(
                name: "Turnament",
                columns: table => new
                {
                    TurnamentID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Name = table.Column<string>(nullable: true),
                    Slutdate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnament", x => x.TurnamentID);
                });

            migrationBuilder.CreateTable(
                name: "TeamStanding",
                columns: table => new
                {
                    TeamStandingID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    LostMatches = table.Column<int>(nullable: false),
                    TeamID = table.Column<int>(nullable: true),
                    TurnamentID = table.Column<int>(nullable: true),
                    WonMatches = table.Column<int>(nullable: false),
                    drawMatches = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStanding", x => x.TeamStandingID);
                    table.ForeignKey(
                        name: "FK_TeamStanding_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamStanding_Turnament_TurnamentID",
                        column: x => x.TurnamentID,
                        principalTable: "Turnament",
                        principalColumn: "TurnamentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamStanding_TeamID",
                table: "TeamStanding",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStanding_TurnamentID",
                table: "TeamStanding",
                column: "TurnamentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamStanding");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Turnament");
        }
    }
}
