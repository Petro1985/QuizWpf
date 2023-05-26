using System;

namespace DAL.Entities;

public class AnswerEntity : BaseEntity<Guid>
{
    public string Text { get; set; }
    public string? Review { get; set; }
    public bool IsCorrect { get; set; }
    public Guid QuestionId { get; set; }

    public QuestionEntity Question { get; set; }
}