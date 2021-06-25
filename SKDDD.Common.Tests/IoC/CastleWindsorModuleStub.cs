
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace SKDDD.Common.Tests.IoC
{
    public class CastleWindsorModuleStub : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ITestClass>().ImplementedBy<TestClass>().Named("testName"));

            var assembly = Assembly.GetExecutingAssembly();
            var desc = Classes.FromAssembly(assembly)
                              .BasedOn<IAnotherTestClass>()
                              .WithServiceBase();
            container.Register(desc);
        }
    }
}