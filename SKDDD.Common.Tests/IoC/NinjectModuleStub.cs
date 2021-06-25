
using Ninject.Modules;

namespace SKDDD.Common.Tests.IoC
{
    internal class NinjectModuleStub : NinjectModule
    {
        public override void Load()
        {
            Bind<ITestClass>().To<TestClass>().Named("testName");
        }
    }
}