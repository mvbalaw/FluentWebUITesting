using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class LinkWrapper : ControlWrapperBase
	{
		private readonly IWebDriver _browser;
		private readonly IWebElement _link;

		public LinkWrapper(IWebElement link, string howFound, IWebDriver browser)
			: base(howFound)
		{
			_link = link;
			_browser = browser;
		}

		public override IWebElement Element
		{
			get { return _link; }
		}
		protected override bool ElementExists
		{
			get { return _link != null; }
		}

		public WaitWrapper Click()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
			_browser.Focus(_link);
			_link.Click();
			return new WaitWrapper();
		}
	}
}