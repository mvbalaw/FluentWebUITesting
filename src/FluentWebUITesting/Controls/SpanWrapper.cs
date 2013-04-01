using FluentWebUITesting.Accessors;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class SpanWrapper : ControlWrapperBase
	{
		private readonly IWebElement _span;

		public SpanWrapper(IWebElement span, string howFound)
			: base(howFound)
		{
			_span = span;
		}

		public override IWebElement Element
		{
			get { return _span; }
		}
		protected override bool ElementExists
		{
			get { return _span != null; }
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, _span.Text);
		}
	}
}