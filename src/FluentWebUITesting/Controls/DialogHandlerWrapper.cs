using System;
using System.Linq;

using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class DialogHandlerWrapper
	{
		private readonly Action _action;
		private readonly IWebDriver _browser;

		public DialogHandlerWrapper(IWebDriver browser, Action action)
		{
			_browser = browser;
			_action = action;
		}

		public void ClickCancel()
		{
			var parentHandle = _browser.CurrentWindowHandle;

			try
			{
				var handles = _browser.WindowHandles.Except(new[] { parentHandle });
				_browser.SwitchTo().Window(handles.Last());
				_action();
				_browser.ButtonWithVisibleText("Cancel").Click();
			}
			finally
			{
				_browser.SwitchTo().Window(parentHandle);
			}
		}

		public void ClickOk()
		{
			var parentHandle = _browser.CurrentWindowHandle;

			try
			{
				var handles = _browser.WindowHandles.Except(new[] { parentHandle });
				_browser.SwitchTo().Window(handles.Last());
				_action();
				_browser.ButtonWithVisibleText("OK").Click();
			}
			finally
			{
				_browser.SwitchTo().Window(parentHandle);
			}
		}
	}
}