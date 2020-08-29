using OpenAmbientLED.Controllers;
using OpenAmbientLED.Enums;
using OpenAmbientLED.External;
using OpenAmbientLED.Interfaces;
using OpenAmbientLED.WpfApp.Enums;
using System.Windows;
using System.Windows.Media;

namespace OpenAmbientLED.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRgbLedController rgbLed;
        private readonly ILedController audioLed;

        private readonly bool IsRgbLedAvailable;
        private readonly bool IsAudioLedAvailable;

        private AudioLedMode _selectedAudioLedMode;
        public AudioLedMode SelectedAudioLedMode
        {
            get => _selectedAudioLedMode;
            set
            {
                _selectedAudioLedMode = value;
                audioLed.SetMode((LedMode)_selectedAudioLedMode);
            }
        }

        private RgbLedMode _selectedRgbLedMode;
        public RgbLedMode SelectedRgbLedMode
        {
            get => _selectedRgbLedMode;
            set
            {
                _selectedRgbLedMode = value;
                rgbLed.SetMode((LedMode)_selectedRgbLedMode);
            }
        }

        public MainWindow()
        {
            InvkSMBCtrl.LibInitial();

            try
            {
                rgbLed = new MonocLedController();
                IsRgbLedAvailable = rgbLed != null;
            }
            catch
            {
                IsRgbLedAvailable = false;
            }

            try
            {
                audioLed = new AudioLedController();
                IsAudioLedAvailable = audioLed != null;
            }
            catch
            {
                IsAudioLedAvailable = false;
            }

            DataContext = this;
            InitializeComponent();
        }

        private void ColorTool_ColorChanged(Color color)
        {
            int colorArgb = (color.A << 24) | (color.R << 16) | (color.G << 8) | color.B;
            rgbLed.SetColor((uint)colorArgb);
        }
    }
}
