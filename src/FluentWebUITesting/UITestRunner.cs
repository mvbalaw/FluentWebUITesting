using System;
using System.Collections.Generic;

using FluentAssert;

using JetBrains.Annotations;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FluentWebUITesting
{
	public static class UITestRunner
	{
		private static TestRunner _runner;

		public static void CloseBrowsers()
		{
			_runner.CloseBrowsers();
		}

		public static IBrowserProxy GetBrowser()
		{
			return _runner;
		}

		public static void InitializeBrowsers(Action<BrowserSetUp> action)
		{
			if (_runner != null)
			{
				return;
			}
			var browserSetUp = new BrowserSetUp();
			if (action != null)
			{
				action(browserSetUp);
			}

			if (!browserSetUp.CloseBrowserAfterEachTest && String.IsNullOrEmpty(browserSetUp.BaseUrl))
			{
				throw new ArgumentException("Base Url cannot be empty if you are not closing your browser after each test");
			}
			_runner = new TestRunner(new BrowserProvider(browserSetUp), browserSetUp);
		}

		public static IEnumerable<Action<IWebDriver>> InitializeWorkFlowContainer(params Action<IWebDriver>[] steps)
		{
			return steps;
		}

		public static void RunTest([NotNull] string url,
		                           [NotNull] string initialPageTitle,
		                           [CanBeNull] IEnumerable<Action<IWebDriver>> testSteps)
		{
			Console.WriteLine(url);

			var steps = new List<Action<IWebDriver>>
				            {
					            b => b.Navigate().GoToUrl(url),
					            b =>
						            {
							            if (!String.IsNullOrEmpty(initialPageTitle))
							            {
								            var wait = new WebDriverWait(b, TimeSpan.FromSeconds(30));
								            wait.Until(x => b.Title.Equals(initialPageTitle));

								            b.Title.ShouldBeEqualTo(initialPageTitle, "incorrect page title");
							            }
						            }
				            };
			if (testSteps != null)
			{
				steps.AddRange(testSteps);
			}

			var notification = _runner.PassesTest(steps) ?? new Notification();
			notification.Success.ShouldBeTrue(notification.ToString());
		}
	}
}