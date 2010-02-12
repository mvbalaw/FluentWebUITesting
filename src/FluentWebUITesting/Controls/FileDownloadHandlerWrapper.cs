using System;
using System.IO;

using FluentAssert;

using WatiN.Core;
using WatiN.Core.DialogHandlers;

namespace FluentWebUITesting.Controls
{
	public class FileDownloadHandlerWrapper
	{
		private readonly Action _action;
		private readonly Browser _browser;

		public FileDownloadHandlerWrapper(Browser browser, Action action)
		{
			_browser = browser;
			_action = action;
		}

		public void ClickCancel()
		{
			var handler = new FileDownloadHandler(FileDownloadOptionEnum.Cancel);
			using (new UseDialogOnce(_browser.DialogWatcher, handler))
			{
				_action();
				handler.WaitUntilFileDownloadDialogIsHandled(30);
			}
		}

		public void ClickSave(string fullFileNameWithPath)
		{
			var handler = new FileDownloadHandler(fullFileNameWithPath);
			using (new UseDialogOnce(_browser.DialogWatcher, handler))
			{
				_action();
				handler.WaitUntilFileDownloadDialogIsHandled(30);
				handler.WaitUntilDownloadCompleted(300);
				File.Exists(fullFileNameWithPath).ShouldBeTrue(String.Format("file {0} should exist", fullFileNameWithPath));
				File.Delete(fullFileNameWithPath);
			}
		}
	}
}