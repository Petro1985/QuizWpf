using System.Net.Http.Json;
using System.Text.Json;
using DAL;
using DAL.Entities;
using DbSeeder.ApiModel;
using Microsoft.EntityFrameworkCore;

public class Seeder
{
    public async Task SeedDataBase(QuizDb quizDb)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://quizapi.io");
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        client.DefaultRequestHeaders.Add("X-Api-Key", "8VdR2qbyFW842HcGZVbYeXj8fkcRAMlD3G5K5yao");

        for (int i = 0; i < 1000; i++)
        {
            Console.WriteLine(i);
            var response = await client.GetFromJsonAsync<IEnumerable<Response>>("/api/v1/questions", jsonOptions);

            foreach (var item in response!)
            {
                var question = await quizDb.Questions
                    .FirstOrDefaultAsync(x => x.Text == item.Question);

                if (question is not null)
                {
                    continue;
                }

                var topic = await quizDb.Topics.FirstOrDefaultAsync(x => x.Name == item.Category);

                if (topic is null)
                {
                    topic = new TopicEntity()
                    {
                        Id = Guid.NewGuid(),
                        Name = item.Category
                    };
                    quizDb.Topics.Add(topic);
                }

                question = new QuestionEntity
                {
                    Text = item.Question,
                    Id = Guid.NewGuid(),
                    Topic = topic
                };
                quizDb.Questions.Add(question);

                var answers = new List<AnswerEntity?>
                {
                    CreateAnswer(question, item.Answers.Answer_a, item.Correct_answers.Answer_a_correct),
                    CreateAnswer(question, item.Answers.Answer_b, item.Correct_answers.Answer_b_correct),
                    CreateAnswer(question, item.Answers.Answer_c, item.Correct_answers.Answer_c_correct),
                    CreateAnswer(question, item.Answers.Answer_d, item.Correct_answers.Answer_d_correct),
                    CreateAnswer(question, item.Answers.Answer_e, item.Correct_answers.Answer_e_correct),
                    CreateAnswer(question, item.Answers.Answer_f, item.Correct_answers.Answer_f_correct),
                };

                foreach (var answer in answers)
                {
                    if (answer != null) quizDb.Answers.Add(answer);
                }

                await quizDb.SaveChangesAsync();
            }
        }
    }

    private AnswerEntity? CreateAnswer(QuestionEntity q, string text, string isCorrect)
        => string.IsNullOrWhiteSpace(text)
            ? null
            : new AnswerEntity()
            {
                Id = Guid.NewGuid(),
                Text = text,
                Question = q,
                IsCorrect = isCorrect == "true",
            };
}