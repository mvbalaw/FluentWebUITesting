using WatiN.Core;

namespace FluentWebUITesting.Controls
{
    public class LinkWrapper : ControlWrapperBase
    {
        private readonly Browser _browser;
        private readonly Link _link;

        public LinkWrapper(Link link, Browser browser, string howFound)
            : base(howFound)
        {
            _link = link;
            _browser = browser;
        }

        protected override Element Element
        {
            get { return _link; }
        }

        public void Click()
        {
            Exists().ShouldBeTrue();
            Enabled().ShouldBeTrue();
            _link.Click();

            _browser.WaitForComplete();
        }
    }
}