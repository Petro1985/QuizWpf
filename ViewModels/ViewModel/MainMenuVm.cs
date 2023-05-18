using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ViewModels.ViewModel;

public class MainMenuVm : INotifyPropertyChanged
{
    private List<int> _questionCounts;
    private List<string> _topics;

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

    private string _selectedTopic;
    public string SelectedTopic
    {
        get => _selectedTopic;
        set
        {
            _selectedTopic = value;
            OnPropertyChanged();
        }
    }

    public MainMenuVm()
    {
        
    }

    public async Task InitValues()
    {
        
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}