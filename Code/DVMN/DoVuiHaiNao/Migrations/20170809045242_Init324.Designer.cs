using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DoVuiHaiNao.Data;

namespace DoVuiHaiNao.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170809045242_Init324")]
    partial class Init324
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DoVuiHaiNao.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Active")
                        .HasMaxLength(1);

                    b.Property<string>("Approved")
                        .HasMaxLength(1);

                    b.Property<string>("AuthorID");

                    b.Property<DateTime?>("CreateDT");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Like");

                    b.Property<int>("MMultiPuzzle");

                    b.Property<int?>("MultiPuzzleID");

                    b.Property<string>("Note")
                        .HasMaxLength(200);

                    b.Property<int?>("SinglePuzzleID");

                    b.Property<DateTime?>("UpdateDT");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("MultiPuzzleID");

                    b.HasIndex("SinglePuzzleID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.HistoryAnswerPuzzle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Active")
                        .HasMaxLength(1);

                    b.Property<string>("Approved")
                        .HasMaxLength(1);

                    b.Property<string>("AuthorID");

                    b.Property<DateTime?>("CreateDT");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsMultiPuzzle");

                    b.Property<string>("Note")
                        .HasMaxLength(200);

                    b.Property<int>("PuzzleID");

                    b.Property<DateTime?>("UpdateDT");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.ToTable("HistoryAnswerPuzzle");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.HistoryLikePuzzle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Active")
                        .HasMaxLength(1);

                    b.Property<string>("Approved")
                        .HasMaxLength(1);

                    b.Property<string>("AuthorID");

                    b.Property<DateTime?>("CreateDT");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsMultiPuzzle");

                    b.Property<string>("Note")
                        .HasMaxLength(200);

                    b.Property<int>("PuzzleID");

                    b.Property<DateTime?>("UpdateDT");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.ToTable("HistoryLikePuzzle");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.Images", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ALT")
                        .HasMaxLength(150);

                    b.Property<string>("Active")
                        .HasMaxLength(1);

                    b.Property<string>("Approved")
                        .HasMaxLength(1);

                    b.Property<string>("AuthorID");

                    b.Property<DateTime?>("CreateDT");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("Note")
                        .HasMaxLength(200);

                    b.Property<string>("Pic640x480")
                        .HasMaxLength(200);

                    b.Property<string>("PicFull")
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .HasMaxLength(150);

                    b.Property<DateTime?>("UpdateDT");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.Member", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About")
                        .HasMaxLength(1000);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("DateofBirth")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Facebook")
                        .HasMaxLength(100);

                    b.Property<string>("FullName")
                        .HasMaxLength(100);

                    b.Property<string>("GooglePlus")
                        .HasMaxLength(100);

                    b.Property<string>("IdentityFacebook")
                        .HasMaxLength(100);

                    b.Property<string>("Linkedin")
                        .HasMaxLength(100);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Picture65x65")
                        .HasMaxLength(100);

                    b.Property<string>("PictureBig")
                        .HasMaxLength(100);

                    b.Property<string>("PictureSmall")
                        .HasMaxLength(100);

                    b.Property<int>("Score");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Slug")
                        .HasMaxLength(50);

                    b.Property<string>("Twitter")
                        .HasMaxLength(100);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("Website")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.MultiPuzzle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Active")
                        .HasMaxLength(1);

                    b.Property<string>("Approved")
                        .HasMaxLength(1);

                    b.Property<string>("AuthorID");

                    b.Property<DateTime?>("CreateDT");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int?>("ImageID");

                    b.Property<bool>("IsDeleted");

                    b.Property<float>("Level");

                    b.Property<int>("Like");

                    b.Property<string>("Note")
                        .HasMaxLength(200);

                    b.Property<int>("NumberQuestion");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdateDT");

                    b.Property<int>("Views");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("ImageID");

                    b.ToTable("MultiPuzzle");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.SinglePuzzle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Active")
                        .HasMaxLength(1);

                    b.Property<string>("AnswerA");

                    b.Property<string>("AnswerB");

                    b.Property<string>("AnswerC");

                    b.Property<string>("AnswerD");

                    b.Property<string>("Approved")
                        .HasMaxLength(1);

                    b.Property<string>("AuthorID");

                    b.Property<int>("Correct");

                    b.Property<DateTime?>("CreateDT");

                    b.Property<string>("Description");

                    b.Property<int?>("ImageID");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsMMultiPuzzle");

                    b.Property<bool>("IsYesNo");

                    b.Property<float>("Level");

                    b.Property<int>("Like");

                    b.Property<int?>("MMultiPuzzleID");

                    b.Property<int?>("MultiPuzzleID");

                    b.Property<string>("Note")
                        .HasMaxLength(200);

                    b.Property<string>("Reason");

                    b.Property<string>("Slug")
                        .HasMaxLength(50);

                    b.Property<string>("Title")
                        .HasMaxLength(150);

                    b.Property<DateTime?>("UpdateDT");

                    b.Property<int>("Views");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("ImageID");

                    b.HasIndex("MultiPuzzleID");

                    b.ToTable("SinglePuzzle");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.SinglePuzzleTag", b =>
                {
                    b.Property<int>("SinglePuzzleID");

                    b.Property<int>("TagID");

                    b.HasKey("SinglePuzzleID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("SingPuzzleTag");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.Tag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Slug");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.Comment", b =>
                {
                    b.HasOne("DoVuiHaiNao.Models.Member", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID");

                    b.HasOne("DoVuiHaiNao.Models.MultiPuzzle", "MultiPuzzle")
                        .WithMany("Comment")
                        .HasForeignKey("MultiPuzzleID");

                    b.HasOne("DoVuiHaiNao.Models.SinglePuzzle")
                        .WithMany("Comment")
                        .HasForeignKey("SinglePuzzleID");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.HistoryAnswerPuzzle", b =>
                {
                    b.HasOne("DoVuiHaiNao.Models.Member", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.HistoryLikePuzzle", b =>
                {
                    b.HasOne("DoVuiHaiNao.Models.Member", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.Images", b =>
                {
                    b.HasOne("DoVuiHaiNao.Models.Member", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.MultiPuzzle", b =>
                {
                    b.HasOne("DoVuiHaiNao.Models.Member", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID");

                    b.HasOne("DoVuiHaiNao.Models.Images", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.SinglePuzzle", b =>
                {
                    b.HasOne("DoVuiHaiNao.Models.Member", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID");

                    b.HasOne("DoVuiHaiNao.Models.Images", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID");

                    b.HasOne("DoVuiHaiNao.Models.MultiPuzzle", "MultiPuzzle")
                        .WithMany("SinglePuzzleDetails")
                        .HasForeignKey("MultiPuzzleID");
                });

            modelBuilder.Entity("DoVuiHaiNao.Models.SinglePuzzleTag", b =>
                {
                    b.HasOne("DoVuiHaiNao.Models.SinglePuzzle", "SinglePuzzle")
                        .WithMany()
                        .HasForeignKey("SinglePuzzleID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DoVuiHaiNao.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DoVuiHaiNao.Models.Member")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DoVuiHaiNao.Models.Member")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DoVuiHaiNao.Models.Member")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
