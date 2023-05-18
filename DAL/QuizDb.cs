using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class QuizDb : DbContext
{
    public DbSet<UserEntity> Users { get; set; }

    public QuizDb(DbContextOptions options) : base(options)
    {
    }
}