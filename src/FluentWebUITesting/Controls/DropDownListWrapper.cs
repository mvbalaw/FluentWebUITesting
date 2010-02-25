using System.Collections.Generic;
using System.Linq;

using FluentWebUITesting.Extensions;

using JetBrains.Annotations;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public class DropDownListWrapper : ControlWrapperBase
	{
		private readonly SelectList _dropDownList;

		public DropDownListWrapper(SelectList dropDownList, string howFound)
			: base(howFound)
		{
			_dropDownList = dropDownList;
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

		public WaitWrapper SelectOptionWithText([NotNull] string text)
		{
			Verify();
			var option = _dropDownList.OptionWithText(text);
			option.Exists().ShouldBeTrue();
			_dropDownList.Select(option.Text().GetValue());
			return new WaitWrapper();
		}

		public WaitWrapper SelectOptionWithValue([NotNull] string text)
		{
			Verify();
			var option = _dropDownList.OptionWithValue(text);
			option.Exists().ShouldBeTrue();
			_dropDownList.Select(option.Text().GetValue());
			return new WaitWrapper();
		}

		private void Verify()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
		}
	}
}