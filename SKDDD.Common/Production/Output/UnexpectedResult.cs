using System.Collections.Generic;

namespace SKDDD.Common.Production.Output
{
    /// <summary>
    /// Unexpected result.
    /// </summary>
    public class UnexpectedResult<T> : Result<T>
    {
        private readonly string mError;

        public UnexpectedResult(string error)
        {
            mError = error;
        }

        public override ResultType ResultType => ResultType.Unexpected;

        public override List<string> Errors => new List<string> {mError ?? "There was an unexpected problem"};

        public override T Data => default;
    }
}