using FluentWebUITesting.Accessors;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FluentWebUITesting.Controls
{
	public class OptionWrapper : ControlWrapperBase
	{
		private readonly IWebElement _parentDropDown;

		public OptionWrapper(IWebElement option, string howFound, IWebElement parentDropDown, IWebDriver browser)
			: base(option, howFound, browser)
		{
			_parentDropDown = parentDropDown;
		}

		public WaitWrapper Select()
		{
			Exists().ShouldBeTrue();
			var select = new SelectElement(_parentDropDown);
			select.SelectByText(Text().GetValue());
			return new WaitWrapper();
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText("text of " + HowFound, Element.Text);
		}

		public ReadOnlyText Value()
		{
			return new ReadOnlyText("value of " + HowFound, Element.GetAttribute("value"));
		}
	}
}