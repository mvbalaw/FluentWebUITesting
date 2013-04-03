using FluentWebUITesting.Accessors;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class TableCellWrapper : ControlWrapperBase
	{
		public TableCellWrapper(IWebElement tableCell, string howFound, IWebDriver browser)
			: base(tableCell, howFound, browser)
		{
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, Element.Text);
		}
	}

	public class TableHeaderCellWrapper : ControlWrapperBase
	{
		public TableHeaderCellWrapper(IWebElement tableCell, string howFound, IWebDriver browser)
			: base(tableCell, howFound, browser)
		{
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, Element.Text);
		}
	}
}