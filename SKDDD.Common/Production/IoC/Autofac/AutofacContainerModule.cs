using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace SKDDD.Common.Production.IoC.Autofac
{
    public sealed class AutofacContainerModule : IDiContainerModule
    {
        private readonly List<IModule> mRegistrations = new List<IModule>();

        public void LoadModules(IDiContainer container)
        {
            if (!(container is AutofacContainer autofacContainer))
            {
                throw new ArgumentNullException();
            }

            var autofacBuilder = new ContainerBuilder();
            foreach (var registration in mRegistrations)
            {
                autofacBuilder.RegisterModule(registration);
            }
            autofacContainer.InnerContainer = autofacBuilder.Build();
        }

        public void RegisterModule<T>(T module)
        {
            if (!(module is IModule autofacModule))
            {
                throw new ArgumentNullException();
            }
            mRegistrations.Add(autofacModule);
        }
    }
}