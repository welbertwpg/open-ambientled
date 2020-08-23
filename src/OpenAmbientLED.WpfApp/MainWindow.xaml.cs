using OpenAmbientLED.Controllers;
using OpenAmbientLED.External;
using OpenAmbientLED.Interfaces;
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

        public MainWindow()
        {
            InitializeComponent();

            InvkSMBCtrl.LibInitial();
            rgbLed = new MonocLedController();
        }

        private void ColorTool_ColorChanged(Color color)
        {
            int colorArgb = (color.A << 24) | (color.R << 16) | (color.G << 8) | color.B;
            rgbLed.SetColor((uint)colorArgb);
        }
    }
}
