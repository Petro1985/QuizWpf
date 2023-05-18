namespace DbSeeder.ApiModel;

public class Response
{
    public int Id { get; set; }
    public string Question { get; set; }
    public Answer Answers { get; set; }
    public CorrectAnswer Correct_answers { get; set; }
    public string Category { get; set; }
}

public class Answer
{
    public string Answer_a { get; set; }
    public string Answer_b { get; set; }
    public string Answer_c { get; set; }
    public string Answer_d { get; set; }
    public string Answer_e { get; set; }
    public string Answer_f { get; set; }
}

public class CorrectAnswer
{
    public string Answer_a_correct { get; set; }
    public string Answer_b_correct { get; set; }
    public string Answer_c_correct { get; set; }
    public string Answer_d_correct { get; set; }
    public string Answer_e_correct { get; set; }
    public string Answer_f_correct { get; set; }
}