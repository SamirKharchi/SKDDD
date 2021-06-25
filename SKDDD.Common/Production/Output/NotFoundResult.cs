using System.Collections.Generic;

namespace SKDDD.Common.Production.Output
{
    /// <summary>
    /// Not found result.
    /// </summary>
    public class NotFoundResult<T> : Result<T>
    {
        private readonly string mError;

        public NotFoundResult(string error)
        {
            mError = error;
        }

        public override ResultType ResultType => ResultType.NotFound;

        public override List<string> Errors =>
            new List<string> {mError ?? "The entity you're looking for cannot be found"};

        public override T Data => default;
    }
}