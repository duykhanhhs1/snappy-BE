using _468_.Net_Fundamentals.Domain;
using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.EnumType;
using _468_.Net_Fundamentals.Domain.ViewModels.Authenticate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Bỏ tiền tố AspNet của các bảng: mặc định
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }



                // CmtAttachment
                builder.Entity<CommentAttachment>().HasKey(ca => new { ca.CommentId, ca.AttachmentId });
                builder.Entity<CommentAttachment>()
                    .HasOne(ca => ca.Comment)
                    .WithMany()
                    .HasForeignKey(ca => ca.CommentId)
                    .OnDelete(DeleteBehavior.Cascade);
                builder.Entity<CommentAttachment>()
                    .HasOne(ca => ca.Attachment)
                    .WithMany()
                    .HasForeignKey(ca => ca.AttachmentId)
                    .OnDelete(DeleteBehavior.NoAction);

                // CardAssign
                builder.Entity<CardAssign>().HasKey(ca => new { ca.CardId, ca.AssignTo });
                builder.Entity<CardAssign>()
                    .HasOne(ca => ca.Card)
                    .WithMany()
                    .HasForeignKey(ca => ca.CardId)
                    .OnDelete(DeleteBehavior.Cascade);
                builder.Entity<CardAssign>()
                    .HasOne(ca => ca.User)
                    .WithMany()
                    .HasForeignKey(ca => ca.AssignTo)
                    .OnDelete(DeleteBehavior.NoAction);

                // CardTag
                builder.Entity<CardTag>().HasKey(ct => new { ct.CardId, ct.TagId });
                builder.Entity<CardTag>()
                    .HasOne(ct => ct.Card)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(ct => ct.CardId);
                builder.Entity<CardTag>()
                    .HasOne(ct => ct.Tag)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(ct => ct.TagId);
            }

        }

        public DbSet<Activity> Activities { get; set; }


        public DbSet<Comment> Comments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<CommentAttachment> CommentAttachments { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardAssign> CardAssigns { get; set; }
        public DbSet<CardTag> CardTags { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }



    }
}
