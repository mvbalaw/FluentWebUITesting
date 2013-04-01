using FluentWebUITesting.Accessors;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class LabelWrapper : ControlWrapperBase
	{
		private readonly IWebElement _label;

		public LabelWrapper(IWebElement label, string howFound)
			: base(howFound)
		{
			_label = label;
		}

		public override IWebElement Element
		{
			get { return _label; }
		}
		protected override bool ElementExists
		{
			get { return _label != null; }
		}

		public string For
		{
			get { return _label.GetAttribute("for"); }
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, _label.Text);
		}
	}
}