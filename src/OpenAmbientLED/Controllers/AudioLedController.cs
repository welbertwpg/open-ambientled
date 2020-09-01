using OpenAmbientLED.Drivers;
using OpenAmbientLED.Enums;
using OpenAmbientLED.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OpenAmbientLED.Controllers
{
    public class AudioLedController : ILedController
    {
        private readonly IT8620 controller;
        private readonly IEnumerable<LedMode> supportedModes = new[] { LedMode.Off, LedMode.Still, LedMode.Breath };

        public static ILedController Create()
        {
            try
            {
                return new AudioLedController();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return null;
            }
        }

        private AudioLedController()
        {
            var finder = new IT87XXFinder();
            for (int i = 0; i < 6; i++)
            {
                finder.Find(bMainController: true, out IT87XX controller);

                if (controller != null && controller is IT8620)
                {
                    this.controller = controller as IT8620;
                    return;
                }

                Thread.Sleep(1000);
            }

            throw new NotSupportedException("The IT8620 controller wasn't found");
        }

        public void SetMode(LedMode mode)
        {
            if (!supportedModes.Contains(mode))
                throw new NotSupportedException("Mode not supported by AudioLed");

            controller.SetLedMode_PinConfigType1(mode);
        }
    }
}
