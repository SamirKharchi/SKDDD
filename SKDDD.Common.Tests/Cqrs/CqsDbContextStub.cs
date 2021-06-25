
using System.Collections.Generic;

namespace SKDDD.Common.Tests.Cqrs
{
    public class CqsDbContextStub
    {
        public CqsDbContextStub()
        {
            DataList = new List<string> {"hello", "world"};
        }
        public List<string> DataList { get; set; }
    }
}