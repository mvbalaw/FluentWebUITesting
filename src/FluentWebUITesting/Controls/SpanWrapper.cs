using FluentWebUITesting.Accessors;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class SpanWrapper : ControlWrapperBase
	{
		public SpanWrapper(IWebElement span, string howFound, IWebDriver browser)
			: base(span, howFound, browser)
		{
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, Element.Text);
		}
	}
}