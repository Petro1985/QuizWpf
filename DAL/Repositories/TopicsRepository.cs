using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class TopicsRepository
{
    private readonly IDbContextFactory<QuizDb> _dbContextFactory;

    public TopicsRepository(IDbContextFactory<QuizDb> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<TopicEntity>> GetAllTopics()
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        return await db.Topics.ToListAsync();
    }
}