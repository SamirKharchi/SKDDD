using System.Threading.Tasks;
using SKDDD.Common.Production.Output;

namespace SKDDD.Common.Production.Cqrs.Commands
{
    /// <inheritdoc />
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandHandlerFactory mFactory;

        public CommandDispatcher(ICommandHandlerFactory factory)
        {
            mFactory = factory;
        }

        public Result<TResult> Dispatch<TParameter, TResult>(TParameter command) where TParameter : ICommand
        {
            var handler       = mFactory.Create<TParameter, TResult>(command);
            var commandResult = handler.Handle(command);

            mFactory.Destroy(handler);
            return commandResult;
        }

        public async Task<Result<TResult>> DispatchAsync<TParameter, TResult>(TParameter command)
            where TParameter : ICommand
        {
            var handler       = mFactory.Create<TParameter, TResult>(command);
            var commandResult = await handler.HandleAsync(command);

            mFactory.Destroy(handler);
            return commandResult;
        }
    }
}