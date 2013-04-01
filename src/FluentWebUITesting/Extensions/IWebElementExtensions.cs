using OpenQA.Selenium;

namespace FluentWebUITesting.Extensions
{
	public static class IWebElementExtensions
	{
		public static IWebElement GetParent(this IWebElement e)
		{
			// http://watirmelon.com/2012/07/25/getting-an-elements-parent-in-webdriver-in-c/
			IWebElement parent = null;
			try
			{
				parent = e.FindElement(By.XPath(".."));
			}
			catch (InvalidSelectorException)
			{
				// happens if the parent is the document
			}

			return parent;
		}
	}
}