using System.Collections.Generic;

namespace SKDDD.Common.Production.Output
{
    /// <summary>
    /// Success result.
    /// </summary>
    public class SuccessResult<T> : Result<T>
    {
        public SuccessResult(T data)
        {
            Data = data;
        }
        public override ResultType ResultType => ResultType.Ok;

        public override List<string> Errors => new List<string>();

        public override T Data { get; }
    }
}
