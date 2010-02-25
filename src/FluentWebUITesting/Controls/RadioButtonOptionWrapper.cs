using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public class RadioButtonOptionWrapper : ControlWrapperBase
	{
		private readonly RadioButton _radioButton;

		public RadioButtonOptionWrapper(RadioButton radioButton, string howFound)
			: base(howFound)
		{
			_radioButton = radioButton;
		}

		protected override Element Element
		{
			get { return _radioButton; }
		}

		public WaitWrapper Select()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
			_radioButton.Click();
			return new WaitWrapper();
		}
	}
}