using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Modules;

namespace SKDDD.Common.Production.IoC.Ninject
{
    public sealed class NinjectContainerModule : IDiContainerModule
    {
        private readonly List<NinjectModule> mRegistrations = new List<NinjectModule>();

        public void LoadModules(IDiContainer container)
        {
            if (!(container is NinjectContainer ninjectContainer))
            {
                throw new ArgumentNullException();
            }

            foreach (var registration in mRegistrations)
            {
                ninjectContainer.InnerContainer.Load(registration);
            }
        }

        public void RegisterModule<T>(T module)
        {
            if (!(module is NinjectModule ninjectModule))
            {
                throw new ArgumentNullException();
            }
            mRegistrations.Add(ninjectModule);
        }
    }
}