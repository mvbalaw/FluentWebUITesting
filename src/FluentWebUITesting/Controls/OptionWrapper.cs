using FluentWebUITesting.Accessors;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
    public class OptionWrapper : ControlWrapperBase
    {
        private readonly Option _option;

        public OptionWrapper(Option option, string howFound)
            : base(howFound)
        {
            _option = option;
        }

        protected override Element Element
        {
            get { return _option; }
        }

		public ReadOnlyText Text()
		{
			return new ReadOnlyText("text of "+HowFound, _option.Text);	
		}

		public ReadOnlyText Value()
		{
			return new ReadOnlyText("value of "+ HowFound, _option.Value);	
		}
    }
}