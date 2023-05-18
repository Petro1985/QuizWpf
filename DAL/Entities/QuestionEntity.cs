using System;
using System.Collections.Generic;

namespace DAL.Entities;

public class QuestionEntity : BaseEntity<Guid>
{
    public string Text { get; set; } 
    public Guid TopicId { get; set; }

    public TopicEntity Topic { get; set; }
    public ICollection<AnswerEntity> Answers { get; set; }
}