using FluentWebUITesting.Accessors;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public class OptionWrapper : ControlWrapperBase
	{
		private readonly Option _option;
		private readonly SelectList _parentDropDown;

		public OptionWrapper(Option option, string howFound, SelectList parentDropDown)
			: base(howFound)
		{
			_option = option;
			_parentDropDown = parentDropDown;
		}

		protected override Element Element
		{
			get { return _option; }
		}

		public WaitWrapper Select()
		{
			Exists().ShouldBeTrue();
			_parentDropDown.Select(Text().GetValue());
			return new WaitWrapper();
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText("text of " + HowFound, _option.Text);
		}

		public ReadOnlyText Value()
		{
			return new ReadOnlyText("value of " + HowFound, _option.Value);
		}
	}
}