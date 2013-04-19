using FluentWebUITesting.Accessors;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class TextBoxWrapper : ControlWrapperBase
	{
		public TextBoxWrapper(IWebElement textField, string howFound, IWebDriver browser)
			: base(textField, howFound, browser)
		{
		}

		public EditableText Text()
		{
			return new EditableText(Element, HowFound, Element.GetAttribute("value"), Browser);
		}
	}
}