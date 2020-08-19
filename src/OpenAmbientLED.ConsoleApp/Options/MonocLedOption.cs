using Mono.Options;
using System;

namespace OpenAmbientLED.ConsoleApp.Options
{
    internal class MonocLedOption : Option
    {
        private readonly Action<bool> _action;
        public MonocLedOption(Action<bool> action) : base("ml|mled|monocled", "set the configuration for monocled")
        {
            _action = action;
        }

        protected override void OnParseComplete(OptionContext c)
        {
            _action(true);
        }
    }
}