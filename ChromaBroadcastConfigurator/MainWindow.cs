﻿// Decompiled with JetBrains decompiler
// Type: ChromaBroadcastConfigurator.MainWindow
// Assembly: LIFXChromaConnector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D823469-FC65-451C-969D-3742766F4C80
// Assembly location: C:\Program Files (x86)\Yeelight\LIFXChromaConnector.exe

using ChromaBroadcast;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;

namespace ChromaBroadcastConfigurator
{
	public partial class MainWindow : Window, IComponentConnector
	{
		Color Color;
		public MainWindow()
		{
			this.InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ChromaManager.ColorChanged = (color) => this.Dispatcher.Invoke(() => { Background = new SolidColorBrush(color); colorText.Text = string.Format("R: {0} G: {1} B: {2}", color.R, color.G, color.B); });
			ChromaManager.Start();
			this._mSliderBrightness.Value = 100;

		}

		private void Window_Closed(object sender, EventArgs e)
		{
			ChromaBroadcastImpl.UnInitialize();
		}

		private void SliderBrightness_ValueChanged(
		  object sender,
		  RoutedPropertyChangedEventArgs<double> e)
		{
			ChromaManager.Brightness = (int)(sender as Slider).Value;
			this.UpdateBrightness();
		}

		private void SliderBrightness_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			ChromaManager.Brightness = (int)(sender as Slider).Value;
			this.UpdateBrightness();
		}

		private void UpdateBrightness()
		{
			if (this._mTextBrightness == null)
				return;
			this._mTextBrightness.Text = ChromaManager.Brightness.ToString() + "%";
		}
	}
}
