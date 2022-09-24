using ChromaConnector;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace ChromaBroadcastConfigurator
{
	public partial class MainWindow : Window, IComponentConnector
	{
		public MainWindow() => InitializeComponent();

		bool showPreview;

		private void Window_Loaded(object sender, RoutedEventArgs e) => ChromaManager.Connect(App.AppId, Connector_OnBroadcastEvent);

		private void Connector_OnBroadcastEvent(object sender, ChromaConnector.Color[] e)
		{
			Dispatcher.Invoke(() =>
			{
				if (showPreview) Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(e[0].R, e[0].G, e[0].B));
				_mTextColor.Text = $"R: {e[0].R} G: {e[0].G} B: {e[0].B}";
			});
		}

		private void PreviewEnabled_Checked(object sender, RoutedEventArgs e) => showPreview = true;
		private void PreviewEnabled_Unchecked(object sender, RoutedEventArgs e)
		{
			showPreview = false;
			Background = new SolidColorBrush(Colors.Black);
		}

		private void Window_Closed(object sender, EventArgs e) => ChromaManager.Unitialize();

		private void SliderBrightness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			ChromaManager.Brightness = (int)(sender as Slider).Value;
		}

	}
}
