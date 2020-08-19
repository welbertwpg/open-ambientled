using System.Runtime.InteropServices;
using System.Text;

namespace OpenAmbientLED.External
{
	public class InvkSMBCtrl
	{
		public const uint ERROR_INVALID_OPERATION = 4317u;

		public const uint ERROR_SUCCESS = 0u;

		private const string ModuleName = "SMBCtrl.dll";

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_LibInitial", ExactSpelling = true)]
		public static extern uint LibInitial();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_GetModelName", ExactSpelling = true)]
		public static extern void GetModelName(byte[] mdName);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_AssignTestId", ExactSpelling = true)]
		public static extern void AssignTestId();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_SetSMBMutex", ExactSpelling = true)]
		public static extern void SetSMBMutex();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_MCU_Rw", ExactSpelling = true)]
		public static extern uint MCU_Rw(byte mcuAddr, byte regOffset, byte val, ref byte pVal, byte rw, uint delayTime = 0u);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_SMB_Word_RW", ExactSpelling = true)]
		public static extern uint SMB_Word_RW(byte mcuAddr, byte regOffset, byte rw, ushort inData, ref ushort outData);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_Skx_SMB_ByteRW", ExactSpelling = true)]
		public static extern uint Skx_SMB_ByteRW(byte controller, byte mcuAddr, byte regOffset, byte val, ref byte pVal, byte rw);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_Skx_SMB_WordRW", ExactSpelling = true)]
		public static extern uint Skx_SMB_WordRW(byte controller, byte mcuAddr, byte regOffset, ushort val, ref ushort pVal, byte rw);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_IT8295_Block_RW", ExactSpelling = true)]
		public static extern uint IT8295_Block_RW(byte mcuAddr, byte regOffset, ref byte len, byte[] datArry, byte rw, uint delayTime = 0u);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_Get_IT8295_FwVer", ExactSpelling = true)]
		public static extern uint Get_IT8295_FwVer(byte mcuAddr, byte regOffset, ref byte len, byte[] datArry);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_GetSIVId", ExactSpelling = true)]
		public static extern uint GetSIVId();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_GetLEDId", ExactSpelling = true)]
		public static extern uint GetLEDId();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_GetMBId", ExactSpelling = true)]
		public static extern int GetMBId();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_IsX299_Kabylake", ExactSpelling = true)]
		public static extern int IsX299_Kabylake();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_ChkEzSetupSupport", ExactSpelling = true)]
		public static extern uint ChkEzSetupSupport();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_GetLedSetupDataLength", ExactSpelling = true)]
		public static extern int GetLedSetupDataLength();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_GetLedPwrSteSetupDataLength", ExactSpelling = true)]
		public static extern int GetLedPwrSteSetupDataLength();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "dllexp_GetLedModeSetupData", ExactSpelling = true)]
		public static extern uint GetLedModeSetupData(StringBuilder lpReturnBuf, int bufLen);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "dllexp_GetLedPwrStateSetupData", ExactSpelling = true)]
		public static extern uint GetLedPwrStateSetupData(StringBuilder lpReturnBuf, int bufLen);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_GetLedModeCurrentValue", ExactSpelling = true)]
		public static extern int GetLedModeCurrentValue();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_GetLedPwrStateCurrentValue", ExactSpelling = true)]
		public static extern int GetLedPwrStateCurrentValue();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_GetTrueColorVal", ExactSpelling = true)]
		public static extern int GetTrueColorVal();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_SetLedModeToBios", ExactSpelling = true)]
		public static extern uint SetLedModeToBios(int iVal);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_SetTrueColorValueToBios", ExactSpelling = true)]
		public static extern uint SetTrueColorValueToBios(uint iVal);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_SetLedPwrStateToBios", ExactSpelling = true)]
		public static extern uint SetLedPwrStateToBios(int iVal);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_SaveToBios", ExactSpelling = true)]
		public static extern uint SaveToBios();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_ReGetSMBusInfo", ExactSpelling = true)]
		public static extern uint ReGetSMBusInfo();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_BeatGpioCtrl", ExactSpelling = true)]
		public static extern uint BeatGpioCtrl(int iCtrl);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_Pch_D0_D1_D2_Ctrl", ExactSpelling = true)]
		public static extern uint Pch_D0_D1_D2_Ctrl(int d0Val, int d1Val, int d2Val);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_DelayMicroSeconds", ExactSpelling = true)]
		public static extern void DelayMicroSeconds(float microseconds);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_Pch_D22_Ctrl", ExactSpelling = true)]
		public static extern uint Pch_D22_Ctrl(int d22Val);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "dllexp_Pch_D22_D23_Ctrl", ExactSpelling = true)]
		public static extern uint Pch_D22_D23_Ctrl(int d22Val, int d23Val, bool d23AsInp);
	}
}
