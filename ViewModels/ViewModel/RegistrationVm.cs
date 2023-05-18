using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Model.Models;
using Model.Services;
using Model.Services.ViewService;
using ViewModels.Commands;

namespace ViewModels.ViewModel;

public sealed class RegistrationVm : INotifyPropertyChanged
{
    private readonly IUserService _userService;

    public RegistrationVm(IUserService userService)
    {
        _userService = userService;
        RegisterCommand = new RelayCommand(RegisterCommandExecute, _ => RegisterCommandCanExecute());
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

    private string _passwordRep = "";

    public string PasswordRep
    {
        get => _passwordRep;
        set
        {
            _passwordRep = value;
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

    private DateTime? _birthDay;

    public DateTime? BirthDay
    {
        get => _birthDay;
        set
        {
            _birthDay = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public ICommand RegisterCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private bool RegisterCommandCanExecute()
        =>
            !string.IsNullOrWhiteSpace(_login) &&
            !string.IsNullOrWhiteSpace(_password);
            // && _password == PasswordRep;

    private async void RegisterCommandExecute(object? openLoginWindow)
    {
        if (openLoginWindow is null)
        {
            throw new ArgumentException("Parameter action is null", nameof(openLoginWindow));
        }
            
        if (_password != _passwordRep)
        {
            ErrorText = "Passwords are different";
            return;
        }
        try
        {
            var user = new User
            {
                Login = _login,
                Password = _password,
                BirthDate = _birthDay,
            };
            await _userService.TrySignUp(user);
            MessageBox.Show("You've been successfully registered");
            ErrorText = "";
            ((Action) openLoginWindow).Invoke();
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