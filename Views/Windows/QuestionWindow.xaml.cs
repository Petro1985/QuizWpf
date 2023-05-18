using System.Windows.Controls;
using Model.Services.ViewService;

namespace TestMVVM.Windows;

public partial class QuestionWindow : UserControl, IView
{
    public QuestionWindow()
    {
        InitializeComponent();
    }
}