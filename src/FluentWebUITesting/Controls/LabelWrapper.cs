using System;

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
			string text = null;
			try
			{
				text = Element.Text;
			}
			catch (Exception exception)
			{
				throw new Exception("Unable to get label text for " + HowFound+" - this happens when the page content changes dynamically.", exception);
			}
			return new ReadOnlyText(HowFound, text);
		}
	}
}