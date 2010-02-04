using System;
using System.Collections.Generic;
using System.Threading;

using WatiN.Core;

namespace FluentWebUITesting
{
	internal class TestRunner
	{
		private readonly BrowserSetUp _browserSetUp;
		private readonly ManualResetEvent _monitor = new ManualResetEvent(false);
		private readonly IEnumerable<Action<Browser>> _steps;
		private bool _failed;

		public TestRunner(BrowserSetUp browserSetUp, IEnumerable<Action<Browser>> testSteps)
		{
			_browserSetUp = browserSetUp;
			_steps = testSteps;
		}

		public string FailureReason { get; private set; }

		private IEnumerable<Browser> GetBrowsers()
		{
			if (_browserSetUp.UseFireFox)
			{
				yield return new FireFox();
			}
			if (_browserSetUp.UseInternetExplorer)
			{
				yield return new IE
					{
						AutoClose = _browserSetUp.AutoCloseIE
					};
			}
		}

		public bool PassesTest()
		{
			_monitor.Reset();

			var th = new Thread(RunTest);
			th.SetApartmentState(ApartmentState.STA);
			th.Start();

			_monitor.WaitOne();

			return !_failed;
		}

		private void RunTest()
		{
			foreach (var browser in GetBrowsers())
			{
				try
				{
					foreach (var step in _steps)
					{
						step(browser);
					}
				}
				catch (Exception exception)
				{
					_failed = true;
					FailureReason = exception.Message;
					_monitor.Set();
					return;
				}
				finally
				{
					if (browser != null)
					{
						browser.Close();
						browser.Dispose();
					}
				}
			}
			_monitor.Set();
		}
	}
}