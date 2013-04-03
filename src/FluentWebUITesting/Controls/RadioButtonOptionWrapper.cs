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
			Element.Click();
			return new WaitWrapper();
		}
	}
}