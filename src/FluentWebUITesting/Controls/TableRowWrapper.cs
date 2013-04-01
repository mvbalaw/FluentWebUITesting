using System;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class TableRowWrapper : ControlWrapperBase
	{
		private readonly IWebElement _tableRow;

		public TableRowWrapper(IWebElement tableRow, string howFound)
			: base(howFound)
		{
			_tableRow = tableRow;
		}

		public override IWebElement Element
		{
			get { return _tableRow; }
		}
		protected override bool ElementExists
		{
			get { return _tableRow != null; }
		}

		public TableCellWrapper CellWithIndex(int index)
		{
			var elements = _tableRow.FindElements(By.TagName("td"));
			var cell = elements.Count > index ? elements[index] : null;
			var tableCellWrapper = new TableCellWrapper(cell, String.Format("{0}, table cell with index {1}", HowFound, index));
			tableCellWrapper.Exists().ShouldBeTrue();
			return tableCellWrapper;
		}
	}

	public class TableHeaderRowWrapper : ControlWrapperBase
	{
		private readonly IWebElement _tableRow;

		public TableHeaderRowWrapper(IWebElement tableRow, string howFound)
			: base(howFound)
		{
			_tableRow = tableRow;
		}

		public override IWebElement Element
		{
			get { return _tableRow; }
		}
		protected override bool ElementExists
		{
			get { return _tableRow != null; }
		}

		public TableHeaderCellWrapper CellWithIndex(int index)
		{
			var elements = _tableRow.FindElements(By.TagName("th"));
			var cell = elements.Count > index ? elements[index] : null;
			return new TableHeaderCellWrapper(cell, String.Format("{0}, table header cell with index {1}", HowFound, index));
		}
	}
}