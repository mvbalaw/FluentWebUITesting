using FluentWebUITesting.Accessors;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class PageWrapper
	{
		private readonly IWebDriver _browser;

		public PageWrapper(IWebDriver browser)
		{
			_browser = browser;
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText("Page", _browser.PageSource);
		}
	}
}