namespace FluentWebUITesting
{
	public class BrowserSetUp
	{
		public BrowserSetUp()
		{
			AutoCloseIE = true;
			UseFireFox = true;
			WaitAfterEachStepInMilliSeconds = 400;
		}

		public bool AutoCloseIE { get; set; }
		public string BaseUrl { get; set; }
		public bool UseFireFox { get; set; }
		public bool UseInternetExplorer { get; set; }
		public int WaitAfterEachStepInMilliSeconds { get; set; }
	}
}