using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Uno.Foundation;
using Uno.UI.Runtime.WebAssembly;


namespace UnoVideoWasm;

[HtmlElement("video")]
public class MediaPlayerElement : Border
{
	public Uri Source
	{
		get => (Uri)GetValue(SourceProperty);
		set => SetValue(SourceProperty, value);
	}

	public static DependencyProperty SourceProperty { get; } =
		DependencyProperty.Register(
			nameof(Source),
			typeof(Uri),
			typeof(MediaPlayerElement),
			new PropertyMetadata(null, OnSourceChanged));

	private static void OnSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
	{
		((MediaPlayerElement)dependencyObject).SetSource((Uri)args.NewValue);
	}


	public static DependencyProperty CurrentTimeProperty { get; } =
	   DependencyProperty.Register(
nameof(CurrentTime),
typeof(int),
typeof(MediaPlayerElement),
new PropertyMetadata(0, (s, e) =>
{
	((MediaPlayerElement)s).SetTime((int)e.NewValue);

}));

	public int CurrentTime
	{
		get => (int)GetValue(CurrentTimeProperty);
		set => SetValue(CurrentTimeProperty, value);
	}

	public MediaPlayerElement()
	{
		Background = new SolidColorBrush(Colors.Transparent);
		this.SetCssClass("video-js");

		//TODO: Find some solution to not set the height here
		// If it's left 0, the video won't be visible as this height will be set on the element style and the class will be ignored.
		Height = 1000;
		SetStretch(Stretch.Uniform);
	}

	public void SetSource(Uri source)
	{
		var encodedSource = WebAssemblyRuntime.EscapeJs(source.ToString());

		var js = $"umpSetSource(element, '{encodedSource}', 'video/mp4');";
		this.ExecuteJavascript(js);
		SetControls(true);
	}

	public void SetControls(bool enabled)
	{
		var js = $"element.controls = {enabled.ToString().ToLower()};";
		this.ExecuteJavascript(js);
	}

	internal void SetStretch(Stretch stretch)
	{
		//TODO: set the correct class and unset the old one.
		this.SetCssClass("ump-strech-uniform");
	}

	public void SetTime(int time)
	{
		var js = $"umpSetVideoTime(element,{time});";
		this.ExecuteJavascript(js);
	}
}
