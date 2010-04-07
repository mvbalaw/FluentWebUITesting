using FluentWebUITesting.Accessors;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public class DivWrapper : ControlWrapperBase
	{
		private readonly Div _div;

		public DivWrapper(Div div, string howFound)
			: base(howFound)
		{
			_div = div;
		}

		protected override Element Element
		{
			get { return _div; }
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, _div.Text);
		}
	}
}