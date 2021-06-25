using SKDDD.Common.Production.IoC;

namespace SKDDD.Common.Tests.Cqrs
{
    public class CqsFixture
    {
        internal DependencyInjection Injector { get; }

        public CqsFixture()
        {
            Injector = DependencyInjection.Instance(DiFramework.CastleWindsor);
            Injector.RegisterModule(new CqsWindsorTestModule());
            Injector.FinishRegistrations();
        }
    }
}