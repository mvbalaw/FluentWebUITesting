using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class LinkWrapper : ControlWrapperBase, INavigationControl
	{
		public LinkWrapper(IWebElement link, string howFound, IWebDriver browser)
			: base(link, howFound, browser)
		{
		}

		public WaitWrapper Click()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
			Browser.ScrollElementIntoView(Element);
			Browser.Focus(Element);
			Element.Click();
			return new WaitWrapper();
		}

		public void ClickNoWait()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
			Browser.ScrollElementIntoView(Element);
			Browser.Focus(Element);
			Element.Click();
		}
	}
}