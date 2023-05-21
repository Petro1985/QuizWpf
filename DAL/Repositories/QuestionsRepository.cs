using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class QuestionsRepository
{
    private readonly IDbContextFactory<QuizDb> _dbContextFactory;

    public QuestionsRepository(IDbContextFactory<QuizDb> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<QuestionEntity>> GetQuestions(int count, Guid topic = default)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        IQueryable<QuestionEntity> result = db.Questions;
        if (topic != default)
        {
            result = result.Where(x => x.TopicId == topic);
        }

        return await result.OrderBy(r => Guid.NewGuid()).Take(count).Include(x => x.Answers).ToListAsync();
    }
}