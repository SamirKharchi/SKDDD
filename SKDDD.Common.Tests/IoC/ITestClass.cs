
namespace SKDDD.Common.Tests.IoC
{
    public interface ITestClass
    {
        string someProperty { get; set; }
    }

    public class TestClass : ITestClass
    {
        public string someProperty { get; set; }
    }

    public interface IAnotherTestClass
    {
        int someProperty { get; set; }
    }

    public class AnotherTestClass : IAnotherTestClass
    {
        public int someProperty { get; set; }
    }

    public class YetAnotherTestClass : IAnotherTestClass
    {
        public int someProperty { get; set; }
    }
}