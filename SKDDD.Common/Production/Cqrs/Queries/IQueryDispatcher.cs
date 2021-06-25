using System.Threading.Tasks;
using SKDDD.Common.Production.Output;

namespace SKDDD.Common.Production.Cqrs.Queries
{
    /// <summary>
    /// Dispatches a query and invokes the corresponding handler
    /// </summary>
    public interface IQueryDispatcher
    {
        /// <summary>
        /// Dispatches a query and retrieves a query result
        /// </summary>
        /// <typeparam name="TParameter">Request to execute type</typeparam>
        /// <typeparam name="TResult">Request Result to get back type</typeparam>
        /// <param name="query">Request to execute</param>
        /// <returns>Request Result to get back</returns>
        Result<TResult> Dispatch<TParameter, TResult>(TParameter query) where TParameter : IQuery;

        /// <summary>
        /// Dispatches a query and retrieves an async query result
        /// </summary>
        /// <typeparam name="TParameter">Request to execute type</typeparam>
        /// <typeparam name="TResult">Request Result to get back type</typeparam>
        /// <param name="query">Request to execute</param>
        /// <returns>Request Result to get back</returns>
        Task<Result<TResult>> DispatchAsync<TParameter, TResult>(TParameter query) where TParameter : IQuery;
    }
}