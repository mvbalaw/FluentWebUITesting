using System;
using WatiN.Core;
using WatiN.Core.DialogHandlers;

namespace FluentWebUITesting.Controls
{
	public class DialogHandlerWrapper
	{
		private readonly Action _action;
		private readonly Browser _browser;

		public DialogHandlerWrapper(Browser browser, Action action)
		{
			_browser = browser;
			_action = action;
		}

		public void ClickCancel()
		{
			var handler = new ConfirmDialogHandler();
			using (new UseDialogOnce(_browser.DialogWatcher, handler))
			{
				_action();
				handler.WaitUntilExists();
				handler.CancelButton.Click();
				_browser.WaitForComplete();
			}
		}

		public void ClickOk()
		{
			var handler = new ConfirmDialogHandler();
			using (new UseDialogOnce(_browser.DialogWatcher, handler))
			{
				_action();
				handler.WaitUntilExists();
				handler.OKButton.Click();
				_browser.WaitForComplete();
			}
		}
	}
}