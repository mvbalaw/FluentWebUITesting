using System.Collections.Generic;
using System.Linq;

using FluentWebUITesting.Extensions;

using JetBrains.Annotations;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public class DropDownListWrapper : ControlWrapperBase
	{
		private readonly Browser _browser;
		private readonly SelectList _dropDownList;

		public DropDownListWrapper(SelectList dropDownList, Browser browser, string howFound)
			: base(howFound)
		{
			_dropDownList = dropDownList;
			_browser = browser;
		}

		protected override Element Element
		{
			get { return _dropDownList; }
		}

		public string GetSelectedText()
		{
			return _dropDownList.SelectedOption.Value;
		}

		public IEnumerable<string> GetSelectedTexts()
		{
			return ((IEnumerable<Option>)_dropDownList.SelectedOptions).Select(x => x.Value);
		}

		public void SelectOptionWithText([NotNull] string text)
		{
			Verify();
			var option = _dropDownList.OptionWithText(text);
			option.Exists().ShouldBeTrue();
			_dropDownList.Select(option.Text().GetValue());
			_browser.WaitForComplete();
		}

		public void SelectOptionWithValue([NotNull] string text)
		{
			Verify();
			var option = _dropDownList.OptionWithValue(text);
			option.Exists().ShouldBeTrue();
			_dropDownList.Select(option.Text().GetValue());
			_browser.WaitForComplete();
		}

		private void Verify()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
		}
	}
}