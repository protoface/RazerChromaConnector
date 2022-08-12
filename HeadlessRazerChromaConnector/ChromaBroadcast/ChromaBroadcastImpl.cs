// Decompiled with JetBrains decompiler
// Type: ChromaBroadcast.ChromaBroadcastImpl
// Assembly: LIFXChromaConnector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D823469-FC65-451C-969D-3742766F4C80
// Assembly location: C:\Program Files (x86)\Yeelight\LIFXChromaConnector.exe

using System;
using System.Runtime.InteropServices;

namespace ChromaBroadcast
{
	public static class ChromaBroadcastImpl
	{
		private const string assemblyName = "RzChromaBroadcastAPI64.dll";

		public static int Initialize(Guid appId, ChromaBroadcastEvent onChromaBroadcastEvent)
		{
			try
			{
				if (Init(appId) != 0)
					return 1627;
				RegisterEventNotification(onChromaBroadcastEvent);
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
				UnRegisterEventNotification();
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
		public delegate int ChromaBroadcastEvent(CHROMA_BROADCAST_TYPE type, IntPtr pData);
	}
}
