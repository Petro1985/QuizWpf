using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class QuizDb : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<AnswerEntity> Answers { get; set; }
    public DbSet<QuestionEntity> Questions { get; set; }
    public DbSet<TopicEntity> Topics { get; set; }

    public QuizDb(DbContextOptions options) : base(options)
    {
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuestionEntity>()
            .ToTable("Questions")
            .HasKey(p => p.Id);
        modelBuilder.Entity<QuestionEntity>()
            .HasMany(p => p.Answers)
            .WithOne(p => p.Question).HasForeignKey(p => p.QuestionId);

        modelBuilder.Entity<TopicEntity>()
            .ToTable("Topics")
            .HasKey(x => x.Id);
        modelBuilder.Entity<TopicEntity>()
            .HasMany(p => p.Quiestions)
            .WithOne(p => p.Topic)
            .HasForeignKey(p => p.TopicId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AnswerEntity>().ToTable("Answers").HasKey(x => x.Id);
        
        base.OnModelCreating(modelBuilder);
    }
}