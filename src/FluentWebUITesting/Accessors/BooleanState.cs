using System;

using FluentAssert;

using FluentWebUITesting.Controls;

namespace FluentWebUITesting.Accessors
{
	public interface IReadOnlyBooleanState
	{
		bool IsTrue { get; }
		void ShouldBeFalse();
		void ShouldBeFalse(string errorMessage);
		void ShouldBeTrue();
		void ShouldBeTrue(string errorMessage);
	}

	public class BooleanState : IReadOnlyBooleanState
	{
		private readonly Func<bool> _getState;
		private readonly Action<bool> _setState;
		private readonly string _unexpectedlyFalseMessage;
		private readonly string _unexpectedlyTrueMessage;

		public BooleanState(string unexpectedlyFalseMessage,
		                    string unexpectedlyTrueMessage, Func<bool> getState)
			: this(unexpectedlyFalseMessage, unexpectedlyTrueMessage, getState, null)
		{
		}

		public BooleanState(string unexpectedlyFalseMessage, string unexpectedlyTrueMessage, Func<bool> getState, Action<bool> setState)
		{
			_unexpectedlyFalseMessage = unexpectedlyFalseMessage;
			_unexpectedlyTrueMessage = unexpectedlyTrueMessage;
			_getState = getState;
			_setState = setState;
		}

		public bool IsTrue
		{
			get { return _getState(); }
		}

		public void ShouldBeFalse()
		{
			IsTrue.ShouldBeFalse(_unexpectedlyTrueMessage);
		}

		public void ShouldBeFalse(string errorMessage)
		{
			IsTrue.ShouldBeFalse(errorMessage);
		}

		public void ShouldBeTrue()
		{
			IsTrue.ShouldBeTrue(_unexpectedlyFalseMessage);
		}

		public void ShouldBeTrue(string errorMessage)
		{
			IsTrue.ShouldBeTrue(errorMessage);
		}

		[Obsolete("use SetValueTo(bool)")]
		public WaitWrapper SetValue(bool state)
		{
			return SetValueTo(state);
		}
		public WaitWrapper SetValueTo(bool state)
		{
			_setState(state);
			return new WaitWrapper();
		}
	}
}