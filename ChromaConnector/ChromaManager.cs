using System;
using System.Runtime.InteropServices;

namespace ChromaConnector;

public static class ChromaManager
{
	public static void Connect(Guid appId, EventHandler<Color[]> _broadcastEvent)
	{
		BroadcastEvent = OnChromaBroadcastEvent;
		OnBroadcastEvent += _broadcastEvent;
		if (Environment.Is64BitProcess)
			ChromaBroadcastImpl64.Initialize(appId, BroadcastEvent);
		else
			ChromaBroadcastImpl.Initialize(appId, BroadcastEvent);
	}

	public static event EventHandler<Color[]>? OnBroadcastEvent;
	private static ChromaBroadcastEvent? BroadcastEvent;
	public static int Brightness { get; set; } = 100;

	private static int OnChromaBroadcastEvent(byte type, IntPtr pData)
	{
		if (pData != IntPtr.Zero && type == 1)
		{
			ChromaBroadcastEffect structure = Marshal.PtrToStructure<ChromaBroadcastEffect>(pData!);
			OnBroadcastEvent?.Invoke(null, new[] { Utils.GetColor(structure.CL1, Brightness), Utils.GetColor(structure.CL2, Brightness), Utils.GetColor(structure.CL3, Brightness), Utils.GetColor(structure.CL4, Brightness), Utils.GetColor(structure.CL5, Brightness) });
		}

		return 0;
	}

	public static void Unitialize()
	{
		if (Environment.Is64BitProcess)
			ChromaBroadcastImpl64.UnInitialize();
		else
			ChromaBroadcastImpl.UnInitialize();
	}
}
