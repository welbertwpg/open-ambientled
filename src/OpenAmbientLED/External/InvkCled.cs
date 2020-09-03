using System;
using System.Runtime.InteropServices;
using System.Security;

namespace OpenAmbientLED.External
{
    [Serializable]
	[SuppressUnmanagedCodeSecurity]
	public class InvkCled
	{
		[NonSerialized]
		public const string ModuleName = "cled.dll";

		public const uint SD_ERROR_INVALID_OPERATION = 4317u;

		public const uint SD_ERROR_SUCCESS = 0u;

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLed_Init", ExactSpelling = true)]
		public static extern uint piCLed_Init();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetPcHealthCode", ExactSpelling = true)]
		public static extern ushort piGetPcHealthCode();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ChkEzSetupSupport", ExactSpelling = true)]
		public static extern uint piChkEzSetupSupport();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetAzaliaLedVal", ExactSpelling = true)]
		public static extern int piGetAzaliaLedVal();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetBiosOnOff", ExactSpelling = true)]
		public static extern int piGetBiosOnOff();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetRearPanelVal", ExactSpelling = true)]
		public static extern int piGetRearPanelVal();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetThreeColorVal", ExactSpelling = true)]
		public static extern int piGetThreeColorVal();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetTrueColorVal", ExactSpelling = true)]
		public static extern uint piGetTrueColorVal();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetTrueColorVal_1", ExactSpelling = true)]
		public static extern uint piGetTrueColorVal_1();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetAzaliaLedMode", ExactSpelling = true)]
		public static extern uint piSetAzaliaLedMode(int iVal);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetBiosOnOff", ExactSpelling = true)]
		public static extern uint piSetBiosOnOff(int iVal);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetRearPanelLedMode", ExactSpelling = true)]
		public static extern uint piSetRearPanelLedMode(int iVal);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetThreeColorLedMode", ExactSpelling = true)]
		public static extern uint piSetThreeColorLedMode(int iVal);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetTrueColorLedMode", ExactSpelling = true)]
		public static extern uint piSetTrueColorLedMode(uint iVal);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetTrueColorLedMode_1", ExactSpelling = true)]
		public static extern uint piSetTrueColorLedMode_1(uint iVal);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetFunction", ExactSpelling = true)]
		public static extern uint piSetFunction();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetAzaliaLedSetupData", ExactSpelling = true)]
		public static extern uint piGetAzaliaLedSetupData(byte[] ary);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetRearPanelLedSetupData", ExactSpelling = true)]
		public static extern uint piGetRearPanelLedSetupData(byte[] ary);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetThreeColorLedSetupData", ExactSpelling = true)]
		public static extern uint piGetThreeColorLedSetupData(byte[] ary);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "InitPchLedCtrl", ExactSpelling = true)]
		public static extern uint piInitPchLedCtrl();

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetLedColor", ExactSpelling = true)]
		public static extern uint piSetLedColor(uint setting_value);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "IsImplementAutoOption", ExactSpelling = true)]
		public static extern uint piIsImplementAutoOption();
	}
}
