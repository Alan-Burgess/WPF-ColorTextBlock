using System.Windows.Controls;

using MahApps.Metro.Controls;

using WPF_ColorTextBlock.Contracts.Views;
using WPF_ColorTextBlock.ViewModels;

namespace WPF_ColorTextBlock.Views;

public partial class ShellWindow : MetroWindow, IShellWindow
{
    public ShellWindow(ShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public Frame GetNavigationFrame()
        => shellFrame;

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();
}
