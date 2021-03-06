﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DoVuiHaiNao.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    About = table.Column<string>(maxLength: 1000, nullable: true),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: true),
                    DateofBirth = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    Facebook = table.Column<string>(maxLength: 100, nullable: true),
                    FullName = table.Column<string>(maxLength: 100, nullable: true),
                    GooglePlus = table.Column<string>(maxLength: 100, nullable: true),
                    IdentityFacebook = table.Column<string>(maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Linkedin = table.Column<string>(maxLength: 100, nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    Picture65x65 = table.Column<string>(maxLength: 100, nullable: true),
                    PictureBig = table.Column<string>(maxLength: 100, nullable: true),
                    PictureSmall = table.Column<string>(maxLength: 100, nullable: true),
                    Score = table.Column<int>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(maxLength: 50, nullable: true),
                    Twitter = table.Column<string>(maxLength: 100, nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    Website = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "HistoryAnswerPuzzle",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<string>(maxLength: 1, nullable: true),
                    Approved = table.Column<string>(maxLength: 1, nullable: true),
                    AuthorID = table.Column<string>(nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsMultiPuzzle = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(maxLength: 200, nullable: true),
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
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<string>(maxLength: 1, nullable: true),
                    Approved = table.Column<string>(maxLength: 1, nullable: true),
                    AuthorID = table.Column<string>(nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsMultiPuzzle = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(maxLength: 200, nullable: true),
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
                    ALT = table.Column<string>(maxLength: 150, nullable: true),
                    Active = table.Column<string>(maxLength: 1, nullable: true),
                    Approved = table.Column<string>(maxLength: 1, nullable: true),
                    AuthorID = table.Column<string>(nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Note = table.Column<string>(maxLength: 200, nullable: true),
                    Pic640x480 = table.Column<string>(maxLength: 200, nullable: true),
                    PicFull = table.Column<string>(maxLength: 200, nullable: true),
                    Title = table.Column<string>(maxLength: 150, nullable: true),
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultiPuzzle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<string>(maxLength: 1, nullable: true),
                    Approved = table.Column<string>(maxLength: 1, nullable: true),
                    AuthorID = table.Column<string>(nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    ImageID = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Level = table.Column<float>(nullable: false),
                    Like = table.Column<int>(nullable: false),
                    Note = table.Column<string>(maxLength: 200, nullable: true),
                    NumberQuestion = table.Column<int>(nullable: false),
                    Slug = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
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
                name: "Post",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<string>(maxLength: 1, nullable: true),
                    Approved = table.Column<string>(maxLength: 1, nullable: true),
                    AuthorID = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageID = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Like = table.Column<int>(nullable: false),
                    Note = table.Column<string>(maxLength: 200, nullable: true),
                    Slug = table.Column<string>(maxLength: 50, nullable: true),
                    Title = table.Column<string>(maxLength: 150, nullable: true),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    Views = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Post_AspNetUsers_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_Images_ImageID",
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
                    Active = table.Column<string>(maxLength: 1, nullable: true),
                    AnswerA = table.Column<string>(nullable: true),
                    AnswerB = table.Column<string>(nullable: true),
                    AnswerC = table.Column<string>(nullable: true),
                    AnswerD = table.Column<string>(nullable: true),
                    Approved = table.Column<string>(maxLength: 1, nullable: true),
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
                    MultiPuzzleID = table.Column<int>(nullable: true),
                    Note = table.Column<string>(maxLength: 200, nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(maxLength: 50, nullable: true),
                    Title = table.Column<string>(maxLength: 150, nullable: true),
                    Type = table.Column<int>(nullable: false),
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
                name: "PostTag",
                columns: table => new
                {
                    PostID = table.Column<int>(nullable: false),
                    TagID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostID, x.TagID });
                    table.ForeignKey(
                        name: "FK_PostTag_Post_PostID",
                        column: x => x.PostID,
                        principalTable: "Post",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tag_TagID",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<string>(maxLength: 1, nullable: true),
                    Approved = table.Column<string>(maxLength: 1, nullable: true),
                    AuthorID = table.Column<string>(nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Like = table.Column<int>(nullable: false),
                    MMultiPuzzle = table.Column<int>(nullable: false),
                    MultiPuzzleID = table.Column<int>(nullable: true),
                    Note = table.Column<string>(maxLength: 200, nullable: true),
                    PostID = table.Column<int>(nullable: true),
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
                        name: "FK_Comment_Post_PostID",
                        column: x => x.PostID,
                        principalTable: "Post",
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
                name: "IX_Comment_AuthorID",
                table: "Comment",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_MultiPuzzleID",
                table: "Comment",
                column: "MultiPuzzleID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostID",
                table: "Comment",
                column: "PostID");

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
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MultiPuzzle_AuthorID",
                table: "MultiPuzzle",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_MultiPuzzle_ImageID",
                table: "MultiPuzzle",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_AuthorID",
                table: "Post",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_ImageID",
                table: "Post",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagID",
                table: "PostTag",
                column: "TagID");

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

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");
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
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "SingPuzzleTag");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "SinglePuzzle");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MultiPuzzle");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
