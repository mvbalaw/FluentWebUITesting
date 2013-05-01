using System;

using FluentWebUITesting.Accessors;
using FluentWebUITesting.Extensions;

using OpenQA.Selenium;

namespace FluentWebUITesting.Controls
{
	public class CheckBoxWrapper : ControlWrapperBase
	{
		public CheckBoxWrapper(IWebElement checkBox, string howFound, IWebDriver browser)
			: base(checkBox, howFound, browser)
		{
		}

		public BooleanState CheckedState()
		{
			Exists().ShouldBeTrue();
			return new BooleanState(HowFound + " should have been checked but was not",
			                        HowFound + " should not have been checked but was",
									() => Element.Selected,
			                        value =>
				                        {
											Browser.Focus(Element);
					                        try
					                        {
						                        Element.Click();
					                        }
											catch (InvalidOperationException invalidOperationException)
					                        {
						                        if (invalidOperationException.Message.Contains("Other element would receive the click"))
						                        {
													Browser.ScrollElementIntoView(Element);
													Browser.Focus(Element);
													Element.Click();
												}
					                        }
				                        });
		}
	}
}