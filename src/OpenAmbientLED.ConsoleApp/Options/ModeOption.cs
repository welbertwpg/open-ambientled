using Mono.Options;
using OpenAmbientLED.ConsoleApp.Parsers;
using OpenAmbientLED.Enums;
using System;

namespace OpenAmbientLED.ConsoleApp.Options
{
    internal class ModeOption : Option
    {
        private const string description = "set led mode \n" +
            "off, darkoff, still, breath, auto, flash, random, wave, scene, condition, dflash, colorcycle";

        private readonly Action<LedMode> _action;

        public ModeOption(Action<LedMode> action) : base("m|mode=", description)
        {
            _action = action;
        }

        protected override void OnParseComplete(OptionContext c)
        {
            var ledMode = LedModeParser.Parse(c.OptionValues[0]);
            _action(ledMode);
        }
    }
}
