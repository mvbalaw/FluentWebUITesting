using FluentWebUITesting.Accessors;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class DivWrapper : ControlWrapperBase
	{
		private readonly IWebElement _div;

		public DivWrapper(IWebElement div, string howFound)
			: base(howFound)
		{
			_div = div;
		}

		public override IWebElement Element
		{
			get { return _div; }
		}
		protected override bool ElementExists
		{
			get { return _div != null; }
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, _div.Text);
		}
	}
}