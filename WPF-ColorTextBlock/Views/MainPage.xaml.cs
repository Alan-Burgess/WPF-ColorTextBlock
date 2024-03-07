using System.Windows.Controls;

using WPF_ColorTextBlock.ViewModels;

namespace WPF_ColorTextBlock.Views;

public partial class MainPage : Page
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
