
using System;
using SKDDD.Common.Production.IoC;

namespace SKDDD.Common.Tests.IoC
{
    public class DependencyInjectionFixture
    {
        private readonly DependencyInjection mWindsorInjection;
        private readonly DependencyInjection mNinjectInjection;
        private readonly DependencyInjection mAutofacInjection;

        public DependencyInjectionFixture()
        {
            mWindsorInjection = new DependencyInjection(DiFramework.CastleWindsor);
            mNinjectInjection = new DependencyInjection(DiFramework.Ninject);
            mAutofacInjection = new DependencyInjection(DiFramework.Autofac);
        }

        public DependencyInjection GetInjector(DiFramework type)
        {
            switch (type)
            {
                case DiFramework.CastleWindsor:
                    return mWindsorInjection;
                case DiFramework.Ninject:
                    return mNinjectInjection;
                case DiFramework.Autofac:
                    return mAutofacInjection;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}