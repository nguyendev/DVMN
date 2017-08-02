using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DVMN.Migrations
{
    public partial class History : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryAnswerPuzzle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<string>(nullable: true),
                    Approved = table.Column<string>(nullable: true),
                    AuthorID = table.Column<string>(nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsMultiPuzzle = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PuzzleID = table.Column<int>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryAnswerPuzzle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HistoryAnswerPuzzle_AspNetUsers_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryAnswerPuzzle_AuthorID",
                table: "HistoryAnswerPuzzle",
                column: "AuthorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryAnswerPuzzle");
        }
    }
}
