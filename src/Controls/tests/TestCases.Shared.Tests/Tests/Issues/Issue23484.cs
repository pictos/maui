#if !WINDOWS // https://github.com/dotnet/maui/issues/24661
using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues
{
    public class Issue23484 : _IssuesUITest
	{
		public Issue23484(TestDevice device) : base(device)
		{
		}

		public override string Issue => "TabbedPage Clear and Adding existing page does not display across the entire screen";

		[Test]
		[Category(UITestCategories.Layout)]
		public void ReusingNavigationPageDoesntBreakLayout()
		{
			var originalSize = App.WaitForElement("SizeLabel").GetText();
			for (int i = 0; i < 3; i++)
			{
				App.Tap("RecreateButton");
				var recreatedSize = App.WaitForElement("SizeLabel").GetText();
				Assert.That(originalSize, Is.EqualTo(recreatedSize));
			}
		}
	}
    
    
	public class Issue6994Maui : _IssuesUITest
	{
		public Issue6994Maui(TestDevice device) : base(device)
		{
		}
	
		public override string Issue => "TabbedPage Clear and Adding existing page does not display across the entire screen";

		[Test]
		[Category(UITestCategories.Layout)]
		public void ReusingPageOnModalDoesNotBreakLayout()
		{
			App.Tap("OpenModal");
			App.WaitForElement("SizeLabel");
			var originalSize = App.WaitForElement("SizeLabel").GetText();
			
			for (int i = 0; i < 3; i++)
			{
				App.Tap("CloseModal");
				App.Tap("OpenModal");
				App.WaitForElement("SizeLabel");
				var recreatedSize = App.WaitForElement("SizeLabel").GetText();
				Assert.That(originalSize, Is.EqualTo(recreatedSize));
			}
		}
	}
}
#endif
