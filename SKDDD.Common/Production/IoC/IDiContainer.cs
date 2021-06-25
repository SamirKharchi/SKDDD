using System;
using System.Collections;
using System.Collections.Generic;

namespace SKDDD.Common.Production.IoC
{
    public interface IDiContainer
    {
        /// <summary>
        /// Calls the module registration and must be implemented explicitly!
        /// Because this is only for internal use by the <see cref="DependencyInjection"/> class.
        /// </summary>
        /// <param name="module"></param>
        void RegisterModules(IDiContainerModule module);

        /// <summary>
        /// Return the list of all instances that implements T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// Return the list of all instances that implements type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        IEnumerable ResolveAll(Type type);

        /// <summary>
        /// Resolve an instance of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();

        /// <summary>
        /// Resolve an instance of the specified type
        /// Throws an exception when the type is not found
        /// </summary>
        /// <param name="type">The type to resolve.</param>
        /// <returns>The instance</returns>
        object Resolve(Type type);

        /// <summary>
        /// Resolve an instance of the specified type
        /// </summary>
        /// <param name="type">The type to resolve.</param>
        /// <returns>The instance or null when not found</returns>
        object TryResolve(Type type);

        /// <summary>
        /// Resolve a named instance of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T Resolve<T>(string key);
    }
}