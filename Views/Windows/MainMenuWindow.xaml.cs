using System;
using System.Windows;
using System.Windows.Controls;
using Model.Services.ViewService;
using ViewModels.ViewModel;

namespace TestMVVM.Windows
{
    public partial class MainMenuWindow : UserControl, IView
    {
        private readonly IViewsLoader _viewsLoader;
        public MainMenuWindow(MainMenuVm mainMenuVm, IViewsLoader viewsLoader)
        {
            _viewsLoader = viewsLoader;
            DataContext = mainMenuVm;
            InitializeComponent();
            Loaded += InitValues;
        }

        private async void InitValues(object sender, RoutedEventArgs e)
        {
            await ((MainMenuVm)DataContext).InitTopicsValue();
        }

        public Action OpenQuizWindow => () =>
        {
            _viewsLoader.LoadView<QuizWindow>();
        };
    }
}

