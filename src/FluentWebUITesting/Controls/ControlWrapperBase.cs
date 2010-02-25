using System;

using FluentWebUITesting.Accessors;

using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public abstract class ControlWrapperBase
	{
		protected ControlWrapperBase(string howFound)
		{
			HowFound = howFound;
		}

		protected abstract Element Element { get; }
		protected string HowFound { get; private set; }

		public Result Enabled()
		{
			const string unexpectedlyFalse = "{0} is not enabled but should be.";
			const string unexpectedlyTrue = "{0} is enabled but should not be.";
			var result = new Result
				{
					Passed = Element.Enabled,
					UnexpectedlyTrueMessage = String.Format(unexpectedlyTrue, HowFound),
					UnexpectedlyFalseMessage = String.Format(unexpectedlyFalse, HowFound)
				};
			return result;
		}

		public Result Exists()
		{
			const string unexpectedlyFalse = "{0} does not exist but should.";
			const string unexpectedlyTrue = "{0} exists but should not.";
			var result = new Result
				{
					Passed = Element.Exists,
					UnexpectedlyTrueMessage = String.Format(unexpectedlyTrue, HowFound),
					UnexpectedlyFalseMessage = String.Format(unexpectedlyFalse, HowFound)
				};
			return result;
		}
	}
}