using System;
using System.Collections.Generic;
using System.Threading;

using WatiN.Core;

namespace FluentWebUITesting
{
	internal class TestRunner
	{
		private readonly BrowserProvider _browserProvider;
		private readonly BrowserSetUp _browserSetUp;
		private readonly ManualResetEvent _monitor = new ManualResetEvent(false);
		private bool _failed;
		private string _failureReason;

		public TestRunner(BrowserProvider browserProvider, BrowserSetUp browserSetUp)
		{
			_browserProvider = browserProvider;
			_browserSetUp = browserSetUp;
		}
        
		private void CloseBrowserAfterTest()
		{
			if (_browserSetUp.CloseBrowserAfterEachTest)
			{
				_browserProvider.CloseAllOpenBrowsers();
			}
		}

		public void CloseBrowsers()
		{
			_monitor.Reset();

			var th = new Thread(CloseWindows);
			th.SetApartmentState(ApartmentState.STA);
			th.Start();

			_monitor.WaitOne();
		}

		private void CloseWindows()
		{
			_browserProvider.CloseAllOpenBrowsers();
			_monitor.Set();
		}

		public Notification PassesTest(IEnumerable<Action<Browser>> testSteps)
		{
			_monitor.Reset();

			var thread = new Thread(() => RunTest(testSteps));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();

			_monitor.WaitOne();

			return _failed ? new Notification { Success = false, Message = _failureReason } : new Notification {Success = true};
		}

		private void RunTest(object steps)
		{
			var testSteps = (IEnumerable<Action<Browser>>)steps;
			foreach (var browser in _browserProvider.GetOpenOrNewBrowsers())
			{
				try
				{
					foreach (var step in testSteps)
					{
						step(browser);
						Thread.Sleep(_browserSetUp.WaitAfterEachStepInMilliSeconds);
					}
				}
				catch (Exception exception)
				{
					_failed = true;
					_failureReason = String.Format("{0}: {1}", browser.GetType().Name, exception.Message);
					CloseBrowserAfterTest();
					_monitor.Set();
					return;
				}
			}
			_failed = false;
			CloseBrowserAfterTest();
			_monitor.Set();
		}
	}
}