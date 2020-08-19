using Mono.Options;
using System;

namespace OpenAmbientLED.ConsoleApp.Options
{
    internal class AudioLedOption : Option
    {
        private readonly Action<bool> _action;
        public AudioLedOption(Action<bool> action) : base("al|aled|audioled", "set the configuration for audioled")
        {
            _action = action;
        }

        protected override void OnParseComplete(OptionContext c)
        {
            _action(true);
        }
    }
}
