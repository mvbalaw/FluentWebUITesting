using System;
using FluentWebUITesting.Accessors;
using WatiN.Core;

namespace FluentWebUITesting.Controls
{
    public abstract class ControlWrapperBase
    {
        protected ControlWrapperBase(string howFound)
        {
            HowFound = howFound;
        }

        protected abstract Element Element { get; }
        protected string HowFound { get; private set; }

        public IReadOnlyBooleanState Enabled()
        {
            const string unexpectedlyFalse = "{0} is not enabled but should be.";
            const string unexpectedlyTrue = "{0} is enabled but should not be.";
            string unexpectedlyTrueMessage = String.Format(unexpectedlyTrue, HowFound);
            string unexpectedlyFalseMessage = String.Format(unexpectedlyFalse, HowFound);
            var result = new BooleanState(unexpectedlyFalseMessage,
                                          unexpectedlyTrueMessage,
                                          () => Element.Enabled);
            return result;
        }

        public IReadOnlyBooleanState Exists()
        {
            const string unexpectedlyFalse = "{0} does not exist but should.";
            const string unexpectedlyTrue = "{0} exists but should not.";
            string unexpectedlyTrueMessage = String.Format(unexpectedlyTrue, HowFound);
            string unexpectedlyFalseMessage = String.Format(unexpectedlyFalse, HowFound);
            var result = new BooleanState(unexpectedlyFalseMessage,
                                          unexpectedlyTrueMessage,
                                          () => Element.Exists);
            return result;
        }

        public IReadOnlyBooleanState Visible()
        {
            const string unexpectedlyFalse = "{0} is not visible but should be.";
            const string unexpectedlyTrue = "{0} is visible but should not be.";
            string unexpectedlyTrueMessage = String.Format(unexpectedlyTrue, HowFound);
            string unexpectedlyFalseMessage = String.Format(unexpectedlyFalse, HowFound);
            var result = new BooleanState(unexpectedlyFalseMessage,
                                          unexpectedlyTrueMessage,
                                          () => IsDisplayed(Element));
            return result;
        }

        // source http://blog.coditate.com/2009/07/determining-html-element-visibility.html
        private static bool IsDisplayed(Element element)
        {
            if (string.Equals(element.Style.Display, "none"))
            {
                return false;
            }
            if (string.Equals(element.TagName,"form",StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            if (element.Parent != null)
            {
                return IsDisplayed(element.Parent);
            }
            return true;
        }
    }
}