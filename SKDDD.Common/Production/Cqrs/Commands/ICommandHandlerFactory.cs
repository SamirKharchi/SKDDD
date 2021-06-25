namespace SKDDD.Common.Production.Cqrs.Commands
{
    /// <summary>
    /// This factory is used for CastleWindsor.
    /// The interface declaration is all needed to resolve the correct CommandHandler by our IoC container
    /// when trying so in the <see cref="CommandDispatcher"/>.
    /// <br/><br/>
    /// Add it via:<br/>
    /// <code>container.AddFacility&lt;TypedFactoryFacility&gt;();<br/>
    /// container.Register(Component.For&lt;ICommandHandlerFactory&gt;().AsFactory());
    /// </code>
    /// </summary>
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T, TResult> Create<T, TResult>(T command) where T : ICommand;
        void       Destroy(object handler);
    }
}