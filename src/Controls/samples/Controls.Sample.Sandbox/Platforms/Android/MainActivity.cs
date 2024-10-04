using System.Diagnostics;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Maui;
using Debug = System.Diagnostics.Debug;

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
			base.OnCreate(savedInstanceState);
			Microsoft.Maui.ApplicationModel.Platform.Init(this, savedInstanceState);
			Window?.AddFlags(WindowManagerFlags.Fullscreen);

			var activity = this;
			if (activity == null)
				return;

			Debug.Assert(activity.Window is not null);
			if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
			{
#pragma warning disable CA1416 // Supported on: 'android' 30.0 and later
#pragma warning disable CA1422 // Validate platform compatibility
				activity.Window.SetDecorFitsSystemWindows(false);
#pragma warning restore CA1422 // Validate platform compatibility
				activity.Window.InsetsController?.Hide(WindowInsets.Type.SystemBars());
				if (activity.Window.InsetsController != null)
					activity.Window.InsetsController.SystemBarsBehavior = (int)WindowInsetsControllerBehavior.ShowTransientBarsBySwipe;
#pragma warning restore CA1416 // Supported on: 'android' 30.0 and later
			}
			else
			{
#pragma warning disable CS0618 // Type or member is obsolete
				SystemUiFlags systemUiVisibility = (SystemUiFlags)activity.Window.DecorView.SystemUiVisibility;
				systemUiVisibility |= SystemUiFlags.HideNavigation;
				systemUiVisibility |= SystemUiFlags.Immersive;
				activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)systemUiVisibility;
#pragma warning restore CS0618 // Type or member is obsolete
			}
		}


		//		protected override void OnCreate(Bundle? savedInstanceState)
		//		{
		//			System.Diagnostics.Debug.Assert(Window is not null);
		//			base.OnCreate(savedInstanceState);
		//			//HideSystemControls();
		//			MauiEdgeToEdge.EnableEdgeToEdge(this);

		//#pragma warning disable CA1416 // Validate platform compatibility
		//			var controller = Window?.InsetsController;
		//			controller?.Hide(WindowInsets.Type.SystemBars());
		//#pragma warning restore CA1416 // Validate platform compatibility
		//		}

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
