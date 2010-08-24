using System;
using System.Collections.Generic;
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
            const string unexpectedlyFalse = "{0} should be visible but is not because {1} is marked display: none .";
            const string unexpectedlyTrue = "{0} should not be visible but is.";
            var visibility = IsDisplayed(Element);
            string parent;
            if (visibility.Value != null)
            {
                parent = "its parent tag " + visibility.Value;
            }
            else
            {
                parent = "it";
            }
            string unexpectedlyTrueMessage = String.Format(unexpectedlyTrue, HowFound);
            string unexpectedlyFalseMessage = String.Format(unexpectedlyFalse, HowFound, parent);

            var result = new BooleanState(unexpectedlyFalseMessage,
                                          unexpectedlyTrueMessage,
                                          () => visibility.Key);
            return result;
        }

        // adapted from http://blog.coditate.com/2009/07/determining-html-element-visibility.html
        private static KeyValuePair<bool, string> IsDisplayed(Element element)
        {
            if (string.Equals(element.Style.Display, "none"))
            {
                return new KeyValuePair<bool, string>(false, element.TagName);
            }
            if (string.Equals(element.TagName, "form", StringComparison.InvariantCultureIgnoreCase))
            {
                return new KeyValuePair<bool, string>(true, null);
            }
            if (element.Parent != null)
            {
                var result = IsDisplayed(element.Parent);
                if (result.Key)
                {
                    return result;
                }
                return new KeyValuePair<bool, string>(false, element.TagName + "." + result.Value);
            }
            return new KeyValuePair<bool, string>(true, null);
        }
    }
}