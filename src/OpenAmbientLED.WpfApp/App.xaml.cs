using OpenAmbientLED.External;
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

            Current.Exit += (sender, e) => Configuration.Save(); ;
        }
    }
}
