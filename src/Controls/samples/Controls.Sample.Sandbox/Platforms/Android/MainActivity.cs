using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Microsoft.Maui;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;

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
	}
}


public sealed class FragmentLifecycleManager : FragmentManager.FragmentLifecycleCallbacks
{

	public override void OnFragmentAttached(FragmentManager fm, AndroidX.Fragment.App.Fragment f, Context context)
	{
		base.OnFragmentAttached(fm, f, context);
	}

	public override void OnFragmentCreated(FragmentManager fm, AndroidX.Fragment.App.Fragment f, Bundle? savedInstanceState)
	{
		base.OnFragmentCreated(fm, f, savedInstanceState);
	}

	public override void OnFragmentDestroyed(FragmentManager fm, AndroidX.Fragment.App.Fragment f)
	{
		base.OnFragmentDestroyed(fm, f);
	}

	public override void OnFragmentDetached(FragmentManager fm, AndroidX.Fragment.App.Fragment f)
	{
		base.OnFragmentDetached(fm, f);
	}

	public override void OnFragmentPaused(FragmentManager fm, AndroidX.Fragment.App.Fragment f)
	{
		base.OnFragmentPaused(fm, f);
	}

	public override void OnFragmentPreAttached(FragmentManager fm, AndroidX.Fragment.App.Fragment f, Context context)
	{
		base.OnFragmentPreAttached(fm, f, context);
	}

	public override void OnFragmentPreCreated(FragmentManager fm, AndroidX.Fragment.App.Fragment f, Bundle? savedInstanceState)
	{
		base.OnFragmentPreCreated(fm, f, savedInstanceState);
	}

	public override void OnFragmentResumed(FragmentManager fm, AndroidX.Fragment.App.Fragment f)
	{
		base.OnFragmentResumed(fm, f);
	}

	public override void OnFragmentSaveInstanceState(FragmentManager fm, AndroidX.Fragment.App.Fragment f, Bundle outState)
	{
		base.OnFragmentSaveInstanceState(fm, f, outState);
	}

	public override void OnFragmentStarted(FragmentManager fm, AndroidX.Fragment.App.Fragment f)
	{
		base.OnFragmentStarted(fm, f);
	}

	public override void OnFragmentStopped(FragmentManager fm, AndroidX.Fragment.App.Fragment f)
	{
		base.OnFragmentStopped(fm, f);
	}

	public override void OnFragmentViewCreated(FragmentManager fm, AndroidX.Fragment.App.Fragment f, Android.Views.View v, Bundle? savedInstanceState)
	{
		base.OnFragmentViewCreated(fm, f, v, savedInstanceState);
	}

	public override void OnFragmentViewDestroyed(FragmentManager fm, AndroidX.Fragment.App.Fragment f)
	{
		base.OnFragmentViewDestroyed(fm, f);
	}
}