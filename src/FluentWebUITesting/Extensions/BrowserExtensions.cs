using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using FluentWebUITesting.Accessors;
using FluentWebUITesting.Controls;

using JetBrains.Annotations;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace FluentWebUITesting.Extensions
{
	public static class BrowserExtensions
	{
		public static ButtonWrapper ButtonWithId(this IWebDriver browser, [NotNull] string id)
		{
			const string howFound = "button with id '{0}'";
			var button = browser.TryGetElementByIdAndInputType(id, "submit") ?? browser.TryGetElementByIdAndInputType(id, "button");
			return new ButtonWrapper(button, String.Format(howFound, id), browser);
		}

		public static ButtonWrapper ButtonWithVisibleText(this IWebDriver browser, [NotNull] string text)
		{
			const string howFound = "button with visible text '{0}'";
			var button = browser.Buttons().FirstOrDefault(x => x.Element.GetAttribute("value") == text);
			return new ButtonWrapper(button == null ? null : button.Element, String.Format(howFound, text), browser);
		}

		public static IEnumerable<ButtonWrapper> Buttons(this IWebDriver browser)
		{
			const string howFound = "type 'button'";
			var submits = browser.GetInputsByInputType("submit");
			var buttons = browser.GetInputsByInputType("button");
			return submits.Concat(buttons).Select(x => new ButtonWrapper(x, howFound, browser));
		}

		public static CheckBoxWrapper CheckBoxWithId(this IWebDriver browser, [NotNull] string id)
		{
			const string howFound = "checkbox with id '{0}'";
			var checkBox = browser.TryGetElementByIdAndInputType(id, "checkbox");
			return new CheckBoxWrapper(checkBox, String.Format(howFound, id), browser);
		}

		public static CheckBoxWrapper CheckBoxWithIdAndValue(this IWebDriver browser, [NotNull] string id, [NotNull] string value)
		{
			const string howFound = "checkbox with id '{0}' and value '{1}'";
			var checkBox = browser.FindElements(By.Id(id)).Where(x => x.GetAttribute("type") == "checkbox").FirstOrDefault(x => x.GetAttribute("value") == value);
			return new CheckBoxWrapper(checkBox, String.Format(howFound, id, value), browser);
		}

		public static IEnumerable<CheckBoxWrapper> CheckBoxes(this IWebDriver browser)
		{
			const string howFound = "type 'checkbox'";
			var checkBoxes = browser.GetInputsByInputType("checkbox");
			return checkBoxes.Select(x => new CheckBoxWrapper(x, howFound, browser));
		}

		public static DialogHandlerWrapper Dialog(this IWebDriver browser, Action action)
		{
			return new DialogHandlerWrapper(browser, action);
		}

		public static DivWrapper DivWithId(this IWebDriver browser, [NotNull] string id)
		{
			const string howFound = "div with id '{0}'";
			var div = browser.TryGetElementByIdAndTagType(id, "div");
			return new DivWrapper(div, String.Format(howFound, id));
		}

		public static IEnumerable<DivWrapper> Divs(this IWebDriver browser)
		{
			const string howFound = "type 'div'";
			var divs = browser.GetElementsByTagType("div");
			return divs.Select(x => new DivWrapper(x, howFound));
		}

		public static DropDownListWrapper DropDownListWithId(this IWebDriver browser, [NotNull] string idOfList)
		{
			const string howFound = "drop down list with id '{0}'";
			var dropDownList = browser.TryGetElementByIdAndTagType(idOfList, "select");
			return new DropDownListWrapper(dropDownList,
			                               String.Format(howFound, idOfList));
		}

		public static IEnumerable<DropDownListWrapper> DropDownLists(this IWebDriver browser)
		{
			const string howFound = "type 'select'";
			var dropDowns = browser.GetElementsByTagType("select");
			return dropDowns.Select(x => new DropDownListWrapper(x, howFound));
		}

		public static void Focus(this IWebDriver browser, IWebElement element)
		{
			new Actions(browser).MoveToElement(element).Perform();
		}

		private static IEnumerable<IWebElement> GetElementsByTagType(this IWebDriver browser, string tag)
		{
			return browser.FindElements(By.TagName(tag));
		}

		private static IEnumerable<IWebElement> GetInputs(this IWebDriver browser)
		{
			return browser.FindElements(By.TagName("input"));
		}

		private static IEnumerable<IWebElement> GetInputsByInputType(this IWebDriver browser, string type)
		{
			return browser.GetInputs().Where(x => x.GetAttribute("type") == type);
		}

		public static TextBoxWrapper HiddenWithId(this IWebDriver browser, string id)
		{
			const string howFound = "hidden input with id '{0}'";
			var hidden = browser.TryGetElementByIdAndInputType(id, "hidden");
			return new TextBoxWrapper(hidden, String.Format(howFound, id));
		}

		public static IEnumerable<TextBoxWrapper> Hiddens(this IWebDriver browser)
		{
			const string howFound = "type 'hidden'";
			var hiddens = browser.GetElementsByTagType("input").Where(x => x.GetAttribute("type") == "hidden");
			return hiddens.Select(x => new TextBoxWrapper(x, howFound));
		}

		public static IEnumerable<ButtonWrapper> ImageButtons(this IWebDriver browser)
		{
			const string howFound = "type 'img'";
			var images = browser.GetElementsByTagType("img");
			return images.Select(x => new ButtonWrapper(x, howFound, browser));
		}

		public static ButtonWrapper ImageWithId(this IWebDriver browser, [NotNull] string id)
		{
			const string howFound = "image with id '{0}'";
			var button = browser.TryGetElementByIdAndTagType(id, "img");
			return new ButtonWrapper(button, String.Format(howFound, id), browser);
		}

		public static LabelWrapper LabelWithId(this IWebDriver browser, [NotNull] string id)
		{
			const string howFound = "label with id '{0}'";
			var label = browser.TryGetElementByIdAndTagType(id, "label");
			return new LabelWrapper(label, String.Format(howFound, id));
		}

		public static IEnumerable<LabelWrapper> Labels(this IWebDriver browser)
		{
			const string howFound = "type 'label'";
			var labels = browser.GetElementsByTagType("label");
			return labels.Select(x => new LabelWrapper(x, howFound));
		}

		public static LinkWrapper LinkWithId(this IWebDriver browser, [NotNull] string id)
		{
			const string howFound = "link with id '{0}'";
			var link = browser.TryGetElementByIdAndTagType(id, "a");
			return new LinkWrapper(link, String.Format(howFound, id), browser);
		}

		public static LinkWrapper LinkWithVisibleText(this IWebDriver browser, [NotNull] string text)
		{
			const string howFound = "link with visible text '{0}'";
			var link = browser.FindElements(By.LinkText(text)).FirstOrDefault();
			return new LinkWrapper(link, String.Format(howFound, text), browser);
		}

		public static IEnumerable<LinkWrapper> Links(this IWebDriver browser)
		{
			const string howFound = "type 'a'";
			var links = browser.GetElementsByTagType("a");
			return links.Select(x => new LinkWrapper(x, howFound, browser));
		}

		public static void Pause(this IWebDriver browser, int milliseconds)
		{
			Thread.Sleep(milliseconds);
		}

		public static RadioButtonOptionWrapper RadioButtonOptionWithId(this IWebDriver browser, [NotNull] string idOfOption)
		{
			const string howFound = "radio button option with id '{0}'";
			var radioButton = browser.TryGetElementByIdAndInputType(idOfOption, "radio");
			return new RadioButtonOptionWrapper(radioButton,
			                                    String.Format(howFound, idOfOption),
			                                    browser);
		}

		public static IEnumerable<RadioButtonOptionWrapper> RadioButtonsOptions(this IWebDriver browser)
		{
			const string howFound = "type 'radio'";
			var radios = browser.GetInputsByInputType("radio");
			return radios.Select(x => new RadioButtonOptionWrapper(x, howFound, browser));
		}

		public static SpanWrapper SpanWithId(this IWebDriver browser, [NotNull] string id)
		{
			const string howFound = "span with id '{0}'";
			var span = browser.TryGetElementByIdAndTagType(id, "span");
			return new SpanWrapper(span, String.Format(howFound, id));
		}

		public static IEnumerable<SpanWrapper> Spans(this IWebDriver browser)
		{
			const string howFound = "type 'span'";
			var spans = browser.GetElementsByTagType("span");
			return spans.Select(x => new SpanWrapper(x, howFound));
		}

		public static TableWrapper TableWithId(this IWebDriver browser, [NotNull] string id)
		{
			const string howFound = "table with id '{0}'";
			var table = browser.TryGetElementByIdAndTagType(id, "table");
			return new TableWrapper(table, String.Format(howFound, id));
		}

		public static IEnumerable<TableWrapper> Tables(this IWebDriver browser)
		{
			const string howFound = "type 'table'";
			var tables = browser.GetElementsByTagType("table");
			return tables.Select(x => new TableWrapper(x, howFound));
		}

		public static ReadOnlyText Text(this IWebDriver browser)
		{
			return new ReadOnlyText("Page", browser.PageSource);
		}

		public static TextBoxWrapper TextBoxWithId(this IWebDriver browser, [NotNull] string id)
		{
			const string howFound = "text box with id '{0}'";
			var textField = browser.TryGetElementByIdAndInputType(id, "text") ?? browser.TryGetElementByIdAndInputType(id, "textarea");
			return new TextBoxWrapper(textField, String.Format(howFound, id));
		}

		public static IEnumerable<TextBoxWrapper> TextBoxes(this IWebDriver browser)
		{
			var textBoxes = browser.GetInputsByInputType("text").Select(x => new TextBoxWrapper(x, "type 'text'"));
			var textAreas = browser.GetInputsByInputType("textarea").Select(x => new TextBoxWrapper(x, "type 'textarea'"));
			return textBoxes.Concat(textAreas);
		}

		private static IWebElement TryGetElementByIdAndInputType(this IWebDriver browser, string id, string type)
		{
			return browser.FindElements(By.Id(id)).FirstOrDefault(x => x.GetAttribute("type") == type);
		}

		private static IWebElement TryGetElementByIdAndTagType(this IWebDriver browser, string id, string tag)
		{
			return browser.FindElements(By.Id(id)).FirstOrDefault(x => x.TagName == tag);
		}
	}
}