namespace ChromaConnector
{
	internal static class Utils
	{
		public static byte GetRed(int colorRef) => (byte)(colorRef & byte.MaxValue);

		public static byte GetGreen(int colorRef) => (byte)(colorRef >> 8 & byte.MaxValue);

		public static byte GetBlue(int colorRef) => (byte)(colorRef >> 16 & byte.MaxValue);
		public static Color GetColor(int colorRef, float bri) => new(
				(byte)(GetRed(colorRef) * bri),
				(byte)(GetGreen(colorRef) * bri),
				(byte)(GetBlue(colorRef) * bri)
				);
	}

}