using System.Diagnostics;
using ChromaBroadcast;

namespace HeadlessRazerChromaConnector
{
	static class Program
	{
		private static async Task Main()
		{
			Process currentProcess = Process.GetCurrentProcess();
			if (Process.GetProcessesByName(currentProcess.ProcessName).Length > 1)
			{
				Console.Error.WriteLine("Application is already running!");
				return;
			}

			TaskCompletionSource tcs = new();
			bool exiting = false;

			ChromaManager.Start();

			Console.CancelKeyPress += (_, e) =>
			{
				e.Cancel = true;
				ChromaBroadcastImpl.UnInitialize();
				tcs.SetResult();
				exiting = true;
			};
			AppDomain.CurrentDomain.ProcessExit += (_, _) =>
			{
				if (!exiting)
				{
					ChromaBroadcastImpl.UnInitialize();
					tcs.SetResult();
				}
			};

			await tcs.Task;
		}
	}
}