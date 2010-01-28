using WatiN.Core;

namespace FluentWebUITesting.Controls
{
    public class ButtonWrapper : ControlWrapperBase
    {
        private readonly Browser _browser;
        private readonly Button _button;

        public ButtonWrapper(Button button, Browser browser, string howFound)
            : base(howFound)
        {
            _button = button;
            _browser = browser;
        }

        protected override Element Element
        {
            get { return _button; }
        }

        public void Click()
        {
            Exists().ShouldBeTrue();
            Enabled().ShouldBeTrue();
            _button.Click();

            _browser.WaitForComplete();
        }
    }
}