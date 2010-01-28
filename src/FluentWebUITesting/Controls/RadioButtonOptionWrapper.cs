using WatiN.Core;

namespace FluentWebUITesting.Controls
{
    public class RadioButtonOptionWrapper : ControlWrapperBase
    {
        private readonly Browser _browser;
        private readonly RadioButton _radioButton;

        public RadioButtonOptionWrapper(RadioButton radioButton, Browser browser, string howFound)
            : base(howFound)
        {
            _radioButton = radioButton;
            _browser = browser;
        }

        protected override Element Element
        {
            get { return _radioButton; }
        }

        public void Select()
        {
            Exists().ShouldBeTrue();
            Enabled().ShouldBeTrue();
            _radioButton.Click();

            _browser.WaitForComplete();
        }
    }
}