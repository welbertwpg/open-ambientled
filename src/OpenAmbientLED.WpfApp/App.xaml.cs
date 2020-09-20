using Microsoft.Win32;
using OpenAmbientLED.Controllers;
using OpenAmbientLED.Enums;
using OpenAmbientLED.External;
using OpenAmbientLED.WpfApp.Extensions;
using System;
using System.Windows;

namespace OpenAmbientLED.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static readonly OpenRgbConfiguration Configuration = OpenRgbConfiguration.Load();
        private static readonly BiosLedSettingsController biosSettingsController = new BiosLedSettingsController();

        public App()
        {
            InvkSMBCtrl.LibInitial();

            Current.Exit += (sender, args) => SaveConfiguration();
            Current.SessionEnding += (sender, args) => SaveConfiguration();
            
            SystemEvents.PowerModeChanged += OnPowerModeChanged;
        }

        private static void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Suspend:
                    SaveConfiguration();
                    break;
            }
        }

        private static void SaveConfiguration()
        {
            Configuration.Save();

            try
            {
                biosSettingsController.SetColor(Configuration.Color.GetHex());
                biosSettingsController.SetMode((LedMode)Configuration.RgbLedMode);
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine("[ex123]:Save LED setting to BIOS fail, " + ex.Message);
            }
        }
    }
}
