using OpenAmbientLED.Enums;

namespace OpenAmbientLED.ConsoleApp
{
    internal class Args
    {
        public bool ChangeAudioLed { get; set; }
        public bool ChangeRgbLed { get; set; }
        public LedMode? Mode { get; set; }
        public uint? Color { get; set; }
    }
}
