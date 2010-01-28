using FluentWebUITesting.Accessors;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
    public class TextBoxWrapper : ControlWrapperBase
    {
        private readonly TextField _textField;

        public TextBoxWrapper(TextField textField, string howFound)
            : base(howFound)
        {
            _textField = textField;
        }

        protected override Element Element
        {
            get { return _textField; }
        }

        public EditableText Text()
        {
            return new EditableText(_textField, HowFound, _textField.Text);
        }
    }
}