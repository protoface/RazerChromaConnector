using System.Collections.Generic;
using System.Threading;

namespace ChromaConnector;

public static class ChromaManager
{
	public static void Connect(Guid appId, Action<Color[]>? BroadcastEvent = null)
	{
		if (BroadcastEvent != null)
			OnBroadcastEvent = BroadcastEvent;
		if (Environment.Is64BitProcess)
			ChromaBroadcastImpl64.Initialize(appId, BroadcastHandler);
		else
			ChromaBroadcastImpl.Initialize(appId, BroadcastHandler);
	}

	private static event Action<Color[]>? OnBroadcastEvent;
	private static readonly ChromaBroadcastEvent BroadcastHandler = OnChromaBroadcastEvent;
	private static float brightness = 1;

	public static float Brightness { get => brightness; set => brightness = Math.Clamp(value, 0, 1); }
	private static int OnChromaBroadcastEvent(byte type, IntPtr pData)
	{
		if (pData != IntPtr.Zero && type == (byte)CHROMA_BROADCAST_TYPE.BROADCAST_EFFECT)
		{
			ChromaBroadcastEffect structure = Marshal.PtrToStructure<ChromaBroadcastEffect>(pData!);
			OnBroadcastEvent?.Invoke(new[] {
				Utils.GetColor(structure.CL1, Brightness),
				Utils.GetColor(structure.CL2, Brightness),
				Utils.GetColor(structure.CL3, Brightness),
				Utils.GetColor(structure.CL4, Brightness),
				Utils.GetColor(structure.CL5, Brightness)
			});
		}

		return 0;
	}

	public static IEnumerable<Color[]> WaitForColors(CancellationToken token)
	{
		SemaphoreSlim semaphore = new(1);
		Color[]? colors = null;
		OnBroadcastEvent += e => { colors = e; semaphore.Release(); };
		while (!token.IsCancellationRequested)
		{
			semaphore.Wait(token);
			if (colors != null)
				yield return colors;
		}
		yield break;
	}

	public static void Unitialize()
	{
		if (Environment.Is64BitProcess)
			ChromaBroadcastImpl64.UnInitialize();
		else
			ChromaBroadcastImpl.UnInitialize();
	}
}
