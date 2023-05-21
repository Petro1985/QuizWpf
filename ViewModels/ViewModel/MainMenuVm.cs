using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DAL.Entities;
using DAL.Repositories;
using Model.Services;
using ViewModels.Commands;

namespace ViewModels.ViewModel;

public class MainMenuVm : INotifyPropertyChanged
{
    public List<int> QuestionCounts { get; set; }
    public List<TopicEntity> Topics { get; set; }

    private int _selectedQuestionCount;
    private readonly QuizEngine _quizEngine;
    public int SelectedQuestionCount
    {
        get => _selectedQuestionCount;
        set
        {
            _selectedQuestionCount = value;
            OnPropertyChanged();
        }
    }

    private Guid _selectedTopic;
    public Guid SelectedTopic
    {
        get => _selectedTopic;
        set
        {
            _selectedTopic = value;
            OnPropertyChanged();
        }
    }

    public ICommand StartQuiz { get; set; }

    private readonly TopicsRepository _topicsRepo;
    public MainMenuVm(TopicsRepository topicsRepo, QuizEngine quizEngine)
    {
        QuestionCounts = new List<int> {5, 10, 15, 20};
        _topicsRepo = topicsRepo;
        _quizEngine = quizEngine;
        StartQuiz = new RelayCommand(StartQuizExecute, _ => true);
    }

    private async void StartQuizExecute(object? openQuizWindow)
    {
        if (openQuizWindow is Action openWindow)
        {
            await _quizEngine.InitNewQuiz(_selectedQuestionCount, _selectedTopic);
            openWindow();
        }
        else
        {
            throw new ArgumentException("Wrong parameter type", nameof(openQuizWindow));
        }
    }
    
    public async Task InitTopicsValue()
    {
        Topics = await _topicsRepo.GetAllTopics();
        Topics.Add(new TopicEntity {Name = "Any topic", Id = default});
        SelectedTopic = Topics.Select(x => x.Id).FirstOrDefault();
        SelectedQuestionCount = 20;
        OnPropertyChanged(nameof(Topics));
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}