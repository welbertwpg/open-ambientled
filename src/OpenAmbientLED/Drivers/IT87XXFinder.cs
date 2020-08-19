using OpenAmbientLED.Enums;
using OpenAmbientLED.External;
using System;
using System.Threading;

namespace OpenAmbientLED.Drivers
{
    internal class IT87XXFinder
    {
        protected const byte CONFIGURATION_CONTROL_REGISTER = 2;

        protected const byte LDN_SELECT_REGISTER = 7;

        protected const byte CHIP_ID_REGISTER = 32;

        protected const byte BASE_ADDRESS_REGISTER = 96;

        protected const byte EXTENDED2_MULTI_FUNCTION_PIN_SELECTION_REGISTER = 44;

        protected const byte IT87_ENVIRONMENT_CONTROLLER_LDN = 4;

        protected const byte IT87_GPIO_LDN = 7;

        protected const byte IT87_CHIP_VERSION_REGISTER = 34;

        protected const byte IT87_SHARE_MEMORY_FLASH_INTERFACE_CONFIGURATION_REGISTER = 15;

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

        private object m_findLock;

        public Chip ChipID
        {
            get;
            protected set;
        }

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

        public IT87XXFinder()
        {
            ChipID = Chip.Unknown;
            RegisterPort = REGISTER_PORTS[0];
            ValuePort = VALUE_PORTS[0];
            if (m_findLock == null)
            {
                m_findLock = new object();
            }
            MutexName4MBPnPMode = "MBPnPModeMutex";
            MutexTimeout4MBPnPMode = TimeSpan.FromMilliseconds(500.0);
        }

        public bool Find(bool bMainController, out IT87XX pEnvironmentController)
        {
            pEnvironmentController = null;
            lock (m_findLock)
            {
                try
                {
                    ChipID = Chip.Unknown;
                    if (bMainController)
                        FindMainEnvironmentController(out pEnvironmentController);

                    return ((pEnvironmentController != null) ? true : false);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool IsExist()
        {
            bool result = false;
            lock (m_findLock)
            {
                try
                {
                    ChipID = Chip.Unknown;
                    FindMainEnvironmentController(out IT87XX pEnvironmentController);
                    result = ((pEnvironmentController != null) ? true : false);
                    pEnvironmentController = null;
                    return result;
                }
                catch (Exception)
                {
                    return result;
                }
            }
        }

        protected void IT87Enter(ushort registerPort)
        {
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
        }

        private bool FindMainEnvironmentController(out IT87XX pEnvironmentController)
        {
            RegisterPort = REGISTER_PORTS[0];
            ValuePort = VALUE_PORTS[0];
            return FindMainEnvironmentController(REGISTER_PORTS[0], VALUE_PORTS[0], out pEnvironmentController);
        }

        private bool FindMainEnvironmentController(ushort registerPort, ushort valuePort, out IT87XX pEnvironmentController)
        {
            bool result = false;
            bool flag = false;
            Mutex mutex = null;
            pEnvironmentController = null;
            if (registerPort != 46)
            {
                return result;
            }
            try
            {
                bool createdNew;
                mutex = new Mutex(initiallyOwned: true, MutexName4MBPnPMode, out createdNew);
                if (!createdNew)
                {
                    mutex.WaitOne(MutexTimeout4MBPnPMode);
                }
                IT87Enter(registerPort);
                flag = true;
                ushort num5 = ReadWord(registerPort, valuePort, 32);
                Chip chip;
                switch (num5)
                {
                    case 34336:
                        chip = Chip.IT8620E;
                        break;
                    case 34344:
                        chip = Chip.IT8626;
                        break;
                    case 34438:
                        chip = Chip.IT8686E;
                        break;
                    case 34440:
                        chip = Chip.IT8688;
                        break;
                    case 34600:
                        chip = Chip.IT8728F;
                        break;
                    default:
                        chip = Chip.Unknown;
                        break;
                }

                ChipID = chip;
                if (chip == Chip.Unknown && num5 != 0 && num5 != ushort.MaxValue)
                {
                    IT87Exit(registerPort, valuePort);
                    return result;
                }

                Select(registerPort, valuePort, 4);
                ushort num = ReadWord(registerPort, valuePort, 96);
                
                Thread.Sleep(1);
                
                ushort num2 = ReadWord(registerPort, valuePort, 96);
                byte b = (byte)(ReadByte(registerPort, valuePort, 34) & 0xF);
                Select(registerPort, valuePort, 7);
                ushort num3 = ReadWord(registerPort, valuePort, 98);
                
                Thread.Sleep(1);
                
                ushort num4 = ReadWord(registerPort, valuePort, 98);
                IT87Exit(registerPort, valuePort);
                flag = false;

                if (num != num2 || num < 256 || (num & 0xF007) != 0)
                    return result;

                if (num3 != num4 || num3 < 256 || (num3 & 0xF007) != 0)
                    return result;

                result = true;
                switch (chip)
                {
                    case Chip.IT8620E:
                    case Chip.IT8626:
                    case Chip.IT8686E:
                    case Chip.IT8688:
                        pEnvironmentController = new IT8620(ChipID, num, num3, b);
                        return result;
                    case Chip.IT8728F:
                        pEnvironmentController = new IT8728(ChipID, num, num3, b);
                        return result;
                    default:
                        result = false;
                        return result;
                }
            }
            catch (Exception)
            {
                return result;
            }
            finally
            {
                if (flag)
                {
                    IT87Exit(registerPort, valuePort);
                }
                mutex?.ReleaseMutex();
            }
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

        protected void Select(ushort registerPort, ushort valuePort, byte logicalDeviceNumber)
        {
            InvkYcc.gb_outp(registerPort, 7);
            InvkYcc.gb_outp(valuePort, logicalDeviceNumber);
        }
    }
}
