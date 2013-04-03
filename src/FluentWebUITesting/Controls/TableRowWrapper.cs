using System;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class TableRowWrapper : ControlWrapperBase
	{
		public TableRowWrapper(IWebElement tableRow, string howFound, IWebDriver browser)
			: base(tableRow, howFound, browser)
		{
		}

		public TableCellWrapper CellWithIndex(int index)
		{
			var elements = Element.FindElements(By.TagName("td"));
			var cell = elements.Count > index ? elements[index] : null;
			var tableCellWrapper = new TableCellWrapper(cell, String.Format("{0}, table cell with index {1}", HowFound, index), Browser);
			tableCellWrapper.Exists().ShouldBeTrue();
			return tableCellWrapper;
		}
	}

	public class TableHeaderRowWrapper : ControlWrapperBase
	{
		public TableHeaderRowWrapper(IWebElement tableRow, string howFound, IWebDriver browser)
			: base(tableRow, howFound, browser)
		{
		}

		public TableHeaderCellWrapper CellWithIndex(int index)
		{
			var elements = Element.FindElements(By.TagName("th"));
			var cell = elements.Count > index ? elements[index] : null;
			return new TableHeaderCellWrapper(cell, String.Format("{0}, table header cell with index {1}", HowFound, index), Browser);
		}
	}
}