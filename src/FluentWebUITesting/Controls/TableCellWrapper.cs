using FluentWebUITesting.Accessors;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public class TableCellWrapper : ControlWrapperBase
	{
		private readonly TableCell _tableCell;

		public TableCellWrapper(TableCell tableCell, string howFound)
			: base(howFound)
		{
			_tableCell = tableCell;
		}

		protected override Element Element
		{
			get { return _tableCell; }
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, _tableCell.Text);
		}
	}

	public class TableHeaderCellWrapper : ControlWrapperBase
	{
		private readonly Element _tableCell;

		public TableHeaderCellWrapper(Element tableCell, string howFound)
			: base(howFound)
		{
			_tableCell = tableCell;
		}

		protected override Element Element
		{
			get { return _tableCell; }
		}

		public ReadOnlyText Text()
		{
			return new ReadOnlyText(HowFound, _tableCell.Text);
		}
	}
}