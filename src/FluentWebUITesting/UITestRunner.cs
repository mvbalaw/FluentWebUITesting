using System;
using System.Collections.Generic;
using System.Threading;

using FluentAssert;

using JetBrains.Annotations;

using WatiN.Core;

namespace FluentWebUITesting
{
    public class UITestRunner
    {
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
            var runner = new Runner(steps);

            runner.PassesTest().ShouldBeTrue(runner.FailureReason);
        }

        private class Runner
        {
            private readonly ManualResetEvent _monitor = new ManualResetEvent(false);
            private readonly IEnumerable<Action<Browser>> _steps;
            private bool _failed;

            public Runner(IEnumerable<Action<Browser>> testSteps)
            {
                _steps = testSteps;
            }

            public string FailureReason { get; private set; }

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
                Browser browser = null;
                try
                {
                    //todo figure out IE Settings in new WatiN
                    //					IE.Settings.AutoMoveMousePointerToTopLeft = false;
                    //					IE.Settings.WaitForCompleteTimeOut = 120;
                    browser = new IE
                        {
                            AutoClose = true
                        };
                    foreach (var step in _steps)
                    {
                        step(browser);
                    }
                }
                catch (Exception exception)
                {
                    _failed = true;
                    FailureReason = exception.Message;
                    return;
                }
                finally
                {
                    if (browser != null)
                    {
                        browser.Dispose();
                    }
                    _monitor.Set();
                }
            }
        }
    }
}