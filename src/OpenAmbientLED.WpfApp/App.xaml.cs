using OpenAmbientLED.Controllers;
using OpenAmbientLED.Enums;
using OpenAmbientLED.External;
using OpenAmbientLED.WpfApp.Extensions;
using System.Windows;

namespace OpenAmbientLED.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static readonly OpenRgbConfiguration Configuration = OpenRgbConfiguration.Load();
        public App()
        {
            InvkSMBCtrl.LibInitial();

            var biosSettingsController = new BiosLedSettingsController();

            Current.Exit += (sender, e) =>
            {
                Configuration.Save();

                biosSettingsController.SetColor(Configuration.Color.GetHex());
                biosSettingsController.SetMode((LedMode)Configuration.RgbLedMode);
            };
        }
    }
}
