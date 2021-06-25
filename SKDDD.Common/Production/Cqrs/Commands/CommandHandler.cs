using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SKDDD.Common.Production.Output;

namespace SKDDD.Common.Production.Cqrs.Commands
{
    public abstract class CommandHandler<TParameter, TResult> : ICommandHandler<TParameter, TResult>
        where TParameter : ICommand
    {
        // We could inject validators, assemblers, authorization handlers here
        protected CommandHandler() { }

        public Result<TResult> Handle(TParameter command)
        {
            Result<TResult> response;

            try
            {
                // We could do authorization, validation, dto assembly and alike here

                response = DoHandle(command);
            }
            catch (Exception)
            {
                throw;
            }
            finally { }

            return response;
        }

        public async Task<Result<TResult>> HandleAsync(TParameter command)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Task<Result<TResult>> response;

            try
            {
                // We could do authorization, validation, dto assembly and alike here

                response = DoHandleAsync(command);
            }
            catch (Exception)
            {
                throw;
            }
            finally { }

            return await response;
        }

        protected abstract Result<TResult> DoHandle(TParameter command);

        protected abstract Task<Result<TResult>> DoHandleAsync(TParameter command);
    }
}