using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DAL.Entities;
using DAL.Repositories;
using ViewModels.Commands;

namespace ViewModels.ViewModel;

public class MainMenuVm : INotifyPropertyChanged
{
    public List<int> QuestionCounts { get; set; }
    public List<TopicEntity> Topics { get; set; }

    private int _selectedQuestionCount;
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
    public MainMenuVm(TopicsRepository topicsRepo)
    {
        QuestionCounts = new List<int> {5, 10, 15, 20};
        _topicsRepo = topicsRepo;
        StartQuiz = new RelayCommand((o) => StartQuizExecute(o), _ => true);
    }

    private void StartQuizExecute(object? openNextWindow)
    {
        MessageBox.Show($"Topic: {_selectedTopic} cunt: {_selectedQuestionCount}");
        
        ((Action) openNextWindow!)();
    }
    
    public async Task InitTopicsValue()
    {
        Topics = await _topicsRepo.GetAllTopics();
        OnPropertyChanged("Topics");
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}