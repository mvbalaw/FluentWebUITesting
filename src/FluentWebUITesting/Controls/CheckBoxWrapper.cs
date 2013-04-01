using FluentWebUITesting.Accessors;
using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class CheckBoxWrapper : ControlWrapperBase
	{
		private readonly IWebDriver _browser;
		private readonly IWebElement _checkBox;

		public CheckBoxWrapper(IWebElement checkBox, string howFound, IWebDriver browser)
			: base(howFound)
		{
			_checkBox = checkBox;
			_browser = browser;
		}

		internal CheckBoxWrapper(CheckBoxWrapper checkBox, string howFound)
			: base(howFound)
		{
			_checkBox = checkBox == null ? null : checkBox.Element;
		}

		public override IWebElement Element
		{
			get { return _checkBox; }
		}
		protected override bool ElementExists
		{
			get { return _checkBox != null; }
		}

		public BooleanState CheckedState()
		{
			Exists().ShouldBeTrue();
			return new BooleanState(HowFound + " should have been checked but was not",
			                        HowFound + " should not have been checked but was",
			                        () => _checkBox.Selected,
			                        value =>
				                        {
					                        _browser.Focus(_checkBox);
					                        _checkBox.Click();
				                        });
		}
	}
}