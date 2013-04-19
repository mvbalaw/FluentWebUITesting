using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Accessors
{
	public class EditableText : TextBase
	{
		private readonly IWebElement _textField;
		private readonly IWebDriver _browser;

		public EditableText(IWebElement textField, string howFound, string text, IWebDriver browser)
			: base(text, howFound)
		{
			_textField = textField;
			_browser = browser;
		}

		public void SetValueTo(string text)
		{
			_browser.ScrollElementIntoView(_textField);
			_textField.Clear();
			_textField.SendKeys(text);
		}
	}
}