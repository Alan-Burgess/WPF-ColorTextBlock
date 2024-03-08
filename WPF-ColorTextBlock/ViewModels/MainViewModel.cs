using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace WPF_ColorTextBlock.ViewModels;

public class MainViewModel : ObservableObject
{
    private string _colourText;
    private string _inputText;

    public MainViewModel()
    {
        InputText = Properties.Resources.MainPageTitle;
        UpdateColourText();
    }

    public string ColourText
    {
        get => _colourText;
        set
        {
            _colourText = value;
            OnPropertyChanged();
        }
    }

    public string InputText
    {
        get => _inputText;
        set
        {
            _inputText = value;
            OnPropertyChanged();
        }
    }

    public void UpdateColourText()
    {
        ColourText = InputText;
    }

    public ICommand UpdateColourTextCommand => new RelayCommand(UpdateColourText);
}


