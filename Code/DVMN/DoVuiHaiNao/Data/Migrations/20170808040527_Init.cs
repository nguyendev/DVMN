using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DoVuiHaiNao.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateofBirth",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GooglePlus",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityFacebook",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Linkedin",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture65x65",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureBig",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureSmall",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "AspNetUsers",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "HistoryLikePuzzle",
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
                    table.PrimaryKey("PK_HistoryLikePuzzle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HistoryLikePuzzle_AspNetUsers_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ALT = table.Column<string>(nullable: true),
                    Active = table.Column<string>(nullable: true),
                    Approved = table.Column<string>(nullable: true),
                    AuthorID = table.Column<string>(nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Pic250x188 = table.Column<string>(nullable: true),
                    PicFull = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UpdateDT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Images_AspNetUsers_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Slug = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MultiPuzzle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<string>(nullable: true),
                    Approved = table.Column<string>(nullable: true),
                    AuthorID = table.Column<string>(nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    ImageID = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Level = table.Column<float>(nullable: false),
                    Like = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    NumberQuestion = table.Column<int>(nullable: false),
                    Slug = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    Views = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiPuzzle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MultiPuzzle_AspNetUsers_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MultiPuzzle_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SinglePuzzle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<string>(nullable: true),
                    AnswerA = table.Column<string>(nullable: true),
                    AnswerB = table.Column<string>(nullable: true),
                    AnswerC = table.Column<string>(nullable: true),
                    AnswerD = table.Column<string>(nullable: true),
                    Approved = table.Column<string>(nullable: true),
                    AuthorID = table.Column<string>(nullable: true),
                    Correct = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageID = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsMMultiPuzzle = table.Column<bool>(nullable: false),
                    IsYesNo = table.Column<bool>(nullable: false),
                    Level = table.Column<float>(nullable: false),
                    Like = table.Column<int>(nullable: false),
                    MMultiPuzzleID = table.Column<int>(nullable: true),
                    MultiPuzzleID = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    Views = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinglePuzzle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SinglePuzzle_AspNetUsers_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SinglePuzzle_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SinglePuzzle_MultiPuzzle_MultiPuzzleID",
                        column: x => x.MultiPuzzleID,
                        principalTable: "MultiPuzzle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<string>(nullable: true),
                    Approved = table.Column<string>(nullable: true),
                    AuthorID = table.Column<string>(nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Like = table.Column<int>(nullable: false),
                    MMultiPuzzle = table.Column<int>(nullable: false),
                    MultiPuzzleID = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    SinglePuzzleID = table.Column<int>(nullable: true),
                    UpdateDT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comment_AspNetUsers_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_MultiPuzzle_MultiPuzzleID",
                        column: x => x.MultiPuzzleID,
                        principalTable: "MultiPuzzle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_SinglePuzzle_SinglePuzzleID",
                        column: x => x.SinglePuzzleID,
                        principalTable: "SinglePuzzle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SingPuzzleTag",
                columns: table => new
                {
                    SinglePuzzleID = table.Column<int>(nullable: false),
                    TagID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingPuzzleTag", x => new { x.SinglePuzzleID, x.TagID });
                    table.ForeignKey(
                        name: "FK_SingPuzzleTag_SinglePuzzle_SinglePuzzleID",
                        column: x => x.SinglePuzzleID,
                        principalTable: "SinglePuzzle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingPuzzleTag_Tag_TagID",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AuthorID",
                table: "Comment",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_MultiPuzzleID",
                table: "Comment",
                column: "MultiPuzzleID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_SinglePuzzleID",
                table: "Comment",
                column: "SinglePuzzleID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryAnswerPuzzle_AuthorID",
                table: "HistoryAnswerPuzzle",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryLikePuzzle_AuthorID",
                table: "HistoryLikePuzzle",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AuthorID",
                table: "Images",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_MultiPuzzle_AuthorID",
                table: "MultiPuzzle",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_MultiPuzzle_ImageID",
                table: "MultiPuzzle",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_SinglePuzzle_AuthorID",
                table: "SinglePuzzle",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_SinglePuzzle_ImageID",
                table: "SinglePuzzle",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_SinglePuzzle_MultiPuzzleID",
                table: "SinglePuzzle",
                column: "MultiPuzzleID");

            migrationBuilder.CreateIndex(
                name: "IX_SingPuzzleTag_TagID",
                table: "SingPuzzleTag",
                column: "TagID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "HistoryAnswerPuzzle");

            migrationBuilder.DropTable(
                name: "HistoryLikePuzzle");

            migrationBuilder.DropTable(
                name: "SingPuzzleTag");

            migrationBuilder.DropTable(
                name: "SinglePuzzle");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "MultiPuzzle");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "About",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateofBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GooglePlus",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdentityFacebook",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Linkedin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Picture65x65",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PictureBig",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PictureSmall",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
