using System;
using System.Diagnostics;
using System.Windows;

namespace ChromaBroadcastConfigurator
{
	public class App : Application
	{
		internal static Guid AppId = Guid.Parse("c009f863-3664-4df9-9da7-f4aad53bf7b7");

		public App(string[] args)
		{
			foreach (string arg in args)
			{
				_ = Guid.TryParse(arg, out AppId);
			}
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			Process currentProcess = Process.GetCurrentProcess();
			if (Process.GetProcessesByName(currentProcess.ProcessName).Length > 1)
			{
				MessageBox.Show(currentProcess.ProcessName + " application is already running!");
				Current.Shutdown();
			}
			else
				base.OnStartup(e);
		}

		public void InitializeComponent() => StartupUri = new Uri("ChromaBroadcastConfigurator/MainWindow.xaml", UriKind.Relative);

		[STAThread]
		public static void Main(string[] args)
		{
			App app = new(args);
			app.InitializeComponent();
			app.Run();
		}
	}
}
