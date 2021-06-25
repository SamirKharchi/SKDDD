using System.Threading.Tasks;
using SKDDD.Common.Production.Output;

namespace SKDDD.Common.Production.Cqrs.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IQueryHandlerFactory mFactory;

        public QueryDispatcher(IQueryHandlerFactory factory)
        {
            mFactory = factory;
        }

        public Result<TResult> Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
        {
            var handler     = mFactory.Create<TParameter, TResult>(query);
            var queryResult = handler.Retrieve(query);
            mFactory.Destroy(handler);
            return queryResult;
        }

        public async Task<Result<TResult>> DispatchAsync<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
        {
            var handler     = mFactory.Create<TParameter, TResult>(query);
            var queryResult = await handler.RetrieveAsync(query);

            mFactory.Destroy(handler);
            return queryResult;
        }
    }
}