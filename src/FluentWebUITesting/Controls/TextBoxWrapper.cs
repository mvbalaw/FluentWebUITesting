using FluentWebUITesting.Accessors;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class TextBoxWrapper : ControlWrapperBase
	{
		private readonly IWebElement _textField;

		public TextBoxWrapper(IWebElement textField, string howFound)
			: base(howFound)
		{
			_textField = textField;
		}

		public override IWebElement Element
		{
			get { return _textField; }
		}
		protected override bool ElementExists
		{
			get { return _textField != null; }
		}

		public EditableText Text()
		{
			return new EditableText(_textField, HowFound, _textField.GetAttribute("value"));
		}
	}
}