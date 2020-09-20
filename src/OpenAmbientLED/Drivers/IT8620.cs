using OpenAmbientLED.Enums;
using OpenAmbientLED.External;
using System;
using System.Threading;

namespace OpenAmbientLED.Drivers
{
    internal class IT8620 : IT87XX
    {
        protected const byte PROCHOT_CONTROL_REGISTER = 103;

        protected const byte GPIO1X_CONTROL_REGISTER = 0;

        protected const byte GPIO2X_CONTROL_REGISTER = 1;

        protected const byte GPIO3X_CONTROL_REGISTER = 2;

        protected const byte GPIO7X_CONTROL_REGISTER = 6;

        protected const byte GPIO8X_CONTROL_REGISTER = 7;

        protected const byte LDN_SELECT_REGISTER = 7;

        protected const byte IT87_GPIO_LDN = 7;

        protected const byte CONFIGURATION_CONTROL_REGISTER = 2;

        protected const byte LED_BLINKING_CONTROL_REGISTER = 251;

        protected const byte INPUT_OUTPUT_SELECT_REGISTER_1X = 200;

        protected const byte INPUT_OUTPUT_SELECT_REGISTER_2X = 201;

        protected const byte INPUT_OUTPUT_SELECT_REGISTER_3X = 202;

        protected const byte INPUT_OUTPUT_SELECT_REGISTER_7X = 206;

        protected const byte INPUT_OUTPUT_SELECT_REGISTER_8X = 207;

        protected const byte INPUT_OUTPUT_CONTROL_REGISTER_9X = 208;

        protected const byte INPUT_OUTPUT_SELECT_REGISTER_9X = 210;

        protected const byte SIMPLE_ENABLE_REGISTER_9X = 211;

        protected readonly ushort[] REGISTER_PORTS = new ushort[2]
        {
            46,
            78
        };

        protected readonly ushort[] VALUE_PORTS = new ushort[2]
        {
            47,
            79
        };

        private Mutex resMutex;

        public ushort RegisterPort
        {
            get;
            protected set;
        }

        public ushort ValuePort
        {
            get;
            protected set;
        }

        public string MutexName4MBPnPMode
        {
            get;
            protected set;
        }

        public TimeSpan MutexTimeout4MBPnPMode
        {
            get;
            protected set;
        }

        public IT8620(Chip chip, ushort baseAddress, ushort gpioAddress, byte version)
        {
            if (chip != Chip.IT8620E && chip != Chip.IT8626 && chip != Chip.IT8686E && chip != Chip.IT8688)
                throw new NotSupportedException();

            resMutex = null;
            MutexName4MBPnPMode = "MBPnPModeMutex";
            MutexTimeout4MBPnPMode = TimeSpan.FromMilliseconds(500.0);
            BaseAddress = baseAddress;
            base.chip = chip;
            Version = version;
            AddressRegister = (ushort)(baseAddress + 5);
            DataRegister = (ushort)(baseAddress + 6);
            GpioAddress = gpioAddress;
            RegisterPort = REGISTER_PORTS[0];
            ValuePort = VALUE_PORTS[0];
        }

        public void SetLedMode_PinConfigType1(LedMode pMode)
        {
            switch (pMode)
            {
                case LedMode.DarkOff:
                    break;
                case LedMode.Off:
                case LedMode.Still:
                case LedMode.Breath:
                    UpdateAudioLedMode(pMode);
                    break;
            }
        }

        public void SetLedMode_PinConfigType2(LedMode pMode)
        {
            switch (pMode)
            {
                case LedMode.DarkOff:
                    break;
                case LedMode.Off:
                case LedMode.Still:
                case LedMode.Breath:
                case LedMode.Beat:
                    UpdateAudioLedMode2(pMode);
                    break;
            }
        }

        public void CtrlGPIO2X(int gp20, int gp21, int gp22)
        {
            IT87Enter(RegisterPort);
            Select(RegisterPort, ValuePort, 7);
            byte b = ReadByte(RegisterPort, ValuePort, 201);
            b = (byte)(b | 7);
            WriteByte(RegisterPort, ValuePort, 201, b);
            IT87Exit(RegisterPort, ValuePort);
            byte b2 = InvkYcc.gb_inp((uint)(GpioAddress + 1));
            b2 = ((gp22 != 1) ? ((byte)(b2 & 0xFB)) : ((byte)(b2 | 4)));
            b2 = ((gp21 != 1) ? ((byte)(b2 & 0xFD)) : ((byte)(b2 | 2)));
            b2 = ((gp20 != 1) ? ((byte)(b2 & 0xFE)) : ((byte)(b2 | 1)));
            InvkYcc.gb_outp((uint)(GpioAddress + 1), b2);
        }

        public void Init_IT86xx()
        {
            IT87Enter(RegisterPort);
            Select(RegisterPort, ValuePort, 0);
            WriteByte(RegisterPort, ValuePort, 176, 240);
            WriteByte(RegisterPort, ValuePort, 178, 32);
            WriteByte(RegisterPort, ValuePort, 182, byte.MaxValue);
            WriteByte(RegisterPort, ValuePort, 192, 240);
            WriteByte(RegisterPort, ValuePort, 194, 16);
            WriteByte(RegisterPort, ValuePort, 198, byte.MaxValue);
            IT87Exit(RegisterPort, ValuePort);
        }

        public void LedCtrl_IT86xx(int Divison, LedMode lMode, uint _iColor, uint uiLedID)
        {
            bool changeDiv1 = false, changeDiv2 = false;
            switch (Divison)
            {
                case 0:
                    changeDiv1 = true;
                    break;
                case 1:
                    changeDiv2 = true;
                    break;
                default:
                    changeDiv1 = true;
                    changeDiv2 = true;
                    break;
            }

            IT87Enter(RegisterPort);
            Select(RegisterPort, ValuePort, 0);
            
            switch (lMode)
            {
                case LedMode.Off:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 15);
                        WriteByte(RegisterPort, ValuePort, 177, 0);
                        WriteByte(RegisterPort, ValuePort, 178, 32);
                        WriteByte(RegisterPort, ValuePort, 179, 0);
                        WriteByte(RegisterPort, ValuePort, 180, 0);
                        WriteByte(RegisterPort, ValuePort, 181, 0);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 15);
                        WriteByte(RegisterPort, ValuePort, 193, 0);
                        WriteByte(RegisterPort, ValuePort, 194, 16);
                        WriteByte(RegisterPort, ValuePort, 195, 0);
                        WriteByte(RegisterPort, ValuePort, 196, 0);
                        WriteByte(RegisterPort, ValuePort, 197, 0);
                    }
                    break;
                case LedMode.Still:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 15);
                        WriteByte(RegisterPort, ValuePort, 177, 0);
                        WriteByte(RegisterPort, ValuePort, 178, 32);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 15);
                        WriteByte(RegisterPort, ValuePort, 193, 0);
                        WriteByte(RegisterPort, ValuePort, 194, 16);
                    }
                    break;
                case LedMode.Breath:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 143);
                        WriteByte(RegisterPort, ValuePort, 177, 0);
                        WriteByte(RegisterPort, ValuePort, 178, 32);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 143);
                        WriteByte(RegisterPort, ValuePort, 193, 0);
                        WriteByte(RegisterPort, ValuePort, 194, 16);
                    }
                    break;
                case LedMode.Flash:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 15);
                        WriteByte(RegisterPort, ValuePort, 177, 8);
                        WriteByte(RegisterPort, ValuePort, 178, 32);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 15);
                        WriteByte(RegisterPort, ValuePort, 193, 8);
                        WriteByte(RegisterPort, ValuePort, 194, 16);
                    }
                    break;
                case LedMode.DoubleFlash:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 15);
                        WriteByte(RegisterPort, ValuePort, 177, 12);
                        WriteByte(RegisterPort, ValuePort, 178, 32);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 15);
                        WriteByte(RegisterPort, ValuePort, 193, 12);
                        WriteByte(RegisterPort, ValuePort, 194, 16);
                    }
                    break;
                case LedMode.ColorCycle:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 127);
                        WriteByte(RegisterPort, ValuePort, 177, 0);
                        WriteByte(RegisterPort, ValuePort, 178, 160);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 127);
                        WriteByte(RegisterPort, ValuePort, 193, 0);
                        WriteByte(RegisterPort, ValuePort, 194, 144);
                    }
                    break;
                case LedMode.Random:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 15);
                        WriteByte(RegisterPort, ValuePort, 177, 64);
                        WriteByte(RegisterPort, ValuePort, 178, 32);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 15);
                        WriteByte(RegisterPort, ValuePort, 193, 64);
                        WriteByte(RegisterPort, ValuePort, 194, 16);
                    }
                    break;
            }
            
            if (lMode != LedMode.Auto)
            {
                byte value = (byte)(_iColor >> 16);
                byte value2 = (byte)(_iColor >> 8);
                byte value3 = (byte)_iColor;
                if (changeDiv1)
                {
                    WriteByte(RegisterPort, ValuePort, 179, value);
                    WriteByte(RegisterPort, ValuePort, 180, value2);
                    WriteByte(RegisterPort, ValuePort, 181, value3);
                }
                if (changeDiv2)
                {
                    WriteByte(RegisterPort, ValuePort, 195, value);
                    WriteByte(RegisterPort, ValuePort, 196, value2);
                    WriteByte(RegisterPort, ValuePort, 197, value3);
                }
            }
            
            IT87Exit(RegisterPort, ValuePort);
        }

        public void LedCtrl_IT86xx_SetMode(int division, LedMode mode)
        {
            bool changeDiv1 = false, changeDiv2 = false;
            switch (division)
            {
                case 0:
                    changeDiv1 = true;
                    break;
                case 1:
                    changeDiv2 = true;
                    break;
                default:
                    changeDiv1 = true;
                    changeDiv2 = true;
                    break;
            }

            IT87Enter(RegisterPort);
            Select(RegisterPort, ValuePort, 0);
            
            switch (mode)
            {
                case LedMode.Off:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 15);
                        WriteByte(RegisterPort, ValuePort, 177, 0);
                        WriteByte(RegisterPort, ValuePort, 178, 32);
                        WriteByte(RegisterPort, ValuePort, 179, 0);
                        WriteByte(RegisterPort, ValuePort, 180, 0);
                        WriteByte(RegisterPort, ValuePort, 181, 0);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 15);
                        WriteByte(RegisterPort, ValuePort, 193, 0);
                        WriteByte(RegisterPort, ValuePort, 194, 16);
                        WriteByte(RegisterPort, ValuePort, 195, 0);
                        WriteByte(RegisterPort, ValuePort, 196, 0);
                        WriteByte(RegisterPort, ValuePort, 197, 0);
                    }
                    break;
                case LedMode.Still:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 15);
                        WriteByte(RegisterPort, ValuePort, 177, 0);
                        WriteByte(RegisterPort, ValuePort, 178, 32);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 15);
                        WriteByte(RegisterPort, ValuePort, 193, 0);
                        WriteByte(RegisterPort, ValuePort, 194, 16);
                    }
                    break;
                case LedMode.Breath:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 143);
                        WriteByte(RegisterPort, ValuePort, 177, 0);
                        WriteByte(RegisterPort, ValuePort, 178, 32);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 143);
                        WriteByte(RegisterPort, ValuePort, 193, 0);
                        WriteByte(RegisterPort, ValuePort, 194, 16);
                    }
                    break;
                case LedMode.Flash:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 15);
                        WriteByte(RegisterPort, ValuePort, 177, 8);
                        WriteByte(RegisterPort, ValuePort, 178, 32);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 15);
                        WriteByte(RegisterPort, ValuePort, 193, 8);
                        WriteByte(RegisterPort, ValuePort, 194, 16);
                    }
                    break;
                case LedMode.DoubleFlash:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 15);
                        WriteByte(RegisterPort, ValuePort, 177, 12);
                        WriteByte(RegisterPort, ValuePort, 178, 32);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 15);
                        WriteByte(RegisterPort, ValuePort, 193, 12);
                        WriteByte(RegisterPort, ValuePort, 194, 16);
                    }
                    break;
                case LedMode.ColorCycle:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 127);
                        WriteByte(RegisterPort, ValuePort, 177, 0);
                        WriteByte(RegisterPort, ValuePort, 178, 160);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 127);
                        WriteByte(RegisterPort, ValuePort, 193, 0);
                        WriteByte(RegisterPort, ValuePort, 194, 144);
                    }
                    break;
                case LedMode.Random:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 15);
                        WriteByte(RegisterPort, ValuePort, 177, 64);
                        WriteByte(RegisterPort, ValuePort, 178, 32);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 15);
                        WriteByte(RegisterPort, ValuePort, 193, 64);
                        WriteByte(RegisterPort, ValuePort, 194, 16);
                    }
                    break;
                case LedMode.Beat:
                    if (changeDiv1)
                    {
                        WriteByte(RegisterPort, ValuePort, 176, 15);
                        WriteByte(RegisterPort, ValuePort, 177, 128);
                    }
                    if (changeDiv2)
                    {
                        WriteByte(RegisterPort, ValuePort, 192, 15);
                        WriteByte(RegisterPort, ValuePort, 193, 128);
                    }
                    break;
            }

            IT87Exit(RegisterPort, ValuePort);
        }

        public void LedCtrl_IT86xx_SetColor(int divison, uint _iColorPart1, uint _iColorPart2)
        {
            bool changeDiv1 = false, changeDiv2 = false;
            switch (divison)
            {
                case 0:
                    changeDiv1 = true;
                    break;
                case 1:
                    changeDiv2 = true;
                    break;
                default:
                    changeDiv1 = true;
                    changeDiv2 = true;
                    break;
            }

            IT87Enter(RegisterPort);
            Select(RegisterPort, ValuePort, 0);

            if (changeDiv1)
            {
                WriteByte(RegisterPort, ValuePort, 179, (byte)(_iColorPart1 >> 16));
                WriteByte(RegisterPort, ValuePort, 180, (byte)(_iColorPart1 >> 8));
                WriteByte(RegisterPort, ValuePort, 181, (byte)_iColorPart1);
            }

            if (changeDiv2)
            {
                WriteByte(RegisterPort, ValuePort, 195, (byte)(_iColorPart2 >> 16));
                WriteByte(RegisterPort, ValuePort, 196, (byte)(_iColorPart2 >> 8));
                WriteByte(RegisterPort, ValuePort, 197, (byte)_iColorPart2);
            }

            IT87Exit(RegisterPort, ValuePort);
        }

        protected void IT87Enter(ushort registerPort)
        {
            bool createdNew;
            resMutex = new Mutex(initiallyOwned: true, MutexName4MBPnPMode, out createdNew);
            if (!createdNew)
            {
                resMutex.WaitOne(MutexTimeout4MBPnPMode);
            }
            switch (registerPort)
            {
                case 46:
                    InvkYcc.gb_outp(registerPort, 135);
                    InvkYcc.gb_outp(registerPort, 1);
                    InvkYcc.gb_outp(registerPort, 85);
                    InvkYcc.gb_outp(registerPort, 85);
                    break;
                case 78:
                    InvkYcc.gb_outp(registerPort, 135);
                    InvkYcc.gb_outp(registerPort, 1);
                    InvkYcc.gb_outp(registerPort, 85);
                    InvkYcc.gb_outp(registerPort, 170);
                    break;
            }
        }

        protected void IT87Exit(ushort registerPort, ushort valuePort)
        {
            InvkYcc.gb_outp(registerPort, 2);
            InvkYcc.gb_outp(valuePort, 2);
            if (resMutex != null)
            {
                resMutex.ReleaseMutex();
                resMutex = null;
            }
        }

        protected void Select(ushort registerPort, ushort valuePort, byte logicalDeviceNumber)
        {
            InvkYcc.gb_outp(registerPort, 7);
            InvkYcc.gb_outp(valuePort, logicalDeviceNumber);
        }

        protected byte ReadByte(ushort registerPort, ushort valuePort, byte register)
        {
            InvkYcc.gb_outp(registerPort, register);
            return InvkYcc.gb_inp(valuePort);
        }

        protected void WriteByte(ushort registerPort, ushort valuePort, byte register, byte value)
        {
            InvkYcc.gb_outp(registerPort, register);
            InvkYcc.gb_outp(valuePort, value);
        }

        protected ushort ReadWord(ushort registerPort, ushort valuePort, byte register)
        {
            return (ushort)((ReadByte(registerPort, valuePort, register) << 8) | ReadByte(registerPort, valuePort, (byte)(register + 1)));
        }

        private void UpdateAudioLedMode(LedMode audioLedMode)
        {
            IT87Enter(RegisterPort);
            Select(RegisterPort, ValuePort, 7);
            byte b = ReadByte(RegisterPort, ValuePort, 251);
            byte b2 = ReadByte(RegisterPort, ValuePort, 210);
            byte b3 = ReadByte(RegisterPort, ValuePort, 208);
            switch (audioLedMode)
            {
                case LedMode.Off:
                    b2 = (byte)(b2 | 2);
                    WriteByte(RegisterPort, ValuePort, 210, b2);
                    b3 = (byte)(b3 & 0xFD);
                    InvkSMBCtrl.Pch_D22_Ctrl(0);
                    b = (byte)(b & 0x6F);
                    break;
                case LedMode.Still:
                    b2 = (byte)(b2 | 2);
                    WriteByte(RegisterPort, ValuePort, 210, b2);
                    b3 = (byte)(b3 & 0xFD);
                    InvkSMBCtrl.Pch_D22_Ctrl(1);
                    b = (byte)(b & 0x6F);
                    break;
                case LedMode.Breath:
                    b2 = (byte)(b2 | 2);
                    WriteByte(RegisterPort, ValuePort, 210, b2);
                    b3 = (byte)(b3 & 0xFD);
                    InvkSMBCtrl.Pch_D22_Ctrl(1);
                    b = (byte)(b | 0x90);
                    break;
            }
            WriteByte(RegisterPort, ValuePort, 208, b3);
            WriteByte(RegisterPort, ValuePort, 251, b);
            IT87Exit(RegisterPort, ValuePort);
        }

        private void UpdateAudioLedMode2(LedMode audioLedMode)
        {
            IT87Enter(RegisterPort);
            Select(RegisterPort, ValuePort, 7);
            byte b = ReadByte(RegisterPort, ValuePort, 251);
            byte b2 = ReadByte(RegisterPort, ValuePort, 202);
            b2 = (byte)(b2 | 0x20);
            WriteByte(RegisterPort, ValuePort, 202, b2);
            byte b4 = InvkYcc.gb_inp((uint)(GpioAddress + 2));
            byte b3 = ReadByte(RegisterPort, ValuePort, 210);
            byte b5 = ReadByte(RegisterPort, ValuePort, 208);
            
            switch (audioLedMode)
            {
                case LedMode.Off:
                    b3 = (byte)(b3 | 0x20);
                    WriteByte(RegisterPort, ValuePort, 210, b3);
                    b5 = (byte)(b5 & 0xDF);
                    b4 = (byte)(b4 & 0xDF);
                    b = (byte)(b & 0x6F);
                    break;
                case LedMode.Still:
                    b3 = (byte)(b3 | 0x20);
                    WriteByte(RegisterPort, ValuePort, 210, b3);
                    b5 = (byte)(b5 | 0x20);
                    b4 = (byte)(b4 & 0xDF);
                    b = (byte)(b & 0x6F);
                    break;
                case LedMode.Breath:
                    b3 = (byte)(b3 | 0x20);
                    WriteByte(RegisterPort, ValuePort, 210, b3);
                    b5 = (byte)(b5 | 0x20);
                    b4 = (byte)(b4 & 0xDF);
                    b = (byte)(b | 0x90);
                    break;
                case LedMode.Beat:
                    b3 = (byte)(b3 | 0x20);
                    WriteByte(RegisterPort, ValuePort, 210, b3);
                    b5 = (byte)(b5 | 0x20);
                    b4 = (byte)(b4 | 0x20);
                    b = (byte)(b & 0x6F);
                    break;
            }

            WriteByte(RegisterPort, ValuePort, 208, b5);
            InvkYcc.gb_outp((uint)(GpioAddress + 2), b4);
            WriteByte(RegisterPort, ValuePort, 251, b);
            IT87Exit(RegisterPort, ValuePort);
        }
    }
}
