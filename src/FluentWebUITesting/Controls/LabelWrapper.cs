using FluentWebUITesting.Accessors;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public class LabelWrapper : ControlWrapperBase
	{
		private readonly Label _label;

		public LabelWrapper(Label label, string howFound)
			: base(howFound)
		{
			_label = label;
		}

		protected override Element Element
		{
			get { return _label; }
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, _label.Text);
		}
	}
}