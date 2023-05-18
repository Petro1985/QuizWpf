using System.Windows;
using Model.Services.ViewService;

namespace TestMVVM.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IViewsLoader _viewsLoader;

    public MainWindow(IViewsLoader viewsLoader)
    {
        _viewsLoader = viewsLoader;
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        //загрузка стартовой View
        _viewsLoader.ContentPresenter = OutputView;
        _viewsLoader.LoadView<LoginWindow>();
    }
}