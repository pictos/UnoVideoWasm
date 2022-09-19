using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnoVideoWasm;
//<local:MediaPlayerElement AreTransportControlsEnabled="True" Source="https://sec.ch9.ms/ch9/5d93/a1eab4bf-3288-4faf-81c4-294402a85d93/XamarinShow_mid.mp4" />
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainPage : Page
{


	MediaPlayerElement media;

	public MainPage()
	{
		this.InitializeComponent();

		media = new MediaPlayerElement
		{
			AreDimensionsConstrained = true,
			Source = new Uri("https://sec.ch9.ms/ch9/5d93/a1eab4bf-3288-4faf-81c4-294402a85d93/XamarinShow_mid.mp4")
		};



		var button = new Button
		{
			Content = "Set time"
		};

		button.Click += (_, __) => media.CurrentTime = 200;

		this.grid.Children.Add(button);

		grid.Children.Add(media);

	}


	protected override async void OnNavigatedFrom(NavigationEventArgs e)
	{
		base.OnNavigatedFrom(e);

		await Task.Delay(5_000);
		media.CurrentTime = 120;
	}
}
