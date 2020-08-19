using System;
using System.Runtime.InteropServices;
using System.Security;

namespace OpenAmbientLED.External
{
	[Serializable]
	[SuppressUnmanagedCodeSecurity]
	internal class InvkYcc
	{
		[NonSerialized]
		public const string ModuleName = "yccV2.dll";

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void gb_outp(uint Port, byte DataValue);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void gb_outpw(uint Port, ushort DataValue);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void gb_outpd(uint Port, ulong DataValue);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern byte gb_inp(uint Port);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern ushort gb_inpw(uint Port);

		[DllImport(ModuleName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern uint gb_inpd(uint Port);
	}
}
