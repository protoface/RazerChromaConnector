// Decompiled with JetBrains decompiler
// Type: ChromaBroadcast.Utils
// Assembly: LIFXChromaConnector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D823469-FC65-451C-969D-3742766F4C80
// Assembly location: C:\Program Files (x86)\Yeelight\LIFXChromaConnector.exe

using System;
using System.Windows.Media;

namespace ChromaBroadcast
{
	internal static class Utils
	{
		public static byte GetRed(int colorRef) => (byte)(colorRef & (int)byte.MaxValue);

		public static byte GetGreen(int colorRef) => (byte)(colorRef >> 8 & (int)byte.MaxValue);

		public static byte GetBlue(int colorRef) => (byte)(colorRef >> 16 & (int)byte.MaxValue);

		public static int GetRGB(byte red, byte green, byte blue) => (int)red & (int)byte.MaxValue | ((int)green & (int)byte.MaxValue) << 8 | ((int)blue & (int)byte.MaxValue) << 16;

		public static Color GetColor(int colorRef)
		{
			int red = (int)Utils.GetRed(colorRef);
			byte green = Utils.GetGreen(colorRef);
			byte blue = Utils.GetBlue(colorRef);
			int g = (int)green;
			int b = (int)blue;
			return Color.FromRgb((byte)red, (byte)g, (byte)b);
		}

		public static Color UpdateBrightness(int brightness, Color color)
		{
			color.R = (byte)Math.Min((float)byte.MaxValue, (float)((int)color.R * brightness) / 100f);
			color.G = (byte)Math.Min((float)byte.MaxValue, (float)((int)color.G * brightness) / 100f);
			color.B = (byte)Math.Min((float)byte.MaxValue, (float)((int)color.B * brightness) / 100f);
			return color;
		}
	}
}
