using System;
using System.Windows;
using System.Windows.Controls;
using Model.Services.ViewService;
using ViewModels.ViewModel;

namespace TestMVVM.Windows
{
    public partial class LoginWindow : UserControl, IView
    {
        private readonly IViewsLoader _viewsLoader;
        
        public LoginWindow(IViewsLoader viewsLoader, LoginVm loginVm)
        {
            _viewsLoader = viewsLoader;
            DataContext = loginVm;
            InitializeComponent();
        }

        private void OnPasswordChange(object sender, RoutedEventArgs e)
        {
            ((LoginVm) DataContext).Password = ((PasswordBox) sender).Password;
        }

        public Action OpenMainMenuWindowCallBack => () =>
        {
            _viewsLoader.LoadView<MainMenuWindow>();
        };

        private void OpenRegistrationWindow(object sender, RoutedEventArgs e)
        {
            _viewsLoader.LoadView<RegistrationWindow>();
        }
    }
}