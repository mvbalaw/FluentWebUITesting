using FluentWebUITesting.Accessors;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public class CheckBoxWrapper : ControlWrapperBase
	{
		private readonly CheckBox _checkBox;

		public CheckBoxWrapper(CheckBox checkBox, string howFound)
			: base(howFound)
		{
			_checkBox = checkBox;
		}

		protected override Element Element
		{
			get { return _checkBox; }
		}

		public BooleanState CheckedState()
		{
			Exists().ShouldBeTrue();
			return new BooleanState(HowFound+" should have been checked but was not",
			                        HowFound+" should not have been checked but was",
			                        ()=>_checkBox.Checked,
			                        value=>_checkBox.Checked = value);
		}
	}
}