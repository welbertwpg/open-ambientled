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

        public bool IsRgbLedAvailable { get; }
        public bool IsAudioLedAvailable { get; }

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

            rgbLed = MonocLedController.Create();
            IsRgbLedAvailable = rgbLed != null;

            audioLed = AudioLedController.Create();
            IsAudioLedAvailable = audioLed != null;

            if (!IsAudioLedAvailable && !IsRgbLedAvailable)
            {
                MessageBox.Show("Not supported!");
                Application.Current.Shutdown();
                return;
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
