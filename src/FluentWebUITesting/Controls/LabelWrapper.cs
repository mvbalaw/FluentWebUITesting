using FluentWebUITesting.Accessors;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class LabelWrapper : ControlWrapperBase
	{
		public LabelWrapper(IWebElement label, string howFound, IWebDriver browser)
			: base(label, howFound, browser)
		{
		}

		public string For
		{
			get { return Element.GetAttribute("for"); }
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, Element.Text);
		}
	}
}