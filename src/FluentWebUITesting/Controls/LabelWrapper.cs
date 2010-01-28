using FluentWebUITesting.Accessors;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
    public class LabelWrapper : ControlWrapperBase
    {
        private readonly Span _span;

        public LabelWrapper(Span span, string howFound)
            : base(howFound)
        {
            _span = span;
        }

        protected override Element Element
        {
            get { return _span; }
        }

        public ReadOnlyText Text()
        {
            return new ReadOnlyText(HowFound, _span.Text);
        }
    }
}