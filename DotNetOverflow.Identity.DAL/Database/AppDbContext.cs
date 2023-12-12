using DotNetOverflow.Core.Entity.Account;
using DotNetOverflow.Core.Entity.Image;
using DotNetOverflow.Core.Entity.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetOverflow.Identity.DAL.Database;
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<long>, long>
    {
        public DbSet<QuestionEntity> Questions { get; set; } = null!;

        public DbSet<ImageEntity> Images { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5433;Database=DNOGeneralDbDevelopment;User Id=postgres;Password=1111;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<IdentityUserLogin<long>>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            modelBuilder.Entity<AppUser>()
                .HasIndex(x => x.Id)
                .HasDatabaseName("IdUserIndex");

            modelBuilder.Entity<AppUser>()
                .HasMany(b => b.Questions)
                .WithOne(x => x.AppUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionEntity>()
                .HasOne(x => x.AppUser)
                .WithMany(x => x.Questions)
                .HasForeignKey(x=>x.AuthorId);

            modelBuilder.Entity<ImageEntity>()
                .HasOne(x => x.Question)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ImageEntity>()
                .HasIndex(x => x.Id)
                .HasDatabaseName("IdImageIndex");
            
            modelBuilder.Entity<QuestionEntity>()
                    .HasMany(x => x.Images)
                    .WithOne(x => x.Question)
                    .HasForeignKey(x => x.QuestionId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionEntity>()
                .HasIndex(x => x.Id)
                .HasDatabaseName("IdQuestionIndex");
            
            modelBuilder.Entity<QuestionEntity>()
                .HasIndex(x => x.Title)
                .HasDatabaseName("TitleQuestionIndex");
        }
    }
