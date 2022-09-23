using System;
using System.Runtime.InteropServices;

namespace ChromaConnector;

public static class ChromaBroadcastImpl
{
	private const string assemblyName = "RzChromaBroadcastAPI.dll";

	public static int Initialize(Guid appId, ChromaBroadcastEvent onChromaBroadcastEvent)
	{
		try
		{
			if (Init(appId) != 0)
				return 1627;
			_ = RegisterEventNotification(onChromaBroadcastEvent);
			return 0;
		}
		catch
		{
			return 1;
		}
	}

	public static int UnInitialize()
	{
		try
		{
			_ = UnRegisterEventNotification();
			return UnInit() == 0 ? 0 : 1627;
		}
		catch
		{
			return 1;
		}
	}

	[DllImport(assemblyName, CallingConvention = CallingConvention.Cdecl)]
	private static extern int Init(Guid appInfo);

	[DllImport(assemblyName, CallingConvention = CallingConvention.Cdecl)]
	private static extern int RegisterEventNotification(ChromaBroadcastEvent lpFunc);

	[DllImport(assemblyName, CallingConvention = CallingConvention.Cdecl)]
	private static extern int UnRegisterEventNotification();

	[DllImport(assemblyName, CallingConvention = CallingConvention.Cdecl)]
	private static extern int UnInit();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int ChromaBroadcastEvent(byte type, IntPtr pData);
}
