
namespace Maui.Controls.Sample;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		var btn = new Button()
		{
			Text = "Modal",
			VerticalOptions = LayoutOptions.Center
		};

		btn.Clicked += (_, __) => Navigation.PushModalAsync(new TestModal());

		Content = btn;
	}
}


class TestModal : ContentPage
{
	public TestModal()
	{
		var lbl = new Label
		{
			Text = "This the Modal",
			VerticalTextAlignment = TextAlignment.Center,
		};
		Content = lbl;
	}
}
