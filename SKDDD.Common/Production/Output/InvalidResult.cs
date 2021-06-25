using System.Collections.Generic;

namespace SKDDD.Common.Production.Output
{
    /// <summary>
    /// Invalid result.
    /// </summary>
    public class InvalidResult<T> : Result<T>
    {
        private readonly string mError;

        public InvalidResult(string error)
        {
            mError = error;
        }

        public override ResultType ResultType => ResultType.Invalid;

        public override List<string> Errors => new List<string> {mError ?? "The input was invalid."};

        public override T Data => default;
    }
}