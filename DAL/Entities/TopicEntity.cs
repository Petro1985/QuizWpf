using System;
using System.Collections.Generic;

namespace DAL.Entities;

public class TopicEntity : BaseEntity<Guid>
{
    public string Name { get; set; }

    public ICollection<QuestionEntity> Quiestions { get; set; }
}