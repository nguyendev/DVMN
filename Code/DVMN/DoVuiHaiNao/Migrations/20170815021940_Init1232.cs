using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DoVuiHaiNao.Migrations
{
    public partial class Init1232 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDT",
                table: "Tag",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PublishDatetimeSinglePuzzleViewModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PublishDT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishDatetimeSinglePuzzleViewModel", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublishDatetimeSinglePuzzleViewModel");

            migrationBuilder.DropColumn(
                name: "CreateDT",
                table: "Tag");
        }
    }
}
