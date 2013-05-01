namespace FluentWebUITesting.Controls
{
	public interface INavigationControl : IFieldControl
	{
		WaitWrapper Click();
		void ClickNoWait();
		void MoveMouseToIt();
	}
}