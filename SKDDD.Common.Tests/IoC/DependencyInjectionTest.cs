
using System;
using SKDDD.Common.Production.IoC;
using SKDDD.Common.Production.IoC.Autofac;
using SKDDD.Common.Production.IoC.CastleWindsor;
using SKDDD.Common.Production.IoC.Ninject;
using SKUT.Xunit;
using Xunit;

namespace SKDDD.Common.Tests.IoC
{
    [TestCaseOrderer(PriorityOrderer.Location, PriorityOrderer.Assembly)]
    public class DependencyInjectionTest : IClassFixture<DependencyInjectionFixture>
    {
        private readonly DependencyInjectionFixture mFixture;

        public DependencyInjectionTest(DependencyInjectionFixture fixture)
        {
            mFixture = fixture;
        }

        private void GenericDependencyInjectionSetup<TModule>(DiFramework type) where TModule : new()
        {
            var di = mFixture.GetInjector(type);

            Assert.NotNull(di);
            Assert.True((bool) di.RegisterModule(new TModule()));
            di.FinishRegistrations();
        }

        private IDiContainerModule CreateModule(DiFramework type)
        {
            switch (type)
            {
                case DiFramework.CastleWindsor:
                    return new CastleWindsorContainerModule();
                case DiFramework.Ninject:
                    return new NinjectContainerModule();
                case DiFramework.Autofac:
                    return new AutofacContainerModule();
                default: return null;
            }
        }

        private IDiContainer CreateContainer(DiFramework type)
        {
            switch (type)
            {
                case DiFramework.CastleWindsor:
                    return new CastleWindsorContainer();
                case DiFramework.Ninject:
                    return new NinjectContainer();
                case DiFramework.Autofac:
                    return new AutofacContainer();
                default: return null;
            }
        }

        [Fact, TestPriority(0)]
        private void TestSetup()
        {
            GenericDependencyInjectionSetup<CastleWindsorModuleStub>(DiFramework.CastleWindsor);
            GenericDependencyInjectionSetup<NinjectModuleStub>(DiFramework.Ninject);
            GenericDependencyInjectionSetup<AutofacModuleStub>(DiFramework.Autofac);
        }

        [Theory, TestPriority(1)]
        [InlineData(DiFramework.CastleWindsor)]
        [InlineData(DiFramework.Ninject)]
        [InlineData(DiFramework.Autofac)]
        private void TestDependencyInjectionResolve(DiFramework type)
        {
            var di = mFixture.GetInjector(type);

            Assert.NotNull(di.Container().Resolve<ITestClass>());
        }

        [Theory, TestPriority(2)]
        [InlineData(DiFramework.CastleWindsor)]
        [InlineData(DiFramework.Ninject)]
        [InlineData(DiFramework.Autofac)]
        private void TestDependencyInjectionResolveAllGeneric(DiFramework type)
        {
            var di = mFixture.GetInjector(type);

            Assert.NotEmpty(di.Container().ResolveAll<ITestClass>());
        }

        [Theory, TestPriority(3)]
        [InlineData(DiFramework.CastleWindsor)]
        [InlineData(DiFramework.Ninject)]
        [InlineData(DiFramework.Autofac)]
        private void TestDependencyInjectionResolveAll(DiFramework type)
        {
            var di = mFixture.GetInjector(type);

            Assert.NotEmpty(di.Container().ResolveAll(typeof(ITestClass)));
        }

        [Theory, TestPriority(4)]
        [InlineData(DiFramework.CastleWindsor)]
        [InlineData(DiFramework.Ninject)]
        [InlineData(DiFramework.Autofac)]
        private void TestDependencyInjectionResolveType(DiFramework type)
        {
            var di = mFixture.GetInjector(type);

            Assert.NotNull(di.Container().Resolve(typeof(ITestClass)));
        }

        [Theory, TestPriority(5)]
        [InlineData(DiFramework.CastleWindsor)]
        [InlineData(DiFramework.Ninject)]
        [InlineData(DiFramework.Autofac)]
        private void TestDependencyInjectionResolveTry(DiFramework type)
        {
            var di = mFixture.GetInjector(type);

            Assert.NotNull(di.Container().TryResolve(typeof(ITestClass)));
            Assert.Null(di.Container().TryResolve(typeof(IAsyncLifetime)));
        }

        [Theory, TestPriority(6)]
        [InlineData(DiFramework.CastleWindsor)]
        [InlineData(DiFramework.Ninject)]
        [InlineData(DiFramework.Autofac)]
        private void TestDependencyInjectionResolveByName(DiFramework type)
        {
            var di = mFixture.GetInjector(type);

            Assert.NotNull(di.Container().Resolve<ITestClass>("testName"));
        }

        [Theory, TestPriority(7)]
        [InlineData(DiFramework.CastleWindsor)]
        [InlineData(DiFramework.Ninject)]
        [InlineData(DiFramework.Autofac)]
        private void TestDependencyInjectionAllocation(DiFramework type)
        {
            Assert.NotNull(new DependencyInjection(type));
        }

        [Fact, TestPriority(8)]
        private void TestDependencyInjectionAllocationInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DependencyInjection(DiFramework.Count));
        }

        [Theory, TestPriority(9)]
        [InlineData(DiFramework.CastleWindsor, DiFramework.Ninject)]
        [InlineData(DiFramework.Ninject, DiFramework.Autofac)]
        [InlineData(DiFramework.Autofac, DiFramework.CastleWindsor)]
        private void TestDependencyInjectionRegisterWrongModule(DiFramework type, DiFramework wrongType)
        {
            var di = mFixture.GetInjector(type);
            Assert.False((bool) di.RegisterModule(CreateModule(wrongType)));
        }

        [Theory, TestPriority(10)]
        [InlineData(DiFramework.CastleWindsor)]
        [InlineData(DiFramework.Ninject)]
        [InlineData(DiFramework.Autofac)]
        private void TestDependencyInjectionFinishRegistrations(DiFramework type)
        {
            var di = new DependencyInjection(type);
            di.FinishRegistrations();
        }

        [Theory, TestPriority(11)]
        [InlineData(DiFramework.CastleWindsor)]
        [InlineData(DiFramework.Ninject)]
        [InlineData(DiFramework.Autofac)]
        private void TestDependencyInjectionDispose(DiFramework type)
        {
            var container = CreateContainer(type) as IDisposable;
            Assert.NotNull(container);
            container.Dispose();
        }

        [Theory, TestPriority(12)]
        [InlineData(DiFramework.CastleWindsor, DiFramework.Ninject)]
        [InlineData(DiFramework.Ninject, DiFramework.Autofac)]
        [InlineData(DiFramework.Autofac, DiFramework.CastleWindsor)]
        private void TestDependencyInjectionLoadModules(DiFramework type, DiFramework wrongType)
        {
            var module = CreateModule(type);

            Assert.NotNull(module);
            Assert.Throws<ArgumentNullException>(()=>module.LoadModules(CreateContainer(wrongType)));

            module.LoadModules(CreateContainer(type));
        }
    }
}