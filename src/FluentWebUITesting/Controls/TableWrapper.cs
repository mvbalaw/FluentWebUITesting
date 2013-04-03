using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class TableWrapper : ControlWrapperBase
	{
		public TableWrapper(IWebElement table, string howFound, IWebDriver browser)
			: base(table, howFound, browser)
		{
		}

		private static IEnumerable<IWebElement> GetBodyRows(ReadOnlyCollection<IWebElement> allRows)
		{
			return allRows.Except(GetHeaderRows(allRows).Concat(GetFooterRows(allRows)));
		}

		private static IEnumerable<IWebElement> GetFooterRows(IEnumerable<IWebElement> allRows)
		{
			var footerRows = allRows.Where(x => String.Compare(x.GetParent().TagName, "tfoot", true) == 0);
			return footerRows;
		}

		private static IEnumerable<IWebElement> GetHeaderRows(ReadOnlyCollection<IWebElement> allRows)
		{
			var theadHeaderRows = allRows.Where(x => String.Compare(x.GetParent().TagName, "thead", true) == 0);
			var inlineHeaderRows = allRows.Where(x => x.FindElements(By.TagName("th")).Any());
			return theadHeaderRows.Union(inlineHeaderRows);
		}

		public IEnumerable<TableHeaderRowWrapper> Headers()
		{
			var allRows = Element.FindElements(By.TagName("tr"));
			var headers = GetHeaderRows(allRows)
				.Select((x, i) =>
				        new TableHeaderRowWrapper(x, String.Format("{0}, header row with index {1}", HowFound, i), Browser));
			return headers;
		}

		public IEnumerable<TableRowWrapper> Rows()
		{
			var allRows = Element.FindElements(By.TagName("tr"));
			var rows = GetBodyRows(allRows)
				.Select((x, i) =>
				        new TableRowWrapper(x, String.Format("{0}, row with index {1}", HowFound, i), Browser));

			return rows;
		}
	}
}