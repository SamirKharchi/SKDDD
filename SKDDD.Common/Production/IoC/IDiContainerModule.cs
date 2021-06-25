namespace SKDDD.Common.Production.IoC
{
    public interface IDiContainerModule
    {
        /// <summary>
        /// This loads modules of registrations and is used for IoC frameworks that
        /// can create their container directly (e.g. castle windsor, ninject..)
        /// </summary>
        /// <param name="container"></param>
        void LoadModules(IDiContainer container);

        /// <summary>
        /// Registers a module of type T and adds it to the list of types to resolve.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="module"></param>
        void RegisterModule<T>(T module);
    }
}