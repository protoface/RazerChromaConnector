using ChromaConnector;
using System.Diagnostics;


Process currentProcess = Process.GetCurrentProcess();
if (Process.GetProcessesByName(currentProcess.ProcessName).Length > 1)
{
	Console.Error.WriteLine("Application is already running!");
	return;
}

Guid AppId = Guid.Parse("c009f863-3664-4df9-9da7-f4aad53bf7b7");

foreach (string arg in args)
{
	Guid.TryParse(arg, out AppId);
}

TaskCompletionSource tcs = new();
bool exiting = false;

ChromaManager.Connect(AppId, e =>
{
	Console.Out.WriteLine(e[0].ToString());
});

Console.CancelKeyPress += (_, e) =>
{
	e.Cancel = true;
	tcs.SetResult();
	exiting = true;
};
AppDomain.CurrentDomain.ProcessExit += (_, _) =>
{
	if (!exiting)
	{
		tcs.SetResult();
		exiting = true;
	}
};
await tcs.Task;

ChromaManager.Unitialize();