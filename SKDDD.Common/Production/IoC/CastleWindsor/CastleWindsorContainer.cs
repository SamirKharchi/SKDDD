using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Windsor;

namespace SKDDD.Common.Production.IoC.CastleWindsor
{
    public class CastleWindsorContainer : IDiContainer, IDisposable
    {
        public CastleWindsorContainer()
        {
            InnerContainer = new WindsorContainer();
        }

        void IDiContainer.RegisterModules(IDiContainerModule module)
        {
            module.LoadModules(this);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return InnerContainer.ResolveAll<T>();
        }

        public IEnumerable ResolveAll(Type type)
        {
            return InnerContainer.ResolveAll(type);
        }

        public T Resolve<T>() => InnerContainer.Resolve<T>();

        public object Resolve(Type type) => InnerContainer.Resolve(type);

        public object TryResolve(Type type) =>
            InnerContainer.Kernel.HasComponent(type) ? InnerContainer.Resolve(type) : null;

        public T Resolve<T>(string key) => InnerContainer.Resolve<T>(key);

        public IWindsorContainer InnerContainer { get; }

        public void Dispose()
        {
            InnerContainer?.Dispose();
        }
    }
}