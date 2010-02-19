using System;

namespace FluentWebUITesting
{
	public class Notification
	{
		public string Message { get; set; }
		public bool Success { get; set; }
		public string BrowserType { get; set; }

		public override string ToString()
		{
			return String.Format("{0}: {1}", BrowserType, Message);
		}
	}
}