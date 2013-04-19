using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class ButtonWrapper : ControlWrapperBase, INavigationControl
	{
		public ButtonWrapper(IWebElement button, string howFound, IWebDriver browser)
			: base(button, howFound, browser)
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