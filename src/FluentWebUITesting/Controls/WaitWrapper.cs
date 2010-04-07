using System.Threading;

namespace FluentWebUITesting.Controls
{
	public class WaitWrapper
	{
		private const int StandardPauseInMilliseconds = 50;

		public void ThenPause(int pauseInMilliseconds)
		{
			Thread.Sleep(pauseInMilliseconds);
		}

		public void ThenPause()
		{
			ThenPause(StandardPauseInMilliseconds);
		}
	}
}