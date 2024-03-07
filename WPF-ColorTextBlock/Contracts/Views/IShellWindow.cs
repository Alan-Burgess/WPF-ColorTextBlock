using System.Windows.Controls;

namespace WPF_ColorTextBlock.Contracts.Views;

public interface IShellWindow
{
    Frame GetNavigationFrame();

    void ShowWindow();

    void CloseWindow();
}
