
using System.Threading.Tasks;
using SKDDD.Common.Production.Cqrs.Commands;
using SKDDD.Common.Production.Output;

namespace SKDDD.Common.Tests.Cqrs.Commands
{
    public class CommandHandlerAddStringStub : CommandHandler<CommandAddStringStub, string>
    {
        private readonly CqsDbContextStub mDbContext;

        public CommandHandlerAddStringStub(CqsDbContextStub context)
        {
            mDbContext = context;
        }

        protected override Result<string> DoHandle(CommandAddStringStub commandAddString)
        {
            mDbContext.DataList.Add(commandAddString.StringToAdd);

            return new SuccessResult<string>("Added a string");
        }

        private async Task DoSomethingAsync(string value)
        {
            mDbContext.DataList.Add(value);
            await Task.Delay(2000);
        }

        protected override async Task<Result<string>> DoHandleAsync(CommandAddStringStub commandAddString)
        {
            await DoSomethingAsync(commandAddString.StringToAdd).ConfigureAwait(false);

            return new SuccessResult<string>("Added a string async");
        }
    }
}