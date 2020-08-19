using OpenAmbientLED.Enums;
using OpenAmbientLED.External;
using System;
using System.Collections.Generic;

namespace OpenAmbientLED.Drivers
{
    internal abstract class IT87XX
    {
        protected const int VAL_FAN_CONTROL_1 = 0;

        protected const int VAL_FAN_CONTROL_2 = 1;

        protected const int VAL_FAN_CONTROL_3 = 2;

        protected const int VAL_FAN_CONTROL_4 = 3;

        protected const int VAL_FAN_CONTROL_5 = 4;

        protected const int VAL_FAN_CONTROL_6 = 5;

        protected const byte ITE_VENDOR_ID = 144;

        protected const byte ADDRESS_REGISTER_OFFSET = 5;

        protected const byte DATA_REGISTER_OFFSET = 6;

        protected const byte CONFIGURATION_REGISTER = 0;

        protected const byte FAN_TACHOMETER_DIVISOR_REGISTER = 11;

        protected const byte FAN_PWM_SMOOTHING_STEP_FREQUENCY_SELECTION_REGISTER = 11;

        protected const byte FAN_TACHOMETER_CONTROL_REGISTER = 12;

        protected const byte FAN_CONTROLLER_MAIN_CONTROL_REGISTER = 19;

        protected const byte VENDOR_ID_REGISTER = 88;

        public Chip chip
        {
            get;
            protected set;
        }

        public byte Version
        {
            get;
            protected set;
        }

        public int MaxPwmValue
        {
            get;
            protected set;
        }

        public int FanControlRegisterCount
        {
            get;
            protected set;
        }

        public int VoltageCount
        {
            get;
            protected set;
        }

        public int TemperatureCount
        {
            get;
            protected set;
        }

        public int GpioCount
        {
            get;
            protected set;
        }

        public ushort BaseAddress
        {
            get;
            protected set;
        }

        public ushort AddressRegister
        {
            get;
            protected set;
        }

        public ushort DataRegister
        {
            get;
            protected set;
        }

        public ushort GpioAddress
        {
            get;
            protected set;
        }

        public float VoltageGain
        {
            get;
            protected set;
        }

        public bool Has16bitFanCounter
        {
            get;
            protected set;
        }

        protected byte ReadByte(byte register, out bool valid)
        {
            InvkYcc.gb_outp(AddressRegister, register);
            byte result = InvkYcc.gb_inp(DataRegister);
            if (chip == Chip.IT8688)
            {
                valid = true;
            }
            else
            {
                valid = (register == InvkYcc.gb_inp(AddressRegister));
            }
            return result;
        }

        protected bool WriteByte(byte register, byte value)
        {
            InvkYcc.gb_outp(AddressRegister, register);
            InvkYcc.gb_outp(DataRegister, value);
            if (chip == Chip.IT8688)
            {
                return true;
            }
            return register == InvkYcc.gb_inp(AddressRegister);
        }
    }
}
