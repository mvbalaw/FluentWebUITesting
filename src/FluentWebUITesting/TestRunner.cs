using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using OpenQA.Selenium;

namespace FluentWebUITesting
{
	public interface IBrowserProxy
	{
		void Do(Action<IWebDriver> action);
	}

	public class TestRunner : IBrowserProxy
	{
		private readonly BrowserProvider _browserProvider;
		private readonly BrowserSetUp _browserSetUp;
		private readonly ManualResetEvent _monitor = new ManualResetEvent(false);

		public TestRunner(BrowserProvider browserProvider, BrowserSetUp browserSetUp)
		{
			_browserProvider = browserProvider;
			_browserSetUp = browserSetUp;
		}

		public void Do(Action<IWebDriver> action)
		{
			var browser = _browserProvider.GetOpenOrNewBrowsers().First();
			Do(action, browser);
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

		private static void Do(Action<IWebDriver> action, IWebDriver browser)
		{
			action(browser);
			//browser.WaitForComplete();
		}

		public Notification PassesTest(IEnumerable<Action<IWebDriver>> testSteps)
		{
			_monitor.Reset();

			Notification notification = null;
			var thread = new Thread(() => RunTest(testSteps, out notification));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();

			_monitor.WaitOne();

			return notification;
		}

		private void RunTest(object steps, out Notification notification)
		{
			var testSteps = (IEnumerable<Action<IWebDriver>>)steps;
			try
			{
				foreach (var browser in _browserProvider.GetOpenOrNewBrowsers())
				{
					try
					{
						foreach (var step in testSteps)
						{
							Do(step, browser);
						}
					}
					catch (Exception exception)
					{
						notification = new Notification
							               {
								               Success = false,
								               Message = exception.Message,
								               BrowserType = browser.GetType().Name
							               };
						CloseBrowserAfterTest();
						_monitor.Set();
						return;
					}
				}
			}
			catch (Exception exception)
			{
				notification = new Notification
					               {
						               Success = false,
						               Message = "While getting browser: " + exception.Message
					               };
				_monitor.Set();
				return;
			}
			notification = new Notification
				               {
					               Success = true
				               };
			CloseBrowserAfterTest();
			_monitor.Set();
		}
	}
}