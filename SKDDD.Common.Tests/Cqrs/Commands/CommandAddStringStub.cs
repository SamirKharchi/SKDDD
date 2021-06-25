using SKDDD.Common.Production.Cqrs.Commands;

namespace SKDDD.Common.Tests.Cqrs.Commands
{
    public class CommandAddStringStub : Command
    {
        public string StringToAdd { get; set; }
    }
}