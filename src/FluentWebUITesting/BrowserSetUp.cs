namespace FluentWebUITesting
{
	public class BrowserSetUp
	{
		public BrowserSetUp()
		{
			CloseBrowserAfterEachTest = false;
			WaitAfterEachStepInMilliSeconds = 300;
		}

		public string BaseUrl { get; set; }
		public bool CloseBrowserAfterEachTest { get; set; }
		public bool UseFireFox { get; set; }
		public bool UseInternetExplorer { get; set; }
		public int WaitAfterEachStepInMilliSeconds { get; set; }
	}
}