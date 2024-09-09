using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Maui.Controls.Sample.Issues;

// Using the PR number as issueNumber argument, because there's a collision with the issue number 6994 from XF issue
[Issue(IssueTracker.Github, 24662, "Reusing the same page for formsheet Modal causes measuring issues ", PlatformAffected.iOS)]
public class Issue6994Maui : TestShell
{
	public Issue6994Maui()
	{
		Routing.RegisterRoute(nameof(TestModal), new TestModalFactory());
		var shellSection = new ShellSection
		{
			Title = "Home",
			Items =
			{
				new ShellContent
				{
					Content = new MyPage()
				}
			}
		};
		
		Items.Add(shellSection);
	}
	
	
	protected override void Init()
	{
	}
}

file class MyPage : ContentPage
{
	public MyPage()
	{
		var btn = new Button() { Text = "Open Modal", AutomationId = "OpenModal" };

		btn.Clicked += async (_, __) =>
		{
			await Shell.Current.GoToAsync(nameof(TestModal));
		};
	}

}

file class TestModal : ContentPage
{
	Label _label;
	public TestModal()
	{
		Shell.SetPresentationMode(this, PresentationMode.Modal);
		On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.FormSheet);

		_label = new()
		{
			AutomationId = "SizeLabel",
			Text = "0x0"
		};

		var btn = new Button
		{
			Text = "Close Modal",
			AutomationId = "CloseModal",
		};
		
		Content = new VerticalStackLayout
		{
			Children = 
			{
				_label,
				btn
			}
		};
	}

	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);
		_label.Text = $"{width} x {height}";
	}
}

file class TestModalFactory : RouteFactory
{
	static TestModal _modal = new();
	public override Element GetOrCreate()
	{
		return _modal;
	}

	public override Element GetOrCreate(IServiceProvider services)
	{
		return _modal;
	}
}

