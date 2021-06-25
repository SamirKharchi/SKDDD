using System;
using System.Collections;
using System.Collections.Generic;
using Ninject;

namespace SKDDD.Common.Production.IoC.Ninject
{
    public class NinjectContainer : IDiContainer, IDisposable
    {
        public NinjectContainer()
        {
            InnerContainer = new StandardKernel();
        }

        void IDiContainer.RegisterModules(IDiContainerModule module)
        {
            module.LoadModules(this);
        }

        public IEnumerable<T> ResolveAll<T>() => InnerContainer.GetAll<T>();

        public IEnumerable ResolveAll(Type type)
        {
            var genericType = typeof(IEnumerable<>).MakeGenericType(type);
            return InnerContainer.GetAll(genericType);
        }

        public T Resolve<T>() => InnerContainer.Get<T>();

        public object Resolve(Type type) => InnerContainer.Get(type);

        public object TryResolve(Type type) => InnerContainer.CanResolve(type) ? Resolve(type) : null;

        public T Resolve<T>(string key) => InnerContainer.Get<T>(key);

        public KernelBase InnerContainer { get; }

        public void Dispose()
        {
            InnerContainer?.Dispose(true);
        }
    }
}