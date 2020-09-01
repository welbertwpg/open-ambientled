using Mono.Options;
using OpenAmbientLED.ConsoleApp.Parsers;
using OpenAmbientLED.Controllers;
using OpenAmbientLED.Enums;
using OpenAmbientLED.External;
using System;

namespace OpenAmbientLED.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var argsObj = ArgsParser.Parse(args);

                if (argsObj.ChangeAudioLed || argsObj.ChangeMonocLed)
                {
                    InvkSMBCtrl.LibInitial();

                    if (argsObj.ChangeAudioLed && argsObj.Mode.HasValue)
                    {
                        var audioled = AudioLedController.Create();
                        if (audioled != null)
                            audioled.SetMode(argsObj.Mode.Value);
                    }

                    if (argsObj.ChangeMonocLed)
                    {
                        var monocLed = MonocLedController.Create();
                        if (monocLed == null)
                            return;

                        if (argsObj.Mode.HasValue)
                            monocLed.SetMode(argsObj.Mode.Value);

                        if (argsObj.Color.HasValue)
                        {
                            if (argsObj.Mode == LedMode.Off || argsObj.Mode == LedMode.Unknown)
                                return;

                            monocLed.SetColor(argsObj.Color.Value);
                        }
                    }
                }
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `--help' for more information.");
            }
        }
    }
}
