using System;
using System.Collections.Generic;

using FluentWebUITesting.Accessors;
using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public abstract class ControlWrapperBase
	{
		protected ControlWrapperBase(string howFound)
		{
			HowFound = howFound;
		}

		public abstract IWebElement Element { get; }
		protected abstract bool ElementExists { get; }
		protected string HowFound { get; private set; }
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

		public IReadOnlyBooleanState Enabled()
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