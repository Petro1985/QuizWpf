using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DAL.Entities;
using Model.Services;
using ViewModels.Commands;

namespace ViewModels.ViewModel;

public class QuizVm : INotifyPropertyChanged
{
    private readonly QuizEngine _quizEngine;
    
    private string _title;
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }
   
    private string _question;
    public string Question
    {
        get => _question;
        set
        {
            _question = value;
            OnPropertyChanged();
        }
    }
   
    private List<QuizAnswers> _answers;
    public List<QuizAnswers> Answers
    {
        get => _answers;
        set
        {
            _answers = value;
            OnPropertyChanged();
        }
    }

    public ICommand NextQuestion { get; set; }
    public ICommand FinishQuiz { get; set; }

    public QuizVm(QuizEngine quizEngine)
    {
        _quizEngine = quizEngine;
        _quizEngine.OnQuizStarted += OnQuizStarted;
        SetPropertiesForQuestion(_quizEngine.GetCurrentQuestion());
        NextQuestion = new RelayCommand(NextQuestionExecute, _ => !_quizEngine.IsLastQuestion);
        FinishQuiz = new RelayCommand(FinishQuizExecute, _ => _quizEngine.IsLastQuestion);
    }

    private void FinishQuizExecute(Object? openResultWindow)
    {
        if (openResultWindow is Action openWindow)
        {
            CheckAnswers();
            _quizEngine.FinishQuiz();
            openWindow();
        }
        else
        {
            throw new ArgumentException("Wrong parameter type", nameof(openResultWindow));
        }
        
    }
    
    private void SetPropertiesForQuestion(QuestionEntity question)
    {
        Answers = question.Answers
            .Select((x, i) => new QuizAnswers {Number = i, IsSelected = false, Text = x.Text})
            .ToList();
        Question = question.Text;
        Title = $"Question №{_quizEngine.GetQuestionNumber() + 1}";
    }

    private void CheckAnswers()
    {
        var answers = _answers
            .Where(x => x.IsSelected)
            .Select(x => x.Number)
            .ToList();
        _quizEngine.CheckAnswers(answers);
    }
    
    private void NextQuestionExecute(object? param)
    {
        CheckAnswers();
        SetPropertiesForQuestion(_quizEngine.GetNextQuestion());
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnQuizStarted(object? sender, EventArgs e)
    {
        SetPropertiesForQuestion(_quizEngine.GetCurrentQuestion());
    }
}


public class QuizAnswers
{
    public int Number { get; set; }
    public string Text { get; set; }
    public bool IsSelected { get; set; }
}