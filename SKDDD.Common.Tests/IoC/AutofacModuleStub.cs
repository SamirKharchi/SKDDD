
using Autofac;

namespace SKDDD.Common.Tests.IoC
{
    internal class AutofacModuleStub : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestClass>().As<ITestClass>().Named<ITestClass>("testName");
        }
    }
}