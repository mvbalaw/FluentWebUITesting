using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class ButtonWrapper : ControlWrapperBase
	{
		private readonly IWebDriver _browser;
		private readonly IWebElement _button;

		public ButtonWrapper(IWebElement button, string howFound, IWebDriver browser)
			: base(howFound)
		{
			_button = button;
			_browser = browser;
		}

		public override IWebElement Element
		{
			get { return _button; }
		}
		protected override bool ElementExists
		{
			get { return _button != null; }
		}

		public WaitWrapper Click()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
			_browser.Focus(_button);
			_button.Click();
			return new WaitWrapper();
		}

		public void ClickNoWait()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
			_browser.Focus(_button);
			_button.Click();
		}
	}
}