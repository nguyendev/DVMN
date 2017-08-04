using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DVMN.Models;

namespace DVMN.Data
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

        }

        public DbSet<MultiPuzzle> MultiPuzzle { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Tag> Tag { get; set; }

        public DbSet<Images> Images { get; set;}
        public DbSet<SinglePuzzleTag> SingPuzzleTag { get; set; }
        public DbSet<DVMN.Models.SinglePuzzle> SinglePuzzle { get; set; }
        public DbSet<HistoryAnswerPuzzle> HistoryAnswerPuzzle { get; set; }
        public DbSet<HistoryLikePuzzle> HistoryLikePuzzle { get; set; }
    }
}
