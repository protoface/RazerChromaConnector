// Decompiled with JetBrains decompiler
// Type: ChromaBroadcast.ChromaBroadcastImpl
// Assembly: LIFXChromaConnector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D823469-FC65-451C-969D-3742766F4C80
// Assembly location: C:\Program Files (x86)\Yeelight\LIFXChromaConnector.exe

using System;
using System.Runtime.InteropServices;

namespace ChromaBroadcast
{
  internal class ChromaBroadcastImpl
  {
    private const string DLL_NAME = "RzChromaBroadcastAPI.dll";

    public static int Initialize(
      Guid appId,
      ChromaBroadcastImpl.ChromaBroadcastEvent onChromaBroadcastEvent)
    {
      try
      {
        if (ChromaBroadcastImpl.Init(appId) != 0)
          return 1627;
        ChromaBroadcastImpl.RegisterEventNotification(onChromaBroadcastEvent);
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
        ChromaBroadcastImpl.UnRegisterEventNotification();
        return ChromaBroadcastImpl.UnInit() == 0 ? 0 : 1627;
      }
      catch
      {
        return 1;
      }
    }

    [DllImport("RzChromaBroadcastAPI.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int Init(Guid appInfo);

    [DllImport("RzChromaBroadcastAPI.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int RegisterEventNotification(
      ChromaBroadcastImpl.ChromaBroadcastEvent lpFunc);

    [DllImport("RzChromaBroadcastAPI.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int UnRegisterEventNotification();

    [DllImport("RzChromaBroadcastAPI.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int UnInit();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int ChromaBroadcastEvent(CHROMA_BROADCAST_TYPE type, IntPtr pData);
  }
}
