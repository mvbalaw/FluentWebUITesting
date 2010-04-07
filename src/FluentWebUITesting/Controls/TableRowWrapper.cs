using System;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public class TableRowWrapper : ControlWrapperBase
	{
		private readonly TableRow _tableRow;

		public TableRowWrapper(TableRow tableRow, string howFound)
			: base(howFound)
		{
			_tableRow = tableRow;
		}

		protected override Element Element
		{
			get { return _tableRow; }
		}

		public TableCellWrapper CellWithIndex(int index)
		{
			var cell = _tableRow.TableCell(Find.ByIndex(index));
			var tableCellWrapper = new TableCellWrapper(cell, String.Format("{0}, table cell with index {1}", HowFound, index));
			tableCellWrapper.Exists().ShouldBeTrue();
			return tableCellWrapper;
		}
	}

	public class TableHeaderRowWrapper : ControlWrapperBase
	{
		private readonly TableRow _tableRow;

		public TableHeaderRowWrapper(TableRow tableRow, string howFound)
			: base(howFound)
		{
			_tableRow = tableRow;
		}

		protected override Element Element
		{
			get { return _tableRow; }
		}

		public TableHeaderCellWrapper CellWithIndex(int index)
		{
			return new TableHeaderCellWrapper(_tableRow.ElementsWithTag("th")[index], String.Format("{0}, table header cell with index {1}", HowFound, index));
		}
	}
}