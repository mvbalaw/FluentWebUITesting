using System.Collections.Generic;
using System.Linq;

using FluentWebUITesting.Extensions;

using JetBrains.Annotations;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class DropDownListWrapper : ControlWrapperBase
	{
		public DropDownListWrapper(IWebElement dropDownList, string howFound, IWebDriver browser)
			: base(dropDownList, howFound, browser)
		{
		}

		public IEnumerable<OptionWrapper> Options
		{
			get { return Element.FindElements(By.TagName("option")).Select(x => new OptionWrapper(x, "", Element, Browser)); }
		}

		public string GetSelectedText()
		{
			var selectedOption = Element.FindElements(By.TagName("option")).FirstOrDefault(x => x.Selected);
			return selectedOption == null ? "" : selectedOption.Text;
		}

		public IEnumerable<string> GetSelectedTexts()
		{
			return Element.FindElements(By.TagName("option")).Where(x => x.Selected).Select(x => x.Text);
		}

		public OptionWrapper OptionWithText([NotNull] string text)
		{
			Verify();
			var option = Element.OptionWithText(text, Browser);
			return option;
		}

		public OptionWrapper OptionWithValue([NotNull] string text)
		{
			Verify();
			var option = Element.OptionWithValue(text, Browser);
			return option;
		}

		private void Verify()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
		}
	}
}