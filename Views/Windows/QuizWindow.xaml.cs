using System;
using System.Windows.Controls;
using Model.Services.ViewService;
using ViewModels.ViewModel;

namespace TestMVVM.Windows;

public partial class QuizWindow : UserControl, IView
{
    private IViewsLoader _viewsLoader;
    public QuizWindow(QuizVm quizVm, IViewsLoader viewsLoader)
    {
        _viewsLoader = viewsLoader;
        DataContext = quizVm;
        InitializeComponent();
    }

    public Action OpenResults => () =>
    {
        _viewsLoader.LoadView<ResultWindow>();
    };
}