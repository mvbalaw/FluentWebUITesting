using FluentWebUITesting.Accessors;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public class PageWrapper
	{
		private readonly Browser _browser;

		public PageWrapper(Browser browser)
		{
			_browser = browser;
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText("Page", _browser.Text);
		}
	}
}