using KitsuQuestions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Goal> Goals => Set<Goal>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Resource> Resources => Set<Resource>();
        public DbSet<QuizAttempt> QuizAttempts => Set<QuizAttempt>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Goal>()
                .HasOne(g => g.Category)
                .WithMany(c => c.Goals)
                .HasForeignKey(g => g.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Resource>()
                .HasOne(r => r.Goal)
                .WithMany(g => g.Resources)
                .HasForeignKey(r => r.GoalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuizAttempt>()
                .HasOne(q => q.Goal)
                .WithMany(g => g.QuizAttempts)
                .HasForeignKey(q => q.GoalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Programming", HexColor = "#7C3AED" },
                new Category { Id = 2, Name = "Languages", HexColor = "#10B981" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
