using System.Windows.Controls;

using WPF_ColorTextBlock.ViewModels;

namespace WPF_ColorTextBlock.Views;

public partial class BlankPage : Page
{
    public BlankPage(BlankViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
