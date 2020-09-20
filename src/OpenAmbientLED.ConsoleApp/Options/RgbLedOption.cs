using Mono.Options;
using System;

namespace OpenAmbientLED.ConsoleApp.Options
{
    internal class RgbLedOption : Option
    {
        private readonly Action<bool> _action;
        public RgbLedOption(Action<bool> action) : base("rgb", "set the configuration for rgb led")
        {
            _action = action;
        }

        protected override void OnParseComplete(OptionContext c)
        {
            _action(true);
        }
    }
}