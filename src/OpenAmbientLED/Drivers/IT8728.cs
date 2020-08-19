using OpenAmbientLED.Enums;
using System;

namespace OpenAmbientLED.Drivers
{
    internal class IT8728 : IT87XX
    {
        public IT8728(Chip chip, ushort baseAddress, ushort gpioAddress, byte version)
        {
            if (chip != Chip.IT8728F)
                throw new NotSupportedException();

            BaseAddress = baseAddress;
            base.chip = chip;
            Version = version;
            AddressRegister = (ushort)(baseAddress + 5);
            DataRegister = (ushort)(baseAddress + 6);
            GpioAddress = gpioAddress;
            byte b = ReadByte(88, out bool valid);
            if (valid && b == 144 && (ReadByte(0, out valid) & 0x10) != 0 && valid)
            {
                VoltageGain = 0.012f;
                Has16bitFanCounter = true;
                GpioCount = 0;
                FanControlRegisterCount = 3;
                TemperatureCount = 3;
                VoltageCount = 7;
                MaxPwmValue = 255;
            }
        }
    }
}
