using System;
using System.Collections.Generic;

using FluentWebUITesting.Accessors;
using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class ControlWrapperBase : IFieldControl
	{
//// ReSharper disable MemberCanBeProtected.Global
		public ControlWrapperBase(IWebElement element, string howFound, IWebDriver browser)
//// ReSharper restore MemberCanBeProtected.Global
		{
			Element = element;
			HowFound = howFound;
			Browser = browser;
		}

		public IWebDriver Browser { get; private set; }

		public IWebElement Element { get; private set; }
		private bool ElementExists
		{
			get { return Element != null; }
		}
		public string HowFound { get; private set; }
		public string Id
		{
			get { return Element.GetAttribute("id"); }
		}

		public void AppendHowFound(string str)
		{
			if (!HowFound.EndsWith(str))
			{
				HowFound += str;
			}
		}

//// ReSharper disable MemberCanBeProtected.Global
		public IReadOnlyBooleanState Enabled()
//// ReSharper restore MemberCanBeProtected.Global
		{
			const string unexpectedlyFalse = "{0} is not enabled but should be.";
			const string unexpectedlyTrue = "{0} is enabled but should not be.";
			var unexpectedlyTrueMessage = String.Format(unexpectedlyTrue, HowFound);
			var unexpectedlyFalseMessage = String.Format(unexpectedlyFalse, HowFound);
			var result = new BooleanState(unexpectedlyFalseMessage,
			                              unexpectedlyTrueMessage,
			                              () => Element.Enabled);
			return result;
		}

		public IReadOnlyBooleanState Exists()
		{
			const string unexpectedlyFalse = "{0} does not exist but should.";
			const string unexpectedlyTrue = "{0} exists but should not.";
			var unexpectedlyTrueMessage = String.Format(unexpectedlyTrue, HowFound);
			var unexpectedlyFalseMessage = String.Format(unexpectedlyFalse, HowFound);
			var result = new BooleanState(unexpectedlyFalseMessage,
			                              unexpectedlyTrueMessage,
			                              () => ElementExists);
			return result;
		}

		private static KeyValuePair<bool, string> IsDisplayed(IWebElement element)
		{
			// adapted from http://blog.coditate.com/2009/07/determining-html-element-visibility.html
			if (!element.Displayed)
			{
				return new KeyValuePair<bool, string>(false, element.TagName);
			}
			if (string.Equals(element.TagName, "form", StringComparison.InvariantCultureIgnoreCase))
			{
				return new KeyValuePair<bool, string>(true, null);
			}
			if (element.GetParent() != null)
			{
				var result = IsDisplayed(element.GetParent());
				if (result.Key)
				{
					return result;
				}
				return new KeyValuePair<bool, string>(false, element.TagName + "." + result.Value);
			}
			return new KeyValuePair<bool, string>(true, null);
		}

		public CheckBoxWrapper ToCheckBoxWrapper()
		{
			var elem = Element;
			if (elem != null)
			{
				if (String.Compare(elem.TagName, "input", true) != 0 ||
				    String.Compare(elem.GetAttribute("type"), "checkbox", true) != 0)
				{
					elem = null;
				}
			}

			return new CheckBoxWrapper(elem, HowFound, Browser);
		}

		public DropDownListWrapper ToDropDownListWrapper()
		{
			var elem = Element;
			if (elem != null)
			{
				if (String.Compare(elem.TagName, "select", true) != 0)
				{
					elem = null;
				}
			}
			return new DropDownListWrapper(elem, HowFound, Browser);
		}

		public RadioButtonOptionWrapper ToRadioButtonOptionWrapper()
		{
			var elem = Element;
			if (elem != null)
			{
				if (String.Compare(elem.TagName, "input", true) != 0 ||
				    String.Compare(elem.GetAttribute("type"), "radio", true) != 0)
				{
					elem = null;
				}
			}

			return new RadioButtonOptionWrapper(elem, HowFound, Browser);
		}

		public TextBoxWrapper ToTextBoxWrapper()
		{
			var elem = Element;
			if (elem != null)
			{
				if ((String.Compare(elem.TagName, "input", true) != 0 ||
				     String.Compare(elem.GetAttribute("type"), "text", true) != 0) &&
				    String.Compare(elem.TagName, "textarea", true) != 0)
				{
					elem = null;
				}
			}

			return new TextBoxWrapper(elem, HowFound, Browser);
		}

		public IReadOnlyBooleanState Visible()
		{
			Exists().ShouldBeTrue();
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
			var unexpectedlyTrueMessage = String.Format(unexpectedlyTrue, HowFound);
			var unexpectedlyFalseMessage = String.Format(unexpectedlyFalse, HowFound, parent);

			var result = new BooleanState(unexpectedlyFalseMessage,
			                              unexpectedlyTrueMessage,
			                              () => visibility.Key);
			return result;
		}
	}
}