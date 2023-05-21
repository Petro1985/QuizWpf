using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Model.Services;

namespace ViewModels.ViewModel;

public class ResultVm : INotifyPropertyChanged
{
    private readonly QuizEngine _quizEngine;
    
    private List<QuizResult> _results;
    public List<QuizResult> Results
    {
        get => _results;
        set
        {
            _results = value;
            OnPropertyChanged();
        }
    }


    private string _scoreText;
    public string ScoreText
    {
        get => _scoreText;
        set
        {
            _scoreText = value;
            OnPropertyChanged();
        }
    }

    public ResultVm(QuizEngine quizEngine)
    {
        _quizEngine = quizEngine;
        _quizEngine.OnQuizEnded += OnQuizEnded;
        Results = _quizEngine.GetResults().ToList();
    }

    private void OnQuizEnded(Object? o, EventArgs e)
    {
        Results = _quizEngine.GetResults().ToList();
        ScoreText = $"Your score is {_quizEngine.GetScore()}";
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}