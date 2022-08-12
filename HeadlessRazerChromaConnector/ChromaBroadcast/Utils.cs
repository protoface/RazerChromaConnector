// Decompiled with JetBrains decompiler
// Type: ChromaBroadcast.Utils
// Assembly: LIFXChromaConnector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D823469-FC65-451C-969D-3742766F4C80
// Assembly location: C:\Program Files (x86)\Yeelight\LIFXChromaConnector.exe

using System;

namespace ChromaBroadcast
{
	internal static class Utils
	{
		public static byte GetRed(int colorRef) => (byte)(colorRef & (int)byte.MaxValue);

		public static byte GetGreen(int colorRef) => (byte)(colorRef >> 8 & (int)byte.MaxValue);

		public static byte GetBlue(int colorRef) => (byte)(colorRef >> 16 & (int)byte.MaxValue);

		//public static int GetRGB(byte red, byte green, byte blue) => (int)red & (int)byte.MaxValue | ((int)green & (int)byte.MaxValue) << 8 | ((int)blue & (int)byte.MaxValue) << 16;

		public static Color GetColor(int colorRef)
		{
			byte red = GetRed(colorRef);
			byte green = GetGreen(colorRef);
			byte blue = GetBlue(colorRef);
			return new Color(red, green, blue);
		}

		public static Color UpdateBrightness(int brightness, Color color)
		{
			color.R = (byte)Math.Min(byte.MaxValue, color.R * brightness / 100f);
			color.G = (byte)Math.Min(byte.MaxValue, color.G * brightness / 100f);
			color.B = (byte)Math.Min(byte.MaxValue, color.B * brightness / 100f);
			return color;
		}
	}

	class Color
	{
		public byte R;
		public byte G;
		public byte B;

		public Color(byte r, byte g, byte b)
		{
			R = r;
			G = g;
			B = b;
		}
	}
}
