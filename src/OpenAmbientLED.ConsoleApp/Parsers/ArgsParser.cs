using Mono.Options;
using OpenAmbientLED.ConsoleApp.Options;

namespace OpenAmbientLED.ConsoleApp.Parsers
{
    internal class ArgsParser
    {
        public static Args Parse(string[] args)
        {
            var argsObj = new Args();

            var options = new OptionSet {
                new ModeOption((m) => argsObj.Mode = m),
                new ColorOption((c) => argsObj.Color = c),
                new MonocLedOption((b) => argsObj.ChangeMonocLed = b),
                new AudioLedOption((b) => argsObj.ChangeAudioLed = b),
                new HelpOption(),
            };

            options.Parse(args);
            return argsObj;
        }
    }
}
