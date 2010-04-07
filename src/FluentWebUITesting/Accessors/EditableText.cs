using WatiN.Core;

namespace FluentWebUITesting.Accessors
{
	public class EditableText : TextBase
	{
		private readonly TextField _textField;

		public EditableText(TextField textField, string howFound, string text)
			: base(text, howFound)
		{
			_textField = textField;
		}

		public void SetValueTo(string text)
		{
			_textField.Value = text;
		}
	}
}