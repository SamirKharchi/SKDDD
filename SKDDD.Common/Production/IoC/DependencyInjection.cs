using System;
using SKDDD.Common.Production.IoC.Autofac;
using SKDDD.Common.Production.IoC.CastleWindsor;
using SKDDD.Common.Production.IoC.Ninject;

namespace SKDDD.Common.Production.IoC
{
    public enum DiFramework
    {
        CastleWindsor,
        Ninject,
        Autofac,

        Count
    }

    public class DependencyInjection
    {
        private readonly IDiContainer       mContainer;
        private readonly IDiContainerModule mContainerModule;

        private static DependencyInjection mInstance;

        public static DependencyInjection Instance(DiFramework type) =>
            mInstance ?? (mInstance = new DependencyInjection(type));

        public DependencyInjection(DiFramework framework)
        {
            switch (framework)
            {
                case DiFramework.CastleWindsor:
                    mContainer       = new CastleWindsorContainer();
                    mContainerModule = new CastleWindsorContainerModule();
                    break;
                case DiFramework.Autofac:
                    mContainer       = new AutofacContainer();
                    mContainerModule = new AutofacContainerModule();
                    break;
                case DiFramework.Ninject:
                    mContainer       = new NinjectContainer();
                    mContainerModule = new NinjectContainerModule();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(framework), framework, null);
            }
        }

        public bool RegisterModule<T>(T module)
        {
            try
            {
                mContainerModule.RegisterModule(module);
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            return true;
        }

        public void FinishRegistrations()
        {
            mContainer.RegisterModules(mContainerModule);
        }

        public IDiContainer Container() => mContainer;
    }
}