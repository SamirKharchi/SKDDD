using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;

namespace SKDDD.Common.Production.IoC.CastleWindsor
{
    public sealed class CastleWindsorContainerModule : IDiContainerModule
    {
        private readonly List<IWindsorInstaller> mRegistrations = new List<IWindsorInstaller>();

        public void LoadModules(IDiContainer container)
        {
            if (!(container is CastleWindsorContainer windsorContainer))
            {
                throw new ArgumentNullException();
            }

            foreach (var registration in mRegistrations)
            {
                windsorContainer.InnerContainer.Install(registration);
            }
        }

        public void RegisterModule<T>(T module)
        {
            if (!(module is IWindsorInstaller windsorModule))
            {
                throw new ArgumentNullException();
            }
            mRegistrations.Add(windsorModule);
        }
    }
}