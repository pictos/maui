using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Maui.Controls.Sample
{
	public partial class Page2 : ContentPage
	{
		public Page2()
		{
			InitializeComponent();
			//ShellToolbarExtensions.SetToolbarBackgroundColor(Shell.Current, Colors.Fuchsia);

			//((IToolbarElement)Shell.Current).Toolbar;
		}

		private void Button_Clicked(object sender, System.EventArgs e)
		{
			ShellToolbarExtensions.SetToolbarBackgroundColor(Shell.Current, Colors.Black);
		}
	}
}