using FluentAssert;

namespace FluentWebUITesting.Accessors
{
    public class Result
    {
        public bool Passed { get; set; }
        public string UnexpectedlyFalseMessage { get; set; }
        public string UnexpectedlyTrueMessage { get; set; }

        public void ShouldBeFalse()
        {
            Passed.ShouldBeFalse(UnexpectedlyTrueMessage);
        }

        public void ShouldBeFalse(string errorMessage)
        {
            Passed.ShouldBeFalse(errorMessage);
        }

        public void ShouldBeTrue()
        {
            Passed.ShouldBeTrue(UnexpectedlyFalseMessage);
        }

        public void ShouldBeTrue(string errorMessage)
        {
            Passed.ShouldBeTrue(errorMessage);
        }
    }
}