using System.Windows.Controls;
using Model.Services.ViewService;

namespace TestMVVM.Windows;

public partial class QuizWindow : UserControl, IView
{
    public QuizWindow()
    {
        InitializeComponent();
    }
}