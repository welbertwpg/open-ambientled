using OpenAmbientLED.WpfApp.Enums;
using System.IO;
using System.Text.Json;
using System.Windows.Media;

namespace OpenAmbientLED.WpfApp
{
    internal class OpenRgbConfiguration
    {
        private const string FileName = "settings.json";

        private OpenRgbConfiguration() { }

        public static OpenRgbConfiguration Load()
        {
            try
            {
                var fileContent = File.ReadAllText(FileName);
                return JsonSerializer.Deserialize<OpenRgbConfiguration>(fileContent);
            }
            catch
            {
                return new OpenRgbConfiguration();
            }
        }

        public static void Save(OpenRgbConfiguration configuration)
            => File.WriteAllText(FileName, JsonSerializer.Serialize(configuration));

        public AudioLedMode AudioLedMode { get; set; }
        public RgbLedMode RgbLedMode { get; set; }
        public Color Color { get; set; }
        public bool StartInCustomPaletteMode { get; set; }
        public bool StartMinimized { get; set; }

        public void Save()
            => Save(this);
    }
}
