
using System.Collections.Generic;
using System.Threading.Tasks;
using SKDDD.Common.Production.Cqrs.Queries;
using SKDDD.Common.Production.Output;

namespace SKDDD.Common.Tests.Cqrs.Queries
{
    public class QueryHandlerGetStringsStub : QueryHandler<QueryGetStringsStub, List<string>>
    {
        private readonly CqsDbContextStub mDbContext;

        public QueryHandlerGetStringsStub(CqsDbContextStub dbContext)
        {
            mDbContext = dbContext;
        }

        protected override Result<List<string>> DoRetrieve(QueryGetStringsStub queryGetStrings)
        {
            return queryGetStrings.OnlyFirst
                ? new SuccessResult<List<string>>(new List<string>() {mDbContext.DataList[0]})
                : new SuccessResult<List<string>>(mDbContext.DataList);
        }

        protected override async Task<Result<List<string>>> DoRetrieveAsync(QueryGetStringsStub queryGetStrings)
        {
            if (queryGetStrings.OnlyFirst)
            {
                return new SuccessResult<List<string>>(new List<string>() {mDbContext.DataList[0]});
            }

            await Task.Delay(1000);

            return new SuccessResult<List<string>>(mDbContext.DataList);
        }
    }
}