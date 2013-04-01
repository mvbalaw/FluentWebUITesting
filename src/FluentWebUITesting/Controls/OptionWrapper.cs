using FluentWebUITesting.Accessors;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FluentWebUITesting.Controls
{
	public class OptionWrapper : ControlWrapperBase
	{
		private readonly IWebElement _option;
		private readonly IWebElement _parentDropDown;

		public OptionWrapper(IWebElement option, string howFound, IWebElement parentDropDown)
			: base(howFound)
		{
			_option = option;
			_parentDropDown = parentDropDown;
		}

		public override IWebElement Element
		{
			get { return _option; }
		}
		protected override bool ElementExists
		{
			get { return _option != null; }
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
			return new ReadOnlyText("text of " + HowFound, _option.Text);
		}

		public ReadOnlyText Value()
		{
			return new ReadOnlyText("value of " + HowFound, _option.GetAttribute("value"));
		}
	}
}