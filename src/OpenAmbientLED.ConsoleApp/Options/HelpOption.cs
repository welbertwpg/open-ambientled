using Mono.Options;
using System;

namespace OpenAmbientLED.ConsoleApp.Options
{
    internal class HelpOption : Option
    {
        public HelpOption() : base("h|help", "show a help message") { }

        protected override void OnParseComplete(OptionContext c)
        {
            // show some app description message
            Console.WriteLine("Usage: oambientled.exe [OPTIONS]");
            Console.WriteLine("Change the mode/color for Gigabyte AmbientLED motherboard leds.");
            Console.WriteLine();

            // output the options
            Console.WriteLine("Options:");
            c.OptionSet.WriteOptionDescriptions(Console.Out);
        }
    }
}
