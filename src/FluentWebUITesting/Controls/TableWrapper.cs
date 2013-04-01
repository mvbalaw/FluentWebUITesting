using System;
using System.Collections.Generic;
using System.Linq;

using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class TableWrapper : ControlWrapperBase
	{
		private readonly IWebElement _table;

		public TableWrapper(IWebElement table, string howFound)
			: base(howFound)
		{
			_table = table;
		}

		public override IWebElement Element
		{
			get { return _table; }
		}
		protected override bool ElementExists
		{
			get { return _table != null; }
		}

		private IEnumerable<IWebElement> GetBodyRows()
		{
			return _table.FindElements(By.TagName("tr"))
			             .Except(GetHeaderRows().Concat(GetFooterRows()));
		}

		private IEnumerable<IWebElement> GetFooterRows()
		{
			var rows = _table.FindElements(By.TagName("tr"));
			var footerRows = rows.Where(x => String.Compare(x.GetParent().TagName, "tfoot", true) == 0);
			return footerRows;
		}

		private IEnumerable<IWebElement> GetHeaderRows()
		{
			var rows = _table.FindElements(By.TagName("tr"));
			var theadHeaderRows = rows.Where(x => String.Compare(x.GetParent().TagName, "thead", true) == 0);
			var inlineHeaderRows = rows.Where(x => x.FindElements(By.TagName("tr")).Any());
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