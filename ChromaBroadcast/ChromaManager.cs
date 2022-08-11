// Decompiled with JetBrains decompiler
// Type: ChromaBroadcast.ChromaManager
// Assembly: LIFXChromaConnector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D823469-FC65-451C-969D-3742766F4C80
// Assembly location: C:\Program Files (x86)\Yeelight\LIFXChromaConnector.exe

using ChromaBroadcastConfigurator;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace ChromaBroadcast
{
	internal static class ChromaManager
	{
		private static readonly Guid CHROMA_BROADCAST_LIFX_APP = Guid.Parse("c009f863-3664-4df9-9da7-f4aad53bf7b7");
#pragma warning disable S1450 // Private fields only used as local variables in methods should become local variables
		private static ChromaBroadcastImpl.ChromaBroadcastEvent RefChromaBroadcastEvent;
#pragma warning restore S1450 // Private fields only used as local variables in methods should become local variables
		public static int Brightness { get; set; } = 100;
		public static Color Color { get; set; } = Color.FromRgb(0, 0, 0);
		public static Action<Color> ColorChanged { get; set; }

		public static void Start()
		{
			RefChromaBroadcastEvent = new ChromaBroadcastImpl.ChromaBroadcastEvent(OnChromaBroadcastEvent);
			ChromaBroadcastImpl.Initialize(CHROMA_BROADCAST_LIFX_APP, RefChromaBroadcastEvent);
		}

		readonly static UdpClient udpClient = new();

		private static int OnChromaBroadcastEvent(CHROMA_BROADCAST_TYPE type, IntPtr pData)
		{
			if (pData == IntPtr.Zero)
				return 0;
			if (type == CHROMA_BROADCAST_TYPE.BROADCAST_EFFECT)
			{
				CHROMA_BROADCAST_EFFECT structure = Marshal.PtrToStructure<CHROMA_BROADCAST_EFFECT>(pData);
				System.Windows.Media.Color color = Utils.UpdateBrightness(Brightness, Utils.GetColor(structure.CL1));
				if (!udpClient.Client.Connected)
					udpClient.Connect("192.168.0.109", 2100);
				udpClient.Send(new byte[] { 0, color.R, color.G, color.B, 1, color.R, color.G, color.B, 2, color.R, color.G, color.B, 3, color.R, color.G, color.B, 4, color.R, color.G, color.B }, 4 * 5);
				if (Color != color)
				{
					ColorChanged?.Invoke(color);
					Color = color;
				}
			}
			return 0;
		}
	}
}
