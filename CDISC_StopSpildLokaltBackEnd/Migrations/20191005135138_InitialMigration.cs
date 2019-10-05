using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CDISC_StopSpildLokaltBackEnd.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Identification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedTs = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    CreatedTs = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Volunteer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedTs = table.Column<DateTime>(nullable: false),
                    AuthToken = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Postcode = table.Column<string>(nullable: true),
                    Phonenumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    VolunteerType = table.Column<int>(nullable: false),
                    OrganizationName = table.Column<string>(nullable: true),
                    IdentificationId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Volunteer_Identification_IdentificationId",
                        column: x => x.IdentificationId,
                        principalTable: "Identification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Volunteer_Organization_OrganizationName",
                        column: x => x.OrganizationName,
                        principalTable: "Organization",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedTs = table.Column<DateTime>(nullable: false),
                    Postcode = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    TeamName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FacebookUrl = table.Column<string>(nullable: true),
                    OrganizationName = table.Column<string>(nullable: true),
                    ContactPersonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Volunteer_ContactPersonId",
                        column: x => x.ContactPersonId,
                        principalTable: "Volunteer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_Organization_OrganizationName",
                        column: x => x.OrganizationName,
                        principalTable: "Organization",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Team_ContactPersonId",
                table: "Team",
                column: "ContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_OrganizationName",
                table: "Team",
                column: "OrganizationName");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteer_IdentificationId",
                table: "Volunteer",
                column: "IdentificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Volunteer_OrganizationName",
                table: "Volunteer",
                column: "OrganizationName");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteer_TeamId",
                table: "Volunteer",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteer_Team_TeamId",
                table: "Volunteer",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Volunteer_ContactPersonId",
                table: "Team");

            migrationBuilder.DropTable(
                name: "Volunteer");

            migrationBuilder.DropTable(
                name: "Identification");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Organization");
        }
    }
}
