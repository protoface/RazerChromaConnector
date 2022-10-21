namespace ChromaConnector;

public static class ChromaManager
{
	public static void Connect(Guid appId, Action<Color[]> BroadcastEvent)
	{
		if (BroadcastEvent is null)
			throw new ArgumentNullException(nameof(BroadcastEvent));
		OnBroadcastEvent = BroadcastEvent;
		if (Environment.Is64BitProcess)
			ChromaBroadcastImpl64.Initialize(appId, BroadcastHandler);
		else
			ChromaBroadcastImpl.Initialize(appId, BroadcastHandler);
	}

	private static event Action<Color[]>? OnBroadcastEvent;
	private static readonly ChromaBroadcastEvent BroadcastHandler = OnChromaBroadcastEvent;
	public static int Brightness { get; set; } = 100;

	private static int OnChromaBroadcastEvent(byte type, IntPtr pData)
	{
		if (pData != IntPtr.Zero && type == 1)
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

	public static void Unitialize()
	{
		if (Environment.Is64BitProcess)
			ChromaBroadcastImpl64.UnInitialize();
		else
			ChromaBroadcastImpl.UnInitialize();
	}
}
