using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace FluentWebUITesting
{
	public class BrowserProvider
	{
		private static string _hwnd;
		private readonly BrowserSetUp _browserSetUp;
		private readonly List<IWebDriver> _browsers = new List<IWebDriver>();

		public BrowserProvider(BrowserSetUp browserSetUp)
		{
			_browserSetUp = browserSetUp;
		}

		private IWebDriver AttachToExistingBrowser<T>() where T : IWebDriver
		{
			if (!_browserSetUp.CloseBrowserAfterEachTest && _browsers.Exists(x => x.GetType() == typeof(T)))
			{
				var browser = _browsers.First(x => x.GetType() == typeof(T));
				return browser;
			}
			return null;
		}

		private void Close(IWebDriver browser)
		{
			if (browser != null)
			{
				_browsers.Remove(browser);
				var windowHandle = browser.CurrentWindowHandle;
				try
				{
					browser.Quit();
					browser.Dispose();
				}
				catch
				{
					if (windowHandle != null)
					{
						var process = Process.GetProcesses().FirstOrDefault(x => x.MainWindowHandle == new IntPtr(Int32.Parse(windowHandle)));
						if (process != null)
						{
							process.Kill();
						}
					}
				}
			}
		}

		public void CloseAllOpenBrowsers()
		{
			CloseBrowser<InternetExplorerDriver>();
			CloseBrowser<FirefoxDriver>();
			CloseBrowser<ChromeDriver>();
		}

		private void CloseBrowser<T>() where T : IWebDriver
		{
			if (String.IsNullOrEmpty(_browserSetUp.BaseUrl) || _browserSetUp.CloseBrowserAfterEachTest)
			{
				var browser = _browsers.FirstOrDefault(x => x.GetType() == typeof(T));
				Close(browser);
			}
			else
			{
				var browser = AttachToExistingBrowser<T>();
				Close(browser);
			}
		}

		public IEnumerable<IWebDriver> GetOpenOrNewBrowsers()
		{
			if (_browserSetUp.UseChrome)
			{
				var driver = AttachToExistingBrowser<ChromeDriver>() ?? new ChromeDriver();
				_hwnd = driver.CurrentWindowHandle;
				_browsers.Add(driver);
				yield return driver;
			}

			if (_browserSetUp.UseFireFox)
			{
				var driver = AttachToExistingBrowser<FirefoxDriver>() ?? new FirefoxDriver();
				_hwnd = driver.CurrentWindowHandle;
				_browsers.Add(driver);
				yield return driver;
			}

			if (_browserSetUp.UseInternetExplorer)
			{
				var driver = AttachToExistingBrowser<InternetExplorerDriver>() ?? new InternetExplorerDriver();
				_hwnd = driver.CurrentWindowHandle;
				_browsers.Add(driver);
				yield return driver;
			}
		}
	}
}