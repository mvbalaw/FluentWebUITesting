using System.Collections.Generic;
using System.Linq;

using FluentWebUITesting.Extensions;

using JetBrains.Annotations;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class DropDownListWrapper : ControlWrapperBase
	{
		private readonly IWebElement _dropDownList;

		public DropDownListWrapper(IWebElement dropDownList, string howFound)
			: base(howFound)
		{
			_dropDownList = dropDownList;
		}

		public override IWebElement Element
		{
			get { return _dropDownList; }
		}

		protected override bool ElementExists
		{
			get { return _dropDownList != null; }
		}
		public IEnumerable<OptionWrapper> Options
		{
			get { return _dropDownList.FindElements(By.TagName("option")).Select(x => new OptionWrapper(x, "", _dropDownList)); }
		}

		public string GetSelectedText()
		{
			var selectedOption = _dropDownList.FindElements(By.TagName("option")).FirstOrDefault(x => x.Selected);
			return selectedOption == null ? "" : selectedOption.Text;
		}

		public IEnumerable<string> GetSelectedTexts()
		{
			return _dropDownList.FindElements(By.TagName("option")).Where(x => x.Selected).Select(x => x.Text);
		}

		public OptionWrapper OptionWithText([NotNull] string text)
		{
			Verify();
			var option = _dropDownList.OptionWithText(text);
			return option;
		}

		public OptionWrapper OptionWithValue([NotNull] string text)
		{
			Verify();
			var option = _dropDownList.OptionWithValue(text);
			return option;
		}

		private void Verify()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
		}
	}
}