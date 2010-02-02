using System;
using System.Collections.Generic;

using FluentAssert;

using JetBrains.Annotations;

using WatiN.Core;

namespace FluentWebUITesting
{
	public class UITestRunner
	{
		private static BrowserSetUp _browserSetUp;

		public static void Initialize(Action<BrowserSetUp> action)
		{
			_browserSetUp = new BrowserSetUp();
			action(_browserSetUp);
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
			var runner = new TestRunner(_browserSetUp, steps);

			runner.PassesTest().ShouldBeTrue(runner.FailureReason);
		}
	}
}