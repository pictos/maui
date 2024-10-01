using System.Diagnostics;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AndroidX.Activity;
using Maui.Controls.Sample.Sandbox.Platforms.Android;
using Microsoft.Maui;

namespace Maui.Controls.Sample.Platform
{
	[Activity(
		Theme = "@style/Maui.SplashTheme",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode)]
	[IntentFilter(
		new[] { Microsoft.Maui.ApplicationModel.Platform.Intent.ActionAppAction },
		Categories = new[] { Android.Content.Intent.CategoryDefault })]
	public class MainActivity : MauiAppCompatActivity
	{
		protected override void OnCreate(Bundle? savedInstanceState)
		{
			System.Diagnostics.Debug.Assert(Window is not null);
			base.OnCreate(savedInstanceState);
			//HideSystemControls();
			MauiEdgeToEdge.EnableEdgeToEdge(this);

#pragma warning disable CA1416 // Validate platform compatibility
			var controller = Window?.InsetsController;
			controller?.Hide(WindowInsets.Type.SystemBars());
#pragma warning restore CA1416 // Validate platform compatibility
		}

		//		void HideSystemControls()
		//		{
		//			Window?.AddFlags(WindowManagerFlags.Fullscreen | WindowManagerFlags.LayoutInScreen);
		//			Window?.ClearFlags(WindowManagerFlags.ForceNotFullscreen);

		//#pragma warning disable CA1416 // Validate platform compatibility
		//			var controller = Window?.InsetsController;
		//			controller?.Hide(WindowInsets.Type.SystemBars());
		//#pragma warning restore CA1416 // Validate platform compatibility
		//		}
	}
}
