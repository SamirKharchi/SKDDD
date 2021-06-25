using System.Threading.Tasks;
using SKDDD.Common.Production.Output;

namespace SKDDD.Common.Production.Cqrs.Commands
{
    /// <summary>
    /// Base interface for command handlers
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface ICommandHandler<in TParameter, TResult> where TParameter : ICommand
    {
        /// <summary>
        /// Executes a command handler
        /// </summary>
        /// <param name="command">The command to be used</param>
        Result<TResult> Handle(TParameter command);

        /// <summary>
        /// Executes an async command handler
        /// </summary>
        /// <param name="command">The command to be used</param>
        Task<Result<TResult>> HandleAsync(TParameter command);
    }
}