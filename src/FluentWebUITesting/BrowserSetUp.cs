namespace FluentWebUITesting
{
	public class BrowserSetUp
	{
		public BrowserSetUp()
		{
			CloseBrowserAfterEachTest = false;
		}

		public string BaseUrl { get; set; }
		public bool CloseBrowserAfterEachTest { get; set; }
		public bool UseFireFox { get; set; }
		public bool UseInternetExplorer { get; set; }

		public string PartialTitle { get; set; }
	}
}