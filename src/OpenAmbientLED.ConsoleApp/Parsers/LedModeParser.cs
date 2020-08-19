using OpenAmbientLED.Enums;
using System;

namespace OpenAmbientLED.ConsoleApp.Parsers
{
    public static class LedModeParser
    {
        public static LedMode Parse(string s)
        {
            Enum.TryParse(s, true, out LedMode ledMode);
            return ledMode;
        }
    }
}
