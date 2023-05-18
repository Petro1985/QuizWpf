using System;
using System.Windows;
using System.Windows.Controls;
using Model.Services.ViewService;
using ViewModels.ViewModel;

namespace TestMVVM.Windows
{
    public partial class LoginWindow : UserControl, IView
    {
        private IViewsLoader _viewsLoader;
        
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

        public Action OpenNewWindowCallBack => () =>
        {
            _viewsLoader.LoadView<RegistrationWindow>();
        };

        public string Test { get; set; } = "Test!!!";

        private void OpenRegistrationWindow(object sender, RoutedEventArgs e)
        {
            _viewsLoader.LoadView<RegistrationWindow>();
        }
    }
}