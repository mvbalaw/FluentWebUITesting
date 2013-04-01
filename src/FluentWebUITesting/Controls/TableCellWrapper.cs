using FluentWebUITesting.Accessors;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class TableCellWrapper : ControlWrapperBase
	{
		private readonly IWebElement _tableCell;

		public TableCellWrapper(IWebElement tableCell, string howFound)
			: base(howFound)
		{
			_tableCell = tableCell;
		}

		public override IWebElement Element
		{
			get { return _tableCell; }
		}
		protected override bool ElementExists
		{
			get { return _tableCell != null; }
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, _tableCell.Text);
		}
	}

	public class TableHeaderCellWrapper : ControlWrapperBase
	{
		private readonly IWebElement _tableCell;

		public TableHeaderCellWrapper(IWebElement tableCell, string howFound)
			: base(howFound)
		{
			_tableCell = tableCell;
		}

		public override IWebElement Element
		{
			get { return _tableCell; }
		}
		protected override bool ElementExists
		{
			get { return _tableCell != null; }
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, _tableCell.Text);
		}
	}
}