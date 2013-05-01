using System;

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
			ClickNoWait();
			return new WaitWrapper();
		}

		public void ClickNoWait()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
			Browser.Focus(Element);
			try
			{
				Element.Click();
			}
			catch (InvalidOperationException invalidOperationException)
			{
				if (invalidOperationException.Message.Contains("Other element would receive the click"))
				{
					Browser.ScrollElementIntoView(Element);
					Browser.Focus(Element);
					Element.Click();
				}
			}
		}
	}
}