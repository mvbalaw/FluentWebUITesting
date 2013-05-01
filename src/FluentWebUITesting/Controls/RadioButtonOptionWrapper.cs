using System;

using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class RadioButtonOptionWrapper : ControlWrapperBase
	{
		public RadioButtonOptionWrapper(IWebElement radioButton, string howFound, IWebDriver browser)
			: base(radioButton, howFound, browser)
		{
		}

		public WaitWrapper Select()
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
			return new WaitWrapper();
		}
	}
}