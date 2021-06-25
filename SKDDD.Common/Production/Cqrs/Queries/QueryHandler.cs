using System;
using System.Threading.Tasks;
using SKDDD.Common.Production.Output;

namespace SKDDD.Common.Production.Cqrs.Queries
{
    public abstract class QueryHandler<TParameter, TResult> : IQueryHandler<TParameter, TResult>
        where TParameter : IQuery, new()
    {
        public Result<TResult> Retrieve(TParameter query)
        {
            Result<TResult> queryResult;

            try
            {
                // We could do authorization, validation, dto assembly and alike here

                queryResult = DoRetrieve(query);
            }
            catch (Exception)
            {
                // Log and Error logic
                throw;
            }
            finally { }

            return queryResult;
        }

        public async Task<Result<TResult>> RetrieveAsync(TParameter query)
        {
            Task<Result<TResult>> queryResult;

            try
            {
                // We could do authorization, validation, dto assembly and alike here

                queryResult = DoRetrieveAsync(query);
            }
            catch (Exception)
            {
                // Log and Error logic
                throw;
            }
            finally { }

            return await queryResult;
        }

        /// <summary>
        /// The actual Retrieve method that will be implemented in the derived class
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected abstract Result<TResult> DoRetrieve(TParameter query);

        /// <summary>
        /// The actual async Retrieve method that will be implemented in the derived class
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected abstract Task<Result<TResult>> DoRetrieveAsync(TParameter query);
    }
}