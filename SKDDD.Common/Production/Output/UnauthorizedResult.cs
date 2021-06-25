using System.Collections.Generic;

namespace SKDDD.Common.Production.Output
{
    /// <summary>
    /// Unauthorized result.
    /// </summary>
    public class UnauthorizedResult<T> : Result<T>
    {
        private readonly string mError;

        public UnauthorizedResult(string error)
        {
            mError = error;
        }

        public override ResultType ResultType => ResultType.Unauthorized;

        public override List<string> Errors => new List<string> {mError ?? "Authorization failed."};

        public override T Data => default;
    }
}