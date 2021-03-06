﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DoVuiHaiNao.Models;
using DoVuiHaiNao.Areas.WebManager.ViewModels.SinglePuzzleViewModels;

namespace DoVuiHaiNao.Data
{
    public class ApplicationDbContext : IdentityDbContext<Member>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<SinglePuzzleTag>().HasKey(t => new { t.SinglePuzzleID, t.TagID });
            builder.Entity<PostTag>().HasKey(t => new { t.PostID, t.TagID });

        }

        public DbSet<MultiPuzzle> MultiPuzzle { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Images> Images { get; set;}
        public DbSet<SinglePuzzleTag> SingPuzzleTag { get; set; }
        public DbSet<SinglePuzzle> SinglePuzzle { get; set; }
        public DbSet<HistoryAnswerPuzzle> HistoryAnswerPuzzle { get; set; }
        public DbSet<HistoryLikePuzzle> HistoryLikePuzzle { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostTag> PostTag { get; set; }
        public DbSet<DoVuiHaiNao.Areas.WebManager.ViewModels.SinglePuzzleViewModels.PublishDatetimeSinglePuzzleViewModel> PublishDatetimeSinglePuzzleViewModel { get; set; }
    }
}
