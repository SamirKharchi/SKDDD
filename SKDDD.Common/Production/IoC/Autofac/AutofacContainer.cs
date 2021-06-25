using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;

namespace SKDDD.Common.Production.IoC.Autofac
{
    public class AutofacContainer : IDiContainer, IDisposable
    {
        public AutofacContainer()
        {
            InnerContainer = new ContainerBuilder().Build();
        }

        void IDiContainer.RegisterModules(IDiContainerModule module)
        {
            module.LoadModules(this);
        }

        /// <inheritdoc />
        public IEnumerable<T> ResolveAll<T>()
        {
            using (var scope = InnerContainer.BeginLifetimeScope())
            {
                return scope.Resolve<IEnumerable<T>>();
            }
        }

        /// <inheritdoc />
        public IEnumerable ResolveAll(Type type)
        {
            using (var scope = InnerContainer.BeginLifetimeScope())
            {
                var genericType = typeof(IEnumerable<>).MakeGenericType(type);
                return (IEnumerable)scope.Resolve(genericType);
            }
        }

        /// <inheritdoc />
        public T Resolve<T>()
        {
            using (var scope = InnerContainer.BeginLifetimeScope())
            {
                return scope.Resolve<T>();
            }
        }

        /// <inheritdoc />
        public object Resolve(Type type)
        {
            using (var scope = InnerContainer.BeginLifetimeScope())
            {
                return scope.Resolve(type);
            }
        }

        /// <inheritdoc />
        public object TryResolve(Type type)
        {
            using (var scope = InnerContainer.BeginLifetimeScope())
            {
                return !scope.IsRegistered(type) ? null : scope.Resolve(type);
            }
        }

        /// <summary>Type must have been registered via builder.RegisterType&lt;Type&gt;().Named&lt;IType&gt;("key");</summary>
        public T Resolve<T>(string key)
        {
            using (var scope = InnerContainer.BeginLifetimeScope())
            {
                return scope.ResolveNamed<T>(key);
            }
        }

        public IContainer InnerContainer { get; internal set; }

        public void Dispose()
        {
            InnerContainer?.Dispose();
        }
    }
}