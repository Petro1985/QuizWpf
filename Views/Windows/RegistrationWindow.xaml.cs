﻿using System;
using System.Windows;
using System.Windows.Controls;
using Model.Services.ViewService;
using ViewModels.ViewModel;

namespace TestMVVM.Windows
{
    public partial class RegistrationWindow : UserControl, IView
    {
        private readonly IViewsLoader _viewsLoader;
        
        public RegistrationWindow(IViewsLoader viewsLoader, RegistrationVm viewModel)
        {
            InitializeComponent();
            _viewsLoader = viewsLoader;
            DataContext = viewModel;
        }

        public Action OpenLoginWindowCallBack => () =>
        {
            _viewsLoader.LoadView<LoginWindow>();
        };

        private void OpenLoginWindow(object sender, RoutedEventArgs e)
        {
            OpenLoginWindowCallBack();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((RegistrationVm) DataContext).Password = Password.Password;
        }

        private void OnRepPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((RegistrationVm) DataContext).PasswordRep = RepPassword.Password;
        }
    }
}
