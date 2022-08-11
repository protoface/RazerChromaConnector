// Decompiled with JetBrains decompiler
// Type: ChromaBroadcastConfigurator.App
// Assembly: LIFXChromaConnector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D823469-FC65-451C-969D-3742766F4C80
// Assembly location: C:\Program Files (x86)\Yeelight\LIFXChromaConnector.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace ChromaBroadcastConfigurator
{
	public class App : Application
	{
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

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent() => this.StartupUri = new Uri("Source/UI/MainWindow.xaml", UriKind.Relative);

		[STAThread]
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public static void Main()
		{
			App app = new App();
			app.InitializeComponent();
			app.Run();
		}
	}
}
