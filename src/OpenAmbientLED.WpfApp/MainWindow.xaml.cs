using OpenAmbientLED.Controllers;
using OpenAmbientLED.Enums;
using OpenAmbientLED.Interfaces;
using OpenAmbientLED.WpfApp.Enums;
using OpenAmbientLED.WpfApp.Extensions;
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

        public AudioLedMode SelectedAudioLedMode
        {
            get => App.Configuration.AudioLedMode;
            set
            {
                App.Configuration.AudioLedMode = value;
                audioLed.SetMode((LedMode)value);
            }
        }

        public RgbLedMode SelectedRgbLedMode
        {
            get => App.Configuration.RgbLedMode;
            set
            {
                App.Configuration.RgbLedMode = value;
                rgbLed.SetMode((LedMode)value);
            }
        }

        public MainWindow()
        {
            rgbLed = RgbLedController.Create();
            IsRgbLedAvailable = rgbLed != null;

            audioLed = AudioLedController.Create();
            IsAudioLedAvailable = audioLed != null;

            if (!IsAudioLedAvailable && !IsRgbLedAvailable)
            {
                MessageBox.Show("Not supported!");
                Application.Current.Shutdown();
                return;
            }

            LoadConfiguration();

            DataContext = this;
            InitializeComponent();
        }

        private void LoadConfiguration()
        {
            if (IsAudioLedAvailable)
                SelectedAudioLedMode = App.Configuration.AudioLedMode;

            if (IsRgbLedAvailable)
            {
                SelectedRgbLedMode = App.Configuration.RgbLedMode;
                SetColor(App.Configuration.Color);
            }
        }

        private void SetColor(Color color)
            => rgbLed.SetColor(color.GetHex());
    }
}
