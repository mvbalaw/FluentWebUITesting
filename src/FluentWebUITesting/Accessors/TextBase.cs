using System;

using FluentAssert;

using JetBrains.Annotations;

namespace FluentWebUITesting.Accessors
{
	public abstract class TextBase
	{
		private readonly string _howFound;

		protected TextBase(string text, string howFound)
		{
			Text = text;
			_howFound = howFound;
		}

		private string Text { get; set; }

		public Result Contains([NotNull] string containedText)
		{
			const string unexpectedlyTrue = "The text '{0}' in {1} should not start with '{2}'";
			const string unexpectedlyFalse = "The text '{0}' in {1} should start with '{2}'";
			var result = new Result
				{
					Passed = Text.Contains(containedText),
					UnexpectedlyTrueMessage = String.Format(unexpectedlyTrue, Text, _howFound, containedText),
					UnexpectedlyFalseMessage = String.Format(unexpectedlyFalse, Text, _howFound, containedText)
				};
			return result;
		}

		public string GetValue()
		{
			return Text;
		}

		public void ShouldBeEqualTo([NotNull] string text)
		{
			const string unexpectedlyNotEqual = "The text '{0}' in {1} should be '{2}'";
			ShouldBeEqualTo(text, String.Format(unexpectedlyNotEqual, Text, _howFound, text));
		}

		public void ShouldBeEqualTo([NotNull] string text, string errorMessage)
		{
			Text.ShouldBeEqualTo(text, errorMessage);
		}

		public void ShouldNotBeEqualTo([NotNull] string text)
		{
			const string unexpectedlyEqual = "The text '{0}' in {1} should not be '{2}'";
			ShouldNotBeEqualTo(text, String.Format(unexpectedlyEqual, Text, _howFound, text));
		}

		public void ShouldNotBeEqualTo([NotNull] string text, string errorMessage)
		{
			Text.ShouldNotBeEqualTo(text, errorMessage);
		}

		public Result StartsWith([NotNull] string startingText)
		{
			const string unexpectedlyTrue = "The text '{0}' in {1} should not contain '{2}'";
			const string unexpectedlyFalse = "The text '{0}' in {1} should contain '{2}'";
			var result = new Result
				{
					Passed = Text.StartsWith(startingText),
					UnexpectedlyTrueMessage = String.Format(unexpectedlyTrue, Text, _howFound, startingText),
					UnexpectedlyFalseMessage = String.Format(unexpectedlyFalse, Text, _howFound, startingText)
				};
			return result;
		}
	}
}