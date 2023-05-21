using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model.Services;
using ViewModels.Commands;

namespace ViewModels.ViewModel;

public sealed class LoginVm : INotifyPropertyChanged
{
    private readonly IUserService _userService;
    
    public LoginVm(IUserService userService) 
    {
        _userService = userService;

        // LoginCommand = new LogInCommand();        
        LoginCommand = new RelayCommand(LoginCommandExecute, _ => LoginCommandCanExecute());
    }

    #region ViewProperties

    private string _login = "";

    public string Login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged();
        }
    }

    private string _password = "";

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    private string _errorText = "";

    public string ErrorText
    {
        get => _errorText;
        set
        {
            _errorText = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public ICommand LoginCommand { get; }
    public ICommand SignupCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private bool LoginCommandCanExecute()
        => !string.IsNullOrWhiteSpace(_login) && !string.IsNullOrWhiteSpace(_password);

    private async void LoginCommandExecute(object? openNextWindow)
    {
        try
        {
            if (openNextWindow is null)
            {
                throw new ArgumentException("Null parameter", nameof(openNextWindow));
            }
            await _userService.TryLogin(Login, Password);
            ((Action)openNextWindow)();
        }
        catch (Exception e)
        {
            ErrorText = e.Message;
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}