using System;

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