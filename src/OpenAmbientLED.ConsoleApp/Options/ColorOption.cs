using Mono.Options;
using System;
using System.Drawing;

namespace OpenAmbientLED.ConsoleApp.Options
{
    internal class ColorOption : Option
    {
        private const string description = "set the color value \n" + "color name/hex";

        private readonly Action<uint> _action;
        public ColorOption(Action<uint> action) : base("c|color=", description)
        {
            _action = action;
        }

        protected override void OnParseComplete(OptionContext c)
        {
            var input = c.OptionValues[0];

            var realColor = Color.FromName(input.Trim());
            if (realColor.A == 0)
                realColor = Color.FromArgb(0xff, Color.FromArgb(int.Parse(input, System.Globalization.NumberStyles.HexNumber)));

            _action((uint)realColor.ToArgb());
        }
    }
}
