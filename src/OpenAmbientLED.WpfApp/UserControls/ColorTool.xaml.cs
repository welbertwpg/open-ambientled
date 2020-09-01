using MaterialDesignColors;
using MaterialDesignColors.Recommended;
using OpenAmbientLED.WpfApp.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OpenAmbientLED.WpfApp.UserControls
{
    public delegate void ColorChangedEventHandler(Color color);

    public partial class ColorTool : UserControl, INotifyPropertyChanged
    {
        public ColorTool()
        {
            ChangeHueCommand = new AnotherCommandImplementation(ChangeHue);

            DataContext = this;
            InitializeComponent();
        }

        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    OnPropertyChanged();
                    OnColorChanged(value);
                }
            }
        }

        public ICommand ChangeHueCommand { get; }

        private void ChangeHue(object obj)
            => SelectedColor = (Color)obj;

        public IEnumerable<ISwatch> Swatches { get; } = new ISwatch[]
        {
                new RedSwatch(),
                new PinkSwatch(),
                new PurpleSwatch(),
                new DeepPurpleSwatch(),
                new IndigoSwatch(),
                new BlueSwatch(),
                new LightBlueSwatch(),
                new CyanSwatch(),
                new TealSwatch(),
                new GreenSwatch(),
                new LightGreenSwatch(),
                new LimeSwatch(),
                new YellowSwatch(),
                new AmberSwatch(),
                new OrangeSwatch(),
                new DeepOrangeSwatch(),
                new BrownSwatch(),
                new GreySwatch(),
                new BlueGreySwatch()
        };

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event ColorChangedEventHandler ColorChanged;
        protected virtual void OnColorChanged(Color color)
            => this.ColorChanged.Invoke(color);
    }
}
