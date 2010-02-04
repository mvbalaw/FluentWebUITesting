using System;
using System.Collections.Generic;
using System.Linq;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public class TableWrapper : ControlWrapperBase
	{
		private readonly Table _table;

		public TableWrapper(Table table, string howFound)
			: base(howFound)
		{
			_table = table;
		}

		protected override Element Element
		{
			get { return _table; }
		}

		private IEnumerable<TableRow> GetBodyRows()
		{
			var tableBody = _table.TableBodies.FirstOrDefault();

			if (tableBody != null)
			{
				return tableBody.OwnTableRows;
			}
			return _table.OwnTableRows
				.Except(GetHeaderRows().Concat(GetFooterRows()));
		}

		private IEnumerable<TableRow> GetFooterRows()
		{
			var rows = _table.OwnTableRows;
			var footerRows = rows.Filter(TableRow.IsFooterRow());
			return footerRows;
		}

		private IEnumerable<TableRow> GetHeaderRows()
		{
			var rows = _table.OwnTableRows;
			var theadHeaderRows = rows.Filter(Find.ByElement(element => String.Compare(element.Parent.TagName, "thead", true) == 0));
			var inlineHeaderRows = rows.Filter(Find.ByElement(e => e.NativeElement.Children.GetElementsByTag("th").Any()));
			return theadHeaderRows.Union(inlineHeaderRows);
		}

		public IEnumerable<TableHeaderRowWrapper> Headers()
		{
			var headers = GetHeaderRows()
				.Select((x, i) =>
				        new TableHeaderRowWrapper(x, String.Format("{0}, header row with index {1}", HowFound, i)));
			return headers;
		}

		public IEnumerable<TableRowWrapper> Rows()
		{
			var rows = GetBodyRows()
				.Select((x, i) =>
				        new TableRowWrapper(x, String.Format("{0}, row with index {1}", HowFound, i)));

			return rows;
		}
	}
}