using System;

using FluentWebUITesting.Accessors;
using FluentWebUITesting.Controls;

using JetBrains.Annotations;

using WatiN.Core;

namespace FluentWebUITesting.Extensions
{
	public static class BrowserExtensions
	{
		public static ButtonWrapper ButtonWithVisibleText(this Browser browser, [NotNull] string text)
		{
			const string buttonWithLabel = "button with visible text '{0}'";
			return new ButtonWrapper(browser.Button(Find.ByValue(text)), String.Format(buttonWithLabel, text));
		}

		public static DivWrapper DivWithId(this Browser browser, [NotNull] string id)
		{
			const string divWithID = "div with id '{0}'";
			return new DivWrapper(browser.Div(Find.ById(id)), String.Format(divWithID, id));
		}

		public static DropDownListWrapper DropDownListWithId(this Browser browser, [NotNull] string idOfList)
		{
			const string dropDownListWithID = "drop down list with id '{0}'";
			return new DropDownListWrapper(browser.SelectList(Find.ById(idOfList)),
			                               String.Format(dropDownListWithID, idOfList));
		}

		public static FileDownloadHandlerWrapper FileDownload(this Browser browser, Action action)
		{
			return new FileDownloadHandlerWrapper(browser, action);
		}

		public static LabelWrapper LabelWithId(this Browser browser, [NotNull] string id)
		{
			const string labelWithID = "label with id '{0}'";
			return new LabelWrapper(browser.Label(Find.ById(id)), String.Format(labelWithID, id));
		}

		public static LinkWrapper LinkWithId(this Browser browser, [NotNull] string id)
		{
			const string linkWithID = "link with id '{0}'";
			return new LinkWrapper(browser.Link(Find.ById(id)), String.Format(linkWithID, id));
		}

		public static LinkWrapper LinkWithVisibleText(this Browser browser, [NotNull] string text)
		{
			const string linkWithText = "link with visible text '{0}'";
			return new LinkWrapper(browser.Link(Find.ByText(text)), String.Format(linkWithText, text));
		}

		public static RadioButtonOptionWrapper RadioButtonOptionWithId(this Browser browser, [NotNull] string idOfOption)
		{
			const string radioButtonOptionWithID = "radio button option with id '{0}'";
			return new RadioButtonOptionWrapper(browser.RadioButton(Find.ById(idOfOption)), String.Format(radioButtonOptionWithID, idOfOption));
		}

		public static SpanWrapper SpanWithId(this Browser browser, [NotNull] string id)
		{
			const string spanWithID = "span with id '{0}'";
			return new SpanWrapper(browser.Span(Find.ById(id)), String.Format(spanWithID, id));
		}

		public static TableWrapper TableWithId(this Browser browser, [NotNull] string id)
		{
			const string tableWithID = "table with id '{0}'";
			return new TableWrapper(browser.Table(Find.ById(id)), String.Format(tableWithID, id));
		}

		public static ReadOnlyText Text(this Browser browser)
		{
			return new ReadOnlyText("Page", browser.Text);
		}

		public static TextBoxWrapper TextBoxWithId(this Browser browser, [NotNull] string id)
		{
			const string textBoxWithID = "text box with id '{0}'";
			return new TextBoxWrapper(browser
			                          	.TextField(Find.ById(id)), String.Format(textBoxWithID, id));
		}
	}
}