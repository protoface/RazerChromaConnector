// Decompiled with JetBrains decompiler
// Type: ChromaBroadcast.CHROMA_BROADCAST_EFFECT
// Assembly: LIFXChromaConnector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D823469-FC65-451C-969D-3742766F4C80
// Assembly location: C:\Program Files (x86)\Yeelight\LIFXChromaConnector.exe

namespace ChromaBroadcast
{
	public struct CHROMA_BROADCAST_EFFECT
	{
		public int CL1;
		public int CL2;
		public int CL3;
		public int CL4;
		public int CL5;
		public uint Reserved;
	}
	//public enum CHROMA_BROADCAST_STATUS
	//{
	//	LIVE = 1,
	//	NOT_LIVE = 2,
	//}
	public enum CHROMA_BROADCAST_TYPE : byte
	{
		BROADCAST_EFFECT = 1,
		BROADCAST_STATUS = 2,
	}
}
