using Microsoft.Win32;
using monocled;
using OpenAmbientLED.Enums;
using OpenAmbientLED.External;
using OpenAmbientLED.Interfaces;
using System;

namespace OpenAmbientLED.Controllers
{
    public class MonocLedController : IRgbLedController
    {
        private readonly MncLedCtrl controller;
        private readonly uint ledIdv4;
        private readonly uint ledFuncType;

        public MonocLedController()
        {
            var ledIdv3 = InvkSMBCtrl.GetSIVId();
            ledIdv4 = InvkSMBCtrl.GetLEDId();

            if (ledIdv4 != 0)
            {
                var siv_Pid = ledIdv3 >> 28;
                ledFuncType = ((ledIdv4 >> 20) & 0xF);

                if (IsLedFunctionNotSuported(siv_Pid))
                    throw new NotSupportedException("Led function not supported");

                if (siv_Pid == 4)
                {
                    var ledCtrlBy = GetLedCtrlBy(siv_Pid, ledFuncType);
                    if (ledCtrlBy == LedCtrlBy.Unknown)
                        throw new NotSupportedException("Unknown controller");

                    if (ledCtrlBy == LedCtrlBy.IT8688)
                    {
                        controller = new MncLedCtrl
                        {
                            iManufacter = (int)GetPlatform(),
                            uPid = siv_Pid,
                            uChipId = ledIdv4 >> 27,
                            GpioPinConfigType = ((ledIdv4 >> 16) & 0xF)
                        };
                    }
                }
            }
        }

        private bool IsLedFunctionNotSuported(uint siv_Pid)
        {
            if (siv_Pid == 4)
            {
                var MB_Id = (MBIdentify)InvkSMBCtrl.GetMBId();
                switch (MB_Id)
                {
                    case MBIdentify.I_Z370:
                    case MBIdentify.I_Z390:
                        break;
                    default:
                        return true;
                }
            }
            return false;
        }

        private LedCtrlBy GetLedCtrlBy(uint siv_Pid, uint ledFuncType)
        {
            if (siv_Pid == 4)
            {
                if (ledFuncType > 6)
                    return LedCtrlBy.IT8297;
                else if (ledFuncType > 3 && ledFuncType < 7)
                    return LedCtrlBy.IT8688;
                else if (ledFuncType != 0 && ledFuncType < 4)
                    return LedCtrlBy.CannonLake_PCH;
            }
            return LedCtrlBy.Unknown;
        }

        private Platform GetPlatform()
        {
            try
            {
                string name = "HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0";
                using (var registryKey = Registry.LocalMachine.OpenSubKey(name))
                {
                    if (registryKey != null)
                    {
                        string text = (string)registryKey.GetValue("ProcessorNameString", "");
                        if (!string.IsNullOrEmpty(text))
                        {
                            if (text.ToLower().Contains("intel"))
                                return Platform.Intel;

                            if (text.ToLower().Contains("amd"))
                                return Platform.AMD;
                        }
                    }

                    return Platform.Unknown;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("[ex391]: GetPlatform(), " + ex.Message);
                throw;
            }
        }

        public void SetMode(LedMode mode)
        {
            var iDiv = ledFuncType == 4 ? 1 : -1;
            switch (mode)
            {
                case LedMode.Off:
                    controller.SetLedMode_2(iDiv, 0, ledIdv4);
                    break;
                case LedMode.Still:
                    controller.SetLedMode_2(iDiv, 2, ledIdv4);
                    break;
                case LedMode.Breath:
                    controller.SetLedMode_2(iDiv, 3, ledIdv4);
                    break;
                case LedMode.Beat:
                    controller.SetLedMode_2(iDiv, 4, ledIdv4);
                    break;
                case LedMode.ColorCycle:
                    controller.SetLedMode_2(iDiv, 12, ledIdv4);
                    break;
                case LedMode.Flash:
                    controller.SetLedMode_2(iDiv, 6, ledIdv4);
                    break;
                case LedMode.DFlash:
                    controller.SetLedMode_2(iDiv, 11, ledIdv4);
                    break;
                case LedMode.Random:
                    controller.SetLedMode_2(iDiv, 7, ledIdv4);
                    break;
                default:
                    throw new NotSupportedException("Mode not supported by MonocLed");
            }
        }

        public void SetColor(uint color)
            => controller.SetLedMode_1(-1, -1, color, ledIdv4);
    }
}
