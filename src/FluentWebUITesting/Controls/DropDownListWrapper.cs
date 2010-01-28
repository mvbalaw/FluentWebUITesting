using FluentWebUITesting.Extensions;

using JetBrains.Annotations;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
    public class DropDownListWrapper : ControlWrapperBase
    {
        private readonly Browser _browser;
        private readonly SelectList _dropDownList;

        public DropDownListWrapper(SelectList dropDownList, Browser browser, string howFound)
            : base(howFound)
        {
            _dropDownList = dropDownList;
            _browser = browser;
        }

        protected override Element Element
        {
            get { return _dropDownList; }
        }

        public void Select([NotNull] string text)
        {
            Exists().ShouldBeTrue();
            Enabled().ShouldBeTrue();
            _dropDownList.OptionWithText(text).Exists().ShouldBeTrue();
            _dropDownList.Select(text);

            _browser.WaitForComplete();
        }
    }
}