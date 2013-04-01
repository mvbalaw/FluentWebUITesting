using OpenQA.Selenium;

namespace FluentWebUITesting.Accessors
{
	public class EditableText : TextBase
	{
		private readonly IWebElement _textField;

		public EditableText(IWebElement textField, string howFound, string text)
			: base(text, howFound)
		{
			_textField = textField;
		}

		public void SetValueTo(string text)
		{
			_textField.Clear();
			_textField.SendKeys(text);
		}
	}
}