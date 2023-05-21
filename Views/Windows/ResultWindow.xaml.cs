using System;
using System.Windows;
using System.Windows.Controls;
using Model.Services.ViewService;
using ViewModels.ViewModel;

namespace TestMVVM.Windows
{
    public partial class ResultWindow : UserControl, IView
    {
        private readonly IViewsLoader _viewsLoader;
        public ResultWindow(ResultVm resultVm, IViewsLoader viewsLoader)
        {
            _viewsLoader = viewsLoader;
            DataContext = resultVm;
            InitializeComponent();
        }

        private void ToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            _viewsLoader.LoadView<MainMenuWindow>();
        }
    }
}

