namespace ChromaConnector
{
	internal static class Utils
	{
		public static byte GetRed(int colorRef) => (byte)(colorRef & byte.MaxValue);

		public static byte GetGreen(int colorRef) => (byte)(colorRef >> 8 & byte.MaxValue);

		public static byte GetBlue(int colorRef) => (byte)(colorRef >> 16 & byte.MaxValue);
		public static Color GetColor(int colorRef, int brightness) => new(
			(byte)Math.Min(byte.MaxValue, GetRed(colorRef) * brightness / 100f),
			(byte)Math.Min(byte.MaxValue, GetGreen(colorRef) * brightness / 100f),
			(byte)Math.Min(byte.MaxValue, GetBlue(colorRef) * brightness / 100f)
			);
	}
	public sealed record Color(byte R, byte G, byte B);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int ChromaBroadcastEvent(byte type, IntPtr pData);
}

//Records
namespace System.Runtime.CompilerServices
{
	internal static class IsExternalInit { }
}