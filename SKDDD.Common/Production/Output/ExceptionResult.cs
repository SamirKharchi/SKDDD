using System;
using System.Collections.Generic;

namespace SKDDD.Common.Production.Output
{
    /// <summary>
    /// Exception result.
    /// </summary>
    public class ExceptionResult<T> : Result<T>
    {
        private readonly string mError;

        public ExceptionResult(Exception error)
        {
            mError = error.Message;
        }

        public override ResultType ResultType => ResultType.Exception;

        public override List<string> Errors => new List<string> {mError ?? "There was an exception triggered."};

        public override T Data => default;
    }
}