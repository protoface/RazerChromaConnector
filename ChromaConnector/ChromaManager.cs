using System;
using System.Runtime.InteropServices;

namespace ChromaConnector;

public sealed class ChromaManager
{
	public static ChromaManager CreateNew(Guid appId, EventHandler<Color[]> _broadcastEvent)
	{
		return new(appId, _broadcastEvent);
	}

	internal ChromaManager(Guid appId, EventHandler<Color[]> _broadcastEvent)
	{
		BroadcastEvent = OnChromaBroadcastEvent;
		OnBroadcastEvent += _broadcastEvent;
		ChromaBroadcastImpl.Initialize(appId, BroadcastEvent);
	}

	public event EventHandler<Color[]> OnBroadcastEvent;
	private readonly ChromaBroadcastImpl.ChromaBroadcastEvent BroadcastEvent;
	public int Brightness { get; set; } = 100;

	private int OnChromaBroadcastEvent(byte type, IntPtr pData)
	{
		if (pData != IntPtr.Zero && type == 1)
		{
			ChromaBroadcastEffect structure = Marshal.PtrToStructure<ChromaBroadcastEffect>(pData!);
			OnBroadcastEvent?.Invoke(this, new[] { Utils.GetColor(structure.CL1, Brightness), Utils.GetColor(structure.CL2, Brightness), Utils.GetColor(structure.CL3, Brightness), Utils.GetColor(structure.CL4, Brightness), Utils.GetColor(structure.CL5, Brightness) });
		}

		return 0;
	}

	public void Unitialize()
	{
		ChromaBroadcastImpl.UnInitialize();
	}
}
