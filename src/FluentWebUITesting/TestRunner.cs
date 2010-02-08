using System;
using System.Collections.Generic;
using System.Threading;

using WatiN.Core;

namespace FluentWebUITesting
{
	internal class TestRunner
	{
		private readonly BrowserProvider _browserProvider;
		private readonly ManualResetEvent _monitor = new ManualResetEvent(false);
		private readonly int _waitAfterEachStepInMilliSeconds;
		private bool _failed;

		public TestRunner(BrowserProvider browserProvider, int waitAfterEachStepInMilliSeconds)
		{
			_browserProvider = browserProvider;
			_waitAfterEachStepInMilliSeconds = waitAfterEachStepInMilliSeconds;
		}

		public string FailureReason { get; private set; }

		public void CloseBrowsers()
		{
			OpenCloseBrowsers(CloseWindows);
		}

		private void CloseWindows()
		{
			_browserProvider.Close();
			_monitor.Set();
		}

		public void Initialize()
		{
			OpenCloseBrowsers(OpenWindows);
		}

		private void OpenCloseBrowsers(Action action)
		{
			_monitor.Reset();

			var th = new Thread(action.Invoke);
			th.SetApartmentState(ApartmentState.STA);
			th.Start();

			_monitor.WaitOne();
		}

		private void OpenWindows()
		{
			_browserProvider.StartBrowsers();
			_monitor.Set();
		}

		public bool PassesTest(IEnumerable<Action<Browser>> testSteps)
		{
			_monitor.Reset();

			var thread = new Thread(RunTest);
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start(testSteps);

			_monitor.WaitOne();

			return !_failed;
		}

		private void RunTest(object steps)
		{
			var testSteps = (IEnumerable<Action<Browser>>)steps;
			foreach (var browser in _browserProvider.GetBrowsers())
			{
				try
				{
					foreach (var step in testSteps)
					{
						step(browser);
						Thread.Sleep(_waitAfterEachStepInMilliSeconds);
					}
				}
				catch (Exception exception)
				{
					_failed = true;
					FailureReason = exception.Message;
					_monitor.Set();
					return;
				}
			}
			_monitor.Set();
		}
	}
}