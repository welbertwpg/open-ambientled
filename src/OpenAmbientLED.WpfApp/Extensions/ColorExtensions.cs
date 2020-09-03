using System.Windows.Media;

namespace OpenAmbientLED.WpfApp.Extensions
{
    public static class ColorExtensions
    {
        public static uint GetHex(this Color color)
        {
            int colorArgb = (color.A << 24) | (color.R << 16) | (color.G << 8) | color.B;
            return (uint)colorArgb;
        }
    }
}
