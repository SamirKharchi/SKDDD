
using System;
using System.Threading.Tasks;
using SKDDD.Common.Production.Cqrs.Commands;
using SKDDD.Common.Production.Output;

namespace SKDDD.Common.Tests.Cqrs.Commands
{
    public class CommandHandlerExceptionStub : CommandHandler<CommandAddStringStub, bool>
    {
        private readonly CqsDbContextStub mDbContext;

        public CommandHandlerExceptionStub(CqsDbContextStub context)
        {
            mDbContext = context;
        }

        protected override Result<bool> DoHandle(CommandAddStringStub commandAddString)
        {
            throw new ArgumentException();
        }

        protected override Task<Result<bool>> DoHandleAsync(CommandAddStringStub commandAddString)
        {
            throw new ArgumentException();
        }
    }
}