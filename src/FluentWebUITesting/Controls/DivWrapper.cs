using FluentWebUITesting.Accessors;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class DivWrapper : ControlWrapperBase
	{
		public DivWrapper(IWebElement div, string howFound, IWebDriver browser)
			: base(div, howFound, browser)
		{
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, Element.Text);
		}
	}
}