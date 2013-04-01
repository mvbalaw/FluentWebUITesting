using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class RadioButtonOptionWrapper : ControlWrapperBase
	{
		private readonly IWebDriver _browser;
		private readonly IWebElement _radioButton;

		public RadioButtonOptionWrapper(IWebElement radioButton, string howFound, IWebDriver browser)
			: base(howFound)
		{
			_radioButton = radioButton;
			_browser = browser;
		}

		public override IWebElement Element
		{
			get { return _radioButton; }
		}
		protected override bool ElementExists
		{
			get { return _radioButton != null; }
		}

		public WaitWrapper Select()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
			_browser.Focus(_radioButton);
			_radioButton.Click();
			return new WaitWrapper();
		}
	}
}