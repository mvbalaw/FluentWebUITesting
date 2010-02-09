using System;
using System.Collections.Generic;
using System.Linq;

using WatiN.Core;

namespace FluentWebUITesting
{
	public class BrowserProvider
	{
		private readonly List<Browser> _browsers = new List<Browser>();
		private readonly BrowserSetUp _browserSetUp;

		public BrowserProvider(BrowserSetUp browserSetUp)
		{
			_browserSetUp = browserSetUp;
		}

		private Browser AttachToExistingBrowser<T>() where T : Browser
		{
			if (!_browserSetUp.CloseBrowserAfterEachTest && _browsers.Exists(x => x.GetType() == typeof(T)))
			{
				return Browser.AttachTo<T>(Find.ByUrl(x => x.Contains(_browserSetUp.BaseUrl)));
			}
			return null;
		}

		private static void Close(Browser browser)
		{
			if (browser != null)
			{
				browser.Close();
				browser.Dispose();
			}
		}

		public void CloseAllOpenBrowsers()
		{
			CloseBrowser<IE>();
			CloseBrowser<FireFox>();
		}

		private void CloseBrowser<T>() where T : Browser
		{
			Browser browser;
			if (String.IsNullOrEmpty(_browserSetUp.BaseUrl))
			{
				browser = _browsers.FirstOrDefault(x => x.GetType() == typeof(T));
				Close(browser);
			}
			else
			{
				browser = AttachToExistingBrowser<T>();
				Close(browser);
			}

			if (browser != null)
			{
				_browsers.Remove(browser);
			}
		}

		public IEnumerable<Browser> GetOpenOrNewBrowsers()
		{
			if (_browserSetUp.UseFireFox)
			{
				var firefox = AttachToExistingBrowser<FireFox>() ?? StartNewFirefox();
				yield return firefox;
			}

			if (_browserSetUp.UseInternetExplorer)
			{
				var ie = AttachToExistingBrowser<IE>() ?? StartNewIE();
				yield return ie;
			}
		}

		private FireFox StartNewFirefox()
		{
			var firefox = String.IsNullOrEmpty(_browserSetUp.BaseUrl) ? new FireFox() : new FireFox(_browserSetUp.BaseUrl);
			_browsers.Add(firefox);
			return firefox;
		}

		private IE StartNewIE()
		{
			var ie = String.IsNullOrEmpty(_browserSetUp.BaseUrl) ? new IE
				{
					AutoClose = true
				} : new IE(_browserSetUp.BaseUrl);
			_browsers.Add(ie);
			return ie;
		}
	}
}