using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public class ButtonWrapper : ControlWrapperBase
	{
		private readonly Button _button;

		public ButtonWrapper(Button button, string howFound)
			: base(howFound)
		{
			_button = button;
		}

		protected override Element Element
		{
			get { return _button; }
		}

		public WaitWrapper Click()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
			_button.Click();
			return new WaitWrapper();
		}

		public void ClickNoWait()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
			_button.ClickNoWait();
		}
	}
}