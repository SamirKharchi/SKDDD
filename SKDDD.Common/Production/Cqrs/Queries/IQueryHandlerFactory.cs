namespace SKDDD.Common.Production.Cqrs.Queries
{
    /// <summary>
    /// This factory is used for CastleWindsor.
    /// The interface declaration is all needed to resolve the correct QueryHandler by our IoC container
    /// when trying so in the <see cref="QueryDispatcher"/>.
    /// <br/><br/>
    /// Add it via:<br/>
    /// <code>container.AddFacility&lt;TypedFactoryFacility&gt;();<br/>
    /// container.Register(Component.For&lt;IQueryHandlerFactory&gt;().AsFactory());
    /// </code>
    /// </summary>
    public interface IQueryHandlerFactory
    {
        IQueryHandler<T, TResult> Create<T, TResult>(T command) where T : IQuery;
        void       Destroy(object handler);
    }
}