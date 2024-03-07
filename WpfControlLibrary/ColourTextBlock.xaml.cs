using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfControlLibrary
{
    /// <summary>
    /// Interaction logic for ColourTextBlock.xaml
    /// </summary>
    public partial class ColourTextBlock : SelectableTextBlock
    {
        private bool _initialised;

        public ColourTextBlock()
        {
            InitializeComponent();

        }

        public static readonly DependencyProperty ColourTextProperty = DependencyProperty.Register(
           nameof(ColourText),
           typeof(string),
           typeof(ColourTextBlock),
           new PropertyMetadata(new PropertyChangedCallback(ColourText_Changed)));

        private static void ColourText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColourTextBlock)d).ColourText = ((ColourTextBlock)d).ColourText;
        }

        public string ColourText
        {
            get => (string)GetValue(ColourTextProperty);
            set
            {
                SetValue(ColourTextProperty, value);

                if (!string.IsNullOrEmpty(value))
                {
                    if (!_initialised)
                    {
                        // We need to wait for the Style to be initialised before we can set the text
                        // This is because the Style can be set after the ColourText property is set

                        Task.Run(async () =>
                        {
                            await Task.Delay(10);
                            Dispatcher.Invoke(() =>
                            {
                                _fontSize = FontSize;
                                _fontFamily = FontFamily;
                                _foreground = Foreground;
                                _fontStyle = FontStyle;
                                _fontWeight = FontWeight;
                                _initialised = true;

                                SetText(value);
                            });
                        });
                    }
                    else
                    {
                        SetText(value);
                    };
                }
                else
                {
                    Text = string.Empty;
                }
            }
        }

        private void SetText(string text)
        {

            Inlines.Clear();
            _stack.Clear();

            var segments = text.Split('%').ToList();

            if (!string.IsNullOrEmpty(segments[0]))
            {
                segments[0] = '%' + segments[0];
            }
            else
            {
                segments.RemoveAt(0);
            }

            try
            {
                foreach (var segment in segments)
                {
                    CreateStringAndColour(segment);
                }
            }
            catch ( ArgumentException ex )
            {
                Text = $"String Colour Error: {ex.Message}";
            }
        }

        private readonly Stack<object> _stack = new();

        private Brush _foreground;
        private bool _escapeChar;
        private FontStyle _fontStyle;
        private FontWeight _fontWeight;
        private double _fontSize;
        private FontFamily _fontFamily;

        private void CreateStringAndColour(string text)
        {
            string str;

            if (string.IsNullOrEmpty(text))
            {
                _escapeChar = true;
                return;
            }

            if (_escapeChar)
            {
                str = '%'+text;
                _escapeChar = false;
            }
            else
            {
                str = text.Remove(0, 1);

                switch (text[0])
                {
                    case '%':
                        break;

                    case 'R':
                    case 'T': // Type
                        _stack.Push(_foreground);
                        _foreground = Brushes.Red;
                        break;

                    case 'P':
                    case 'V': // Variable
                        _stack.Push(_foreground);
                        _foreground = Brushes.Purple;
                        break;

                    case 'G':
                    case 'O': // Optional
                        _stack.Push(_foreground);
                        _foreground = Brushes.Green;
                        break;

                    case 'F': // Function
                        _stack.Push(_foreground);
                        _foreground = Brushes.Blue;
                        break;

                    case 'B':
                        _stack.Push(_foreground);
                        _foreground = Brushes.Black;
                        break;

                    case 'i':
                        _stack.Push(_fontStyle);
                        _fontStyle = FontStyles.Italic;
                        break;

                    case 'o':
                        _stack.Push(_fontStyle);
                        _fontStyle = FontStyles.Oblique;
                        break;

                    case 'n':
                        _stack.Push(_fontStyle);
                        _fontStyle = FontStyles.Normal;
                        break;

                    case 'b':
                        _stack.Push(_fontWeight);
                        _fontWeight = FontWeights.Bold;
                        break;

                    case 'd':
                        _stack.Push(_fontWeight);
                        _fontWeight = FontWeights.DemiBold;
                        break;

                    case 'w':
                        _stack.Push(_fontWeight);
                        var w = GetValue(text, ref str);
                        _fontWeight = Enum.Parse<FontWeight>(w);
                        break;

                    case 's':
                        _stack.Push(_fontSize);
                        var v = GetValue(text, ref str);
                        _fontSize = double.Parse(v);
                        break;

                    case 'f':
                        _stack.Push(_fontFamily);
                        var f = GetValue(text, ref str);
                        _fontFamily = new FontFamily(f);
                        break;

                    case 'c':
                        _stack.Push(_foreground);
                        var c = GetValue(text, ref str);
                        _foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(c));
                        break;

                    case '>':
                    case '<':
                        if (_stack.Count > 0)
                        {
                            var obj = _stack.Pop();
                            switch (obj)
                            {
                                case Brush b:
                                    _foreground = b;
                                    break;
                                case FontStyle fs:
                                    _fontStyle = fs;
                                    break;
                                case FontWeight fw:
                                    _fontWeight = fw;
                                    break;
                                case double d:
                                    _fontSize = d;
                                    break;
                                case FontFamily ff:
                                    _fontFamily = ff;
                                    break;
                            }
                        }
                        break;

                    default:
                        throw new ArgumentException($"'{text[0]}' not a valid colour or type in string '{text}'");
                }
            }

            if (string.IsNullOrEmpty(str)) return;

            Inlines.Add(new Run(str) { Foreground = _foreground, FontFamily = _fontFamily, FontStyle = _fontStyle, FontWeight = _fontWeight, FontSize = _fontSize });
        }

        static string GetValue(string text, ref string str)
        {
            var inx = text.IndexOf('>');
            
            str = str.Remove(0, inx);

            return text[1..inx];
        }
    }
}
