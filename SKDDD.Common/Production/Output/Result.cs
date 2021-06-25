using System.Collections.Generic;

namespace SKDDD.Common.Production.Output
{
    /// <summary>
    /// Result model to contain data, result type, and errors
    /// </summary>
    public abstract class Result<T> : IResult
    {
        public abstract ResultType   ResultType { get; }
        public abstract List<string> Errors     { get; }
        public abstract T            Data       { get; }
    }
}