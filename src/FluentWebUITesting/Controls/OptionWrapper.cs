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
    }
}