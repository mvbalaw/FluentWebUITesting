using WatiN.Core;

namespace FluentWebUITesting.Controls
{
	public class LinkWrapper : ControlWrapperBase
	{
		private readonly Link _link;

		public LinkWrapper(Link link, string howFound)
			: base(howFound)
		{
			_link = link;
		}

		protected override Element Element
		{
			get { return _link; }
		}

		public WaitWrapper Click()
		{
			Exists().ShouldBeTrue();
			Enabled().ShouldBeTrue();
			_link.Click();
			return new WaitWrapper();
		}
	}
}