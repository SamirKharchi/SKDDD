
using System;
using System.Threading.Tasks;
using SKDDD.Common.Production.Cqrs.Queries;
using SKDDD.Common.Production.Output;

namespace SKDDD.Common.Tests.Cqrs.Queries
{
    public class QueryHandlerExceptionStub : QueryHandler<QueryGetStringsStub, bool>
    {
        private readonly CqsDbContextStub mDbContext;

        public QueryHandlerExceptionStub(CqsDbContextStub dbContext)
        {
            mDbContext = dbContext;
        }

        protected override Result<bool> DoRetrieve(QueryGetStringsStub queryGetStrings)
        {
            throw new ArgumentException();
        }

        protected override Task<Result<bool>> DoRetrieveAsync(QueryGetStringsStub queryGetStrings)
        {
            throw new ArgumentException();
        }
    }
}