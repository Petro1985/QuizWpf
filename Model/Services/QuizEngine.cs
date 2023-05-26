using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace Model.Services;

public class QuizEngine
{
    public event EventHandler OnQuizStarted;
    public event EventHandler OnQuizEnded;

    private IList<QuizQuestion> _questions;
    private readonly QuestionsRepository _questionsRepo;
    private int _score;
    private int _currenQuestion;

    public QuizEngine(QuestionsRepository questionsRepo)
    {
        _questionsRepo = questionsRepo;
        _questions = new List<QuizQuestion>();
        _score = 0;
    }

    public async Task InitNewQuiz(int questionsCount, Guid topic)
    {
        _score = 0;
        _currenQuestion = 0;
        _questions = (await _questionsRepo.GetQuestions(questionsCount, topic))
            .Select(x => new QuizQuestion(x)).ToList();
        var realQuestion = await _questionsRepo.GetQuestions(questionsCount, topic);
        OnOnQuizStarted();
    }

    public IEnumerable<QuizResult> GetResults() =>
        _questions.Select((x, i) =>
            new QuizResult
            {
                QuestionText = x.Text,
                QuestionNumber = i,
                IsAnsweredCorrectly = x.WasAnsweredCorrectly
            });


    public QuestionEntity GetCurrentQuestion()
        => _questions[_currenQuestion];

    public int GetQuestionNumber()
        => _currenQuestion;

    public bool IsLastQuestion => _currenQuestion == _questions.Count - 1;

    public QuestionEntity GetNextQuestion()
    {
        if (_currenQuestion == _questions.Count - 1)
        {
            return _questions[_currenQuestion];
        }
        return _questions[++_currenQuestion];
    }

    public bool CheckAnswers(List<int> answers)
    {
        var currenQuestion = _questions[_currenQuestion];
        var correctAnswers = currenQuestion.Answers
            .Select((x, i) => new {IsCorrect = x.IsCorrect, Ind = i})
            .Where(x => x.IsCorrect)
            .Select((x, i) => i)
            .ToList();

        if (!(correctAnswers.All(answers.Contains) && answers.Count == correctAnswers.Count))
        {
            return false;
        }

        _score++;
        currenQuestion.WasAnsweredCorrectly = true;
        return true;
    }

    public int GetScore() => _score;

    private class QuizQuestion : QuestionEntity
    {
        public bool WasAnsweredCorrectly { get; set; }

        public QuizQuestion(QuestionEntity question)
        {
            WasAnsweredCorrectly = false;
            Text = question.Text;
            Topic = question.Topic;
            Answers = question.Answers.ToList();
        }
    }

    public void FinishQuiz()
    {
        OnOnQuizEnded();
    }
    
    protected virtual void OnOnQuizStarted()
    {
        OnQuizStarted?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnOnQuizEnded()
    {
        OnQuizEnded?.Invoke(this, EventArgs.Empty);
    }
}

public class QuizResult
{
    public int QuestionNumber { get; set; }
    public string QuestionText { get; set; }
    public bool IsAnsweredCorrectly { get; set; }
}