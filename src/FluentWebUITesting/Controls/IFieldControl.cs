using FluentWebUITesting.Accessors;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public interface IFieldControl
	{
		IWebDriver Browser { get; }
		IWebElement Element { get; }
		string HowFound { get; }

		IReadOnlyBooleanState Enabled()
//// ReSharper restore MemberCanBeProtected.Global
			;

		IReadOnlyBooleanState Exists();
		IReadOnlyBooleanState Visible();
	}
}