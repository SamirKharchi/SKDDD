
using System.Reflection;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SKDDD.Common.Production.Cqrs.Commands;
using SKDDD.Common.Production.Cqrs.Queries;

namespace SKDDD.Common.Tests.Cqrs
{
    public class CqsWindsorTestModule : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Register factories required in order to inject this windsor container in resolved types
            container.AddFacility<TypedFactoryFacility>();
            container.Register(Component.For<IQueryHandlerFactory>().AsFactory());
            container.Register(Component.For<ICommandHandlerFactory>().AsFactory());

            // Register the QueryDispatcher and CommandDispatcher
            container.Register(Component.For<IQueryDispatcher>().ImplementedBy<QueryDispatcher>()
                                        .LifestyleTransient());
            container.Register(Component.For<ICommandDispatcher>().ImplementedBy<CommandDispatcher>()
                                        .LifestyleTransient());

            // Register our class which will be a singleton for this test to hold data
            container.Register(Component.For<CqsDbContextStub>());

            // Register all QueryHandlers and all CommandHandlers found in this assembly
            // It is important to have WithServiceBase() used, otherwise the factory resolver in the dispatchers won't work
            // Note: For .Net Core we cannot use Assembly.GetCallingAssembly(). See: https://github.com/castleproject/Windsor/issues/239
            container.Register(Classes.FromAssembly(Assembly.Load(typeof(CqsWindsorTestModule).Assembly.FullName))
                                      .BasedOn(typeof(IQueryHandler<,>))
                                      .Configure(c => c.Named(c.Implementation.Name))
                                      .LifestyleTransient()
                                      .WithServiceBase());
            container.Register(Classes.FromAssembly(Assembly.Load(typeof(CqsWindsorTestModule).Assembly.FullName))
                                      .BasedOn(typeof(ICommandHandler<,>))
                                      .Configure(c => c.Named(c.Implementation.Name))
                                      .LifestyleTransient()
                                      .WithServiceBase());
        }
    }
}