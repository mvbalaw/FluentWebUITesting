using System;

namespace FluentWebUITesting
{
	public class Notification
	{
		public string BrowserType { get; set; }
		public string Message { get; set; }
		public bool Success { get; set; }

		public override string ToString()
		{
			return String.Format("{0}: {1}", BrowserType, Message);
		}
	}
}