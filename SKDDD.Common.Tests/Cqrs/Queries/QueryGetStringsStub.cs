
using SKDDD.Common.Production.Cqrs.Queries;

namespace SKDDD.Common.Tests.Cqrs.Queries
{
    public class QueryGetStringsStub : Query
    {
        public bool OnlyFirst { get; set; }
    }
}