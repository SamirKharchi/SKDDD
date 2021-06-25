using System.Threading.Tasks;
using SKDDD.Common.Production.Output;

namespace SKDDD.Common.Production.Cqrs.Commands
{
    /// <summary>
    /// Passed around to all allow dispatching a command and to be mocked by unit tests
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatches a command to its handler
        /// </summary>
        /// <typeparam name="TParameter">Command Type</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command">The command to be passed to the handler</param>
        Result<TResult> Dispatch<TParameter, TResult>(TParameter command)
            where TParameter : ICommand;


        /// <summary>
        /// Dispatches an async command to its handler
        /// </summary>
        /// <typeparam name="TParameter">Command Type</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command">The command to be passed to the handler</param>
        Task<Result<TResult>> DispatchAsync<TParameter, TResult>(TParameter command)
            where TParameter : ICommand;
    }
}