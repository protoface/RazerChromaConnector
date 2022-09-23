namespace ChromaConnector;

public struct ChromaBroadcastEffect
{
	public int CL1 { get; set; }
	public int CL2 { get; set; }
	public int CL3 { get; set; }
	public int CL4 { get; set; }
	public int CL5 { get; set; }
	public uint Reserved { get; set; }
}
public enum CHROMA_BROADCAST_TYPE : byte
{
	BROADCAST_EFFECT = 1,
	BROADCAST_STATUS = 2,
}
