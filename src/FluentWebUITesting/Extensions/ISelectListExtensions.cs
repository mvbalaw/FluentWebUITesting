using System;

using FluentWebUITesting.Controls;

using JetBrains.Annotations;

using WatiN.Core;

namespace FluentWebUITesting.Extensions
{
    public static class ISelectListExtensions
    {
        public static OptionWrapper OptionWithText(this SelectList dropDownList, [NotNull] string text)
        {
            const string optionWithText = "option with visible text '{0}'";
            return new OptionWrapper(dropDownList.Option(Find.ByText(text)), String.Format(optionWithText, text));
        }
    }
}