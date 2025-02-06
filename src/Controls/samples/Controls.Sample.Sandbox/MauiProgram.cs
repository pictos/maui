using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;

namespace Maui.Controls.Sample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		return MauiApp
			.CreateBuilder()
#if __ANDROID__ || __IOS__
			.UseMauiMaps()
#endif
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Dokdo-Regular.ttf", "Dokdo");
				fonts.AddFont("LobsterTwo-Regular.ttf", "Lobster Two");
				fonts.AddFont("LobsterTwo-Bold.ttf", "Lobster Two Bold");
				fonts.AddFont("LobsterTwo-Italic.ttf", "Lobster Two Italic");
				fonts.AddFont("LobsterTwo-BoldItalic.ttf", "Lobster Two BoldItalic");
				fonts.AddFont("ionicons.ttf", "Ionicons");
				fonts.AddFont("SegoeUI.ttf", "Segoe UI");
				fonts.AddFont("SegoeUI-Bold.ttf", "Segoe UI Bold");
				fonts.AddFont("SegoeUI-Italic.ttf", "Segoe UI Italic");
				fonts.AddFont("SegoeUI-Bold-Italic.ttf", "Segoe UI Bold Italic");
			})
#if ANDROID
			.ConfigureLifecycleEvents(static lifecycleBuilder =>
			{
				lifecycleBuilder.AddAndroid(static androidBuilder =>
				{
					androidBuilder.OnCreate(static (activity, _) =>
					{
						if (activity is not AndroidX.AppCompat.App.AppCompatActivity componentActivity)
						{
							System.Diagnostics.Trace.WriteLine($"Unable to Modify Android StatusBar On ModalPage: Activity {activity.LocalClassName} must be an {nameof(AndroidX.AppCompat.App.AppCompatActivity)}");
							return;
						}

						if (componentActivity.GetFragmentManager() is not AndroidX.Fragment.App.FragmentManager fragmentManager)
						{
							System.Diagnostics.Trace.WriteLine($"Unable to Modify Android StatusBar On ModalPage: Unable to retrieve fragment manager from {nameof(AndroidX.AppCompat.App.AppCompatActivity)}");
							return;
						}

						fragmentManager.RegisterFragmentLifecycleCallbacks(new FragmentLifecycleManager(), false);
					});
				});
			})
#endif
			.Build();
	}

}
