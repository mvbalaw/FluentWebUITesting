using System;
using System.Collections.Generic;

using FluentAssert;

using JetBrains.Annotations;

using WatiN.Core;

namespace FluentWebUITesting
{
	public class UITestRunner
	{
		private static TestRunner _runner;

		public static void CloseBrowsers()
		{
			_runner.CloseBrowsers();
		}

		public static void InitializeBrowsers(Action<BrowserSetUp> action)
		{
			var browserSetUp = new BrowserSetUp();
			action(browserSetUp);
			
			if (!browserSetUp.CloseBrowserAfterEachTest && String.IsNullOrEmpty(browserSetUp.BaseUrl))
			{
				throw new ArgumentException("Base Url cannot be empty if you are not closing your browser after each test");
			}
			_runner = new TestRunner(new BrowserProvider(browserSetUp), browserSetUp);
		}

		public static IEnumerable<Action<Browser>> InitializeWorkFlowContainer(params Action<Browser>[] steps)
		{
			return steps;
		}

		public static void RunTest([NotNull] string url,
		                           [NotNull] string initialPageTitle,
		                           [CanBeNull] IEnumerable<Action<Browser>> testSteps)
		{
			Console.WriteLine(url);

			var steps = new List<Action<Browser>>
				{
					b => b.GoTo(url),
					b => b.WaitForComplete(),
					b => b.Title.ShouldBeEqualTo(initialPageTitle, "incorrect page title")
				};
			if (testSteps != null)
			{
				steps.AddRange(testSteps);
			}

			var notification = _runner.PassesTest(steps);
			notification.Success.ShouldBeTrue(notification.Message);
		}
	}
}