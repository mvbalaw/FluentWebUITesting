using System;
using System.Linq;

using FluentWebUITesting.Controls;

using JetBrains.Annotations;

using OpenQA.Selenium;

namespace FluentWebUITesting.Extensions
{
	public static class ISelectListExtensions
	{
		public static OptionWrapper OptionWithText(this IWebElement dropDownList, [NotNull] string text, IWebDriver browser)
		{
			const string optionWithText = "option with visible text '{0}'";
			var options = dropDownList.FindElements(By.TagName("option"));
			var option = options.FirstOrDefault(x => x.Text == text);
			return new OptionWrapper(option, String.Format(optionWithText, text), dropDownList, browser);
		}

		public static OptionWrapper OptionWithValue(this IWebElement dropDownList, [NotNull] string value, IWebDriver browser)
		{
			const string optionWithValue = "option with value '{0}'";
			var options = dropDownList.FindElements(By.TagName("option"));
			var option = options.FirstOrDefault(x => x.GetAttribute("value") == value);
			return new OptionWrapper(option, String.Format(optionWithValue, value), dropDownList, browser);
		}
	}
}