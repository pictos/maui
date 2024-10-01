﻿using Android.App;
using Android.Content.Res;
using global::Android.Graphics;
using Android.Hardware.Camera2;
using Android.OS;
using Android.Views;
using AndroidX.Annotations;
using AndroidX.Core.View;
//using CommunityToolkit.Maui.Core;
using System;

namespace Maui.Controls.Sample.Sandbox.Platforms.Android;
#nullable disable
public static class MauiEdgeToEdge
{
	static global::Android.Graphics.Color DefaultLightScrim = global::Android.Graphics.Color.Argb(0xe6, 0xFF, 0xFF, 0xFF);
	static global::Android.Graphics.Color DefaultDarkScrim = global::Android.Graphics.Color.Argb(0x80, 0x1b, 0x1b, 0x1b);


	public static void EnableEdgeToEdge(this MauiAppCompatActivity activity)
		=> activity.EnableEdgeToEdge(
			SystemBarStyle.Auto(global::Android.Graphics.Color.Transparent, global::Android.Graphics.Color.Transparent),
			SystemBarStyle.Auto(DefaultLightScrim, DefaultDarkScrim));


	public static void EnableEdgeToEdge(this MauiAppCompatActivity activity, SystemBarStyle statusBarStyle, SystemBarStyle navigationBarStyle)
	{
		var view = activity.Window.DecorView;
		var statusBarIsDark = statusBarStyle.DetectDarkMode(activity.Resources);
		var navigationBarIsDark = navigationBarStyle.DetectDarkMode(activity.Resources);
		
		IEdgeToEdge impl;

		if (OperatingSystem.IsAndroidVersionAtLeast(30))
			impl = new EdgeToEdge30();
		else if (OperatingSystem.IsAndroidVersionAtLeast(29))
			impl = new EdgeToEdgeApi29();
		else if (OperatingSystem.IsAndroidVersionAtLeast(28))
			impl = new EdgeToEdgeApi28();
		else if (OperatingSystem.IsAndroidVersionAtLeast(26))
			impl = new EdgeToEdgeApi26();
		else if (OperatingSystem.IsAndroidVersionAtLeast(23))
			impl = new EdgeToEdgeApi23();
		else if (OperatingSystem.IsAndroidVersionAtLeast(21))
			impl = new EdgeToEdgeApi21();
		else
			impl = new EdgeToEdgeBase();

		impl.Setup(statusBarStyle, navigationBarStyle, activity.Window, view, statusBarIsDark, navigationBarIsDark);

		impl.AdjustLayoutInDisplayCutoutMode(activity.Window);
	}
}


public class SystemBarStyle
{
	public SystemBarStyle(global::Android.Graphics.Color lightScrim, global::Android.Graphics.Color darkScrim, UiMode nightMode, Func<Resources, bool> detectDarkMode)
	{
		LightScrim = lightScrim;
		DarkScrim = darkScrim;
		NightMode = nightMode;
		DetectDarkMode = detectDarkMode;
	}

	public static SystemBarStyle Auto(global::Android.Graphics.Color lightScrim, global::Android.Graphics.Color darkScrim)
		=> new SystemBarStyle(lightScrim, darkScrim, UiMode.NightUndefined, res =>
			(res.Configuration.UiMode & UiMode.NightMask) == UiMode.NightYes);

	public static SystemBarStyle Dark(global::Android.Graphics.Color scrim)
		=> new SystemBarStyle(scrim, scrim, UiMode.NightYes, res => true);

	public static SystemBarStyle Light(global::Android.Graphics.Color scrim, global::Android.Graphics.Color darkScrim)
		=> new SystemBarStyle(scrim, darkScrim, UiMode.NightNo, res => false);

	public global::Android.Graphics.Color LightScrim { get; private set; }
	public global::Android.Graphics.Color DarkScrim { get; private set; }

	public UiMode NightMode { get; private set; }

	public Func<Resources, bool> DetectDarkMode { get; private set; }

	public global::Android.Graphics.Color GetScrim(bool isDark)
	{
		return isDark ? DarkScrim : LightScrim;
	}

	public global::Android.Graphics.Color GetScrimWithEnforcedContrast(bool isDark)
	{
		if (NightMode == UiMode.NightUndefined)
			return global::Android.Graphics.Color.Transparent;

		if (isDark)
			return DarkScrim;

		return LightScrim;
	}
}

public interface IEdgeToEdge
{
	void Setup(
		SystemBarStyle statusBarStyle,
		SystemBarStyle navigationBarStyle,
		global::Android.Views.Window window,
		global::Android.Views.View view,
		bool statusBarIsDark,
		bool navigationBarIsDark)
	{
		// No edge to edge, pre SDK 21
	}

	void AdjustLayoutInDisplayCutoutMode(global::Android.Views.Window window)
	{
		// No display cutout before SDK 28
	}
}

public class EdgeToEdgeBase : IEdgeToEdge
{
	public virtual void Setup(
		SystemBarStyle statusBarStyle,
		SystemBarStyle navigationBarStyle,
		global::Android.Views.Window window,
		global::Android.Views.View view,
		bool statusBarIsDark,
		bool navigationBarIsDark)
	{
		// No edge to edge, pre SDK 21
	}

	public virtual void AdjustLayoutInDisplayCutoutMode(global::Android.Views.Window window)
	{
		// No display cutout before SDK 28
	}
}

public class EdgeToEdgeApi21 : EdgeToEdgeBase
{
	public override void Setup(
		SystemBarStyle statusBarStyle,
		SystemBarStyle navigationBarStyle,
		global::Android.Views.Window window,
		global::Android.Views.View view,
		bool statusBarIsDark,
		bool navigationBarIsDark)
	{
		WindowCompat.SetDecorFitsSystemWindows(window, false);
		window.AddFlags(WindowManagerFlags.TranslucentStatus);
		window.AddFlags(WindowManagerFlags.TranslucentNavigation);
	}
}

public class EdgeToEdgeApi23 : EdgeToEdgeBase
{
	public override void Setup(
		SystemBarStyle statusBarStyle,
		SystemBarStyle navigationBarStyle,
		global::Android.Views.Window window,
		global::Android.Views.View view,
		bool statusBarIsDark,
		bool navigationBarIsDark)
	{
		WindowCompat.SetDecorFitsSystemWindows(window, false);
		window.SetStatusBarColor(statusBarStyle.GetScrim(statusBarIsDark));
		window.SetNavigationBarColor(navigationBarStyle.DarkScrim);
		new WindowInsetsControllerCompat(window, view).AppearanceLightStatusBars = !statusBarIsDark;
	}
}

public class EdgeToEdgeApi26 : EdgeToEdgeBase
{
	public override void Setup(
		SystemBarStyle statusBarStyle,
		SystemBarStyle navigationBarStyle,
		global::Android.Views.Window window,
		global::Android.Views.View view,
		bool statusBarIsDark,
		bool navigationBarIsDark)
	{
		WindowCompat.SetDecorFitsSystemWindows(window, false);
		window.SetStatusBarColor(statusBarStyle.GetScrim(statusBarIsDark));
		window.SetNavigationBarColor(navigationBarStyle.GetScrim(navigationBarIsDark));
		var c = new WindowInsetsControllerCompat(window, view);
		c.AppearanceLightStatusBars = !statusBarIsDark;
		c.AppearanceLightNavigationBars = !navigationBarIsDark;
	}
}

public class EdgeToEdgeApi28 : EdgeToEdgeApi26
{
	public override void AdjustLayoutInDisplayCutoutMode(global::Android.Views.Window window)
	{
		window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
	}
}

public class EdgeToEdgeApi29 : EdgeToEdgeApi28
{
	public override void Setup(
		SystemBarStyle statusBarStyle,
		SystemBarStyle navigationBarStyle,
		global::Android.Views.Window window,
		global::Android.Views.View view,
		bool statusBarIsDark,
		bool navigationBarIsDark)
	{
		WindowCompat.SetDecorFitsSystemWindows(window, false);
		window.SetStatusBarColor(statusBarStyle.GetScrimWithEnforcedContrast(navigationBarIsDark));
		window.SetNavigationBarColor(navigationBarStyle.GetScrimWithEnforcedContrast(navigationBarIsDark));
		window.StatusBarContrastEnforced = false;
		window.NavigationBarContrastEnforced = navigationBarStyle.NightMode == UiMode.NightUndefined;

		var c = new WindowInsetsControllerCompat(window, view);
		c.AppearanceLightStatusBars = !statusBarIsDark;
		c.AppearanceLightNavigationBars = !navigationBarIsDark;
	}
}

public class EdgeToEdge30 : EdgeToEdgeApi29
{
	public override void AdjustLayoutInDisplayCutoutMode(global::Android.Views.Window window)
	{
		window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.Always;
	}
}

