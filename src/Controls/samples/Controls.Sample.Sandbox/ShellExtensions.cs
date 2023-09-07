using System;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace Maui.Controls.Sample;
public static class ShellToolbarExtensions
{
	public static readonly BindableProperty ToolbarBackgroundColorProperty =
	   BindableProperty.Create("ToolbarBackgroundColor", typeof(Color), typeof(Shell), null, propertyChanged: OnBackgroundColorChanged);

	public static readonly BindableProperty ToolbarFontSizeProperty =
   BindableProperty.Create("ToolbarFontSize", typeof(int), typeof(Shell), null, propertyChanged: ToolbarFontSizeChanged);

	static void ToolbarFontSizeChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var size = GetToolbarFontSize(bindable);
		if (((IToolbarElement)Shell.Current)?.Toolbar is not Toolbar toolbar)
		{
			return;
		}

		var shell = Shell.Current;

		shell.Loaded += Shell_Loaded;


		static void ChangeSize(int value, Toolbar toolbar)
		{
			var currentPage = Shell.Current.CurrentPage;
#if WINDOWS
			var platformView = (MauiToolbar)toolbar.Handler.PlatformView;
			if (value <= 0)
				return;
			platformView.FontSize = value;
#endif
		}
		void Shell_Loaded(object sender, EventArgs e)
		{
			var shell = (Shell)sender;

			ChangeSize(size, toolbar);
			shell.CurrentItem.PropertyChanged += async (s, e) =>
			{
				var currentPage = Shell.Current.CurrentPage;

				// TODO: Fix me
				// When we first navigate to a certain tab it's null
				// so we add the loop here to await until shell fills the
				// CurrentPage property.
				int count = 0;
				while (currentPage is null)
				{
					await Task.Delay(100);
					if (count > 5)
						return;
					count++;
				}

				var x = GetToolbarFontSize(currentPage);
				if (e.PropertyName == Shell.CurrentItemProperty.PropertyName)
					ChangeSize(x, toolbar);
			};

			shell.Loaded -= Shell_Loaded;
		}
	}


	public static int GetToolbarFontSize(BindableObject element)
	{
		return (int)element.GetValue(ToolbarFontSizeProperty);
	}

	public static void SetToolbarFontSize(BindableObject element, int value)
	{
		element.SetValue(ToolbarFontSizeProperty, value);
	}


	static void OnBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var shell = Shell.Current;
		var color = GetToolbarBackgroundColor(bindable);

		if (color is null || ((IToolbarElement)shell).Toolbar is not Toolbar toolbar)
			return;

		shell.Loaded += (s, e) =>
		{
			//toolbar.Handler.UpdateValue("ShellToolbarExtensions.BackgroundColor");
#if WINDOWS
			toolbar.BarBackground = color;
#endif
		};

		shell.CurrentItem.PropertyChanged += async (s, e) =>
		{
			System.Diagnostics.Debug.WriteLine($"The property is: {e.PropertyName}");
			if (e.PropertyName == Shell.CurrentItemProperty.PropertyName)
			{
				var shell = Shell.Current;
				var currentPage = shell.CurrentPage;


				// TODO: Fix me
				// When we first navigate to a certain tab it's null
				// so we add the loop here to await until shell fills the
				// CurrentPage property.
				int count = 0;
				while (currentPage is null)
				{
					await Task.Delay(100);
					if (count > 5)
						return;
					count++;
				}


				var color = GetToolbarBackgroundColor(currentPage);

				if (((IToolbarElement)shell).Toolbar is not Toolbar toolbar)
					return;

				// ShellAppearance run more than once, and override this value so adding a delay to make sure
				// we are safe to perform the change
				await Task.Delay(100);
				toolbar.BarBackground = color;
			}
		};
	}

	public static Color GetToolbarBackgroundColor(BindableObject element)
	{
		return (Color)element.GetValue(ToolbarBackgroundColorProperty);
	}

	public static void SetToolbarBackgroundColor(BindableObject element, Color value)
	{
		element.SetValue(ToolbarBackgroundColorProperty, value);
	}

	// This doesn't work
	internal static void Init()
	{
		ToolbarHandler.Mapper.Add("ShellToolbarExtensions.BackgroundColor", OnValueChanged);
		// (h, v) =>
		//{
		//#if WINDOWS
		//			((Toolbar)v).BarBackground = Colors.Fuchsia;
		//#endif
		//#if ANDROID
		//			var materialToolbar = h.PlatformView;
		//			materialToolbar.SetBackgroundColor(Colors.Fuchsia.ToPlatform());
		//			materialToolbar.TextAlignment = Android.Views.TextAlignment.TextStart;
		//			var p = materialToolbar.Title;
		//			var z = materialToolbar.TitleFormatted;
		//#endif

		//});
	}

	private static void OnValueChanged(IToolbarHandler handler, IToolbar toolbar)
	{
		var color = GetToolbarBackgroundColor(Shell.Current);
		var tb = (Toolbar)toolbar;
		if (color is null)
		{
			return;
		}

		tb.BarBackground = color;
	}
}