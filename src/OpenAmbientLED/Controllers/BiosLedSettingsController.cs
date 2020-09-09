using OpenAmbientLED.Enums;
using OpenAmbientLED.External;
using OpenAmbientLED.Interfaces;
using System;

namespace OpenAmbientLED.Controllers
{
    public class BiosLedSettingsController : IRgbLedController
    {
        public BiosLedSettingsController()
        {
            InvkCled.piCLed_Init();
            InvkCled.piChkEzSetupSupport();
        }

        public void SetColor(uint color)
        {
            try
            {
                uint iVal = color & 0xFFFFFF;
                InvkCled.piSetTrueColorLedMode(iVal);

                uint iVal2 = color & 0xFFFFFF;
                InvkCled.piSetTrueColorLedMode_1(iVal2);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("[ex123]:Save LED setting to BIOS fail, " + ex.Message);
            }
        }

        public void SetMode(LedMode mode)
        {
            try
            {
                switch (mode)
                {
                    case LedMode.Off:
                        Off:
                        InvkCled.piSetAzaliaLedMode(0);
                        InvkCled.piSetBiosOnOff(0);
                        break;
                    case LedMode.Breath:
                        InvkCled.piSetAzaliaLedMode(1);
                        InvkCled.piSetBiosOnOff(1);
                        break;
                    case LedMode.ColorCycle:
                        InvkCled.piSetAzaliaLedMode(2);
                        InvkCled.piSetBiosOnOff(1);
                        break;
                    case LedMode.Still:
                        InvkCled.piSetAzaliaLedMode(3);
                        InvkCled.piSetBiosOnOff(1);
                        break;
                    case LedMode.Flash:
                        InvkCled.piSetAzaliaLedMode(4);
                        InvkCled.piSetBiosOnOff(1);
                        break;
                    case LedMode.DoubleFlash:
                        InvkCled.piSetAzaliaLedMode(5);
                        InvkCled.piSetBiosOnOff(1);
                        break;
                    default:
                        goto Off;
                }
                InvkCled.piSetFunction();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("[ex123]:Save LED setting to BIOS fail, " + ex.Message);
            }
        }
    }
}
