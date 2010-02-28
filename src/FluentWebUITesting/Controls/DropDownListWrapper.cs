using System;
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

		[Obsolete("Use .OptionWithText(text).Select()")]
		public WaitWrapper SelectOptionWithText([NotNull] string text)
		{
			var option = OptionWithText(text);
			return option.Select();
		}

		[Obsolete("Use .OptionWithValue(text).Select()")]
		public WaitWrapper SelectOptionWithValue([NotNull] string text)
		{
			var option = OptionWithValue(text);
			return option.Select();
		}

		private void Verify()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
		}
	}
}