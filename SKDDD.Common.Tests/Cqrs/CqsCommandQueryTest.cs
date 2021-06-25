
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SKDDD.Common.Production.Cqrs.Commands;
using SKDDD.Common.Production.Cqrs.Queries;
using SKDDD.Common.Production.Output;
using SKDDD.Common.Tests.Cqrs.Commands;
using SKDDD.Common.Tests.Cqrs.Queries;
using SKUT.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace SKDDD.Common.Tests.Cqrs
{
    [TestCaseOrderer(PriorityOrderer.Location, PriorityOrderer.Assembly)]
    public class CqsCommandQueryTest : IClassFixture<CqsFixture>
    {
        private readonly ITestOutputHelper mTestOutputHelper;
        private readonly CqsFixture        mFixture;

        public CqsCommandQueryTest(CqsFixture fixture, ITestOutputHelper testOutputHelper)
        {
            mFixture          = fixture;
            mTestOutputHelper = testOutputHelper;
        }

        private void PrintList(List<string> list)
        {
            foreach (var value in list)
            {
                mTestOutputHelper.WriteLine(value);
            }
        }

        [Fact, TestPriority(0)]
        private void TestCqsDispatchQuerySync()
        {
            var container       = mFixture.Injector.Container();
            var queryDispatcher = container.Resolve<IQueryDispatcher>();

            var response = queryDispatcher.Dispatch<QueryGetStringsStub, List<string>>(new QueryGetStringsStub());

            Assert.Empty(response.Errors);
            Assert.NotNull(response.Data);
            Assert.Equal(ResultType.Ok, response.ResultType);
            Assert.True(Enumerable.Count<string>(response.Data) == 2);

            PrintList(response.Data);
        }

        [Fact, TestPriority(1)]
        private async void TestCqsDispatchQueryAsync()
        {
            var container       = mFixture.Injector.Container();
            var queryDispatcher = container.Resolve<IQueryDispatcher>();

            var response =
                await queryDispatcher.DispatchAsync<QueryGetStringsStub, List<string>>(new QueryGetStringsStub());

            Assert.Empty(response.Errors);
            Assert.NotNull(response.Data);
            Assert.Equal(ResultType.Ok, response.ResultType);
            Assert.True(Enumerable.Count<string>(response.Data) == 2);

            PrintList(response.Data);
        }

        [Fact, TestPriority(2)]
        private void TestCqsDispatchCommandSync()
        {
            var container         = mFixture.Injector.Container();
            var commandDispatcher = container.Resolve<ICommandDispatcher>();

            var result = commandDispatcher.Dispatch<CommandAddStringStub, string>(new CommandAddStringStub()
            {
                StringToAdd = "Another nice string"
            });

            Assert.Empty(result.Errors);
            Assert.NotNull(result.Data);
            Assert.Equal(ResultType.Ok, result.ResultType);
            Assert.Equal((string) "Added a string", (string) result.Data);
        }

        [Fact, TestPriority(3)]
        private async void TestCqsDispatchCommandAsync()
        {
            var container         = mFixture.Injector.Container();
            var commandDispatcher = container.Resolve<ICommandDispatcher>();

            var result = await commandDispatcher.DispatchAsync<CommandAddStringStub, string>(new CommandAddStringStub()
            {
                StringToAdd = "Latest String"
            });

            Assert.Empty(result.Errors);
            Assert.NotNull(result.Data);
            Assert.Equal(ResultType.Ok, result.ResultType);
            Assert.Equal((string) "Added a string async", (string) result.Data);
        }

        [Fact, TestPriority(4)]
        private void TestCqsDispatchCommandCheckResults()
        {
            var container       = mFixture.Injector.Container();
            var queryDispatcher = container.Resolve<IQueryDispatcher>();

            var response = queryDispatcher.Dispatch<QueryGetStringsStub, List<string>>(new QueryGetStringsStub());

            Assert.NotNull(response.Data);
            Assert.True(Enumerable.Count<string>(response.Data) == 4);
            Assert.Equal(ResultType.Ok, response.ResultType);
            Assert.Equal((string) "Another nice string", (string) response.Data[2]);
            Assert.Equal((string) "Latest String", (string) response.Data[3]);

            PrintList(response.Data);
        }

        [Fact, TestPriority(5)]
        private void TestCqsDispatchQuerySyncException()
        {
            var container       = mFixture.Injector.Container();
            var queryDispatcher = container.Resolve<IQueryDispatcher>();
            var query           = new QueryGetStringsStub();

            Assert.ThrowsAny<Exception>(() => queryDispatcher.Dispatch<QueryGetStringsStub, bool>(query));
        }

        [Fact, TestPriority(6)]
        private async Task TestCqsDispatchQueryAsyncException()
        {
            var container       = mFixture.Injector.Container();
            var queryDispatcher = container.Resolve<IQueryDispatcher>();
            var query           = new QueryGetStringsStub();

            await Assert.ThrowsAnyAsync<Exception>(() => queryDispatcher
                                                       .DispatchAsync<QueryGetStringsStub, bool>(query));
        }

        [Fact, TestPriority(7)]
        private void TestCqsDispatchCommandSyncException()
        {
            var container         = mFixture.Injector.Container();
            var commandDispatcher = container.Resolve<ICommandDispatcher>();
            var command           = new CommandAddStringStub();

            Assert.ThrowsAny<Exception>(() => commandDispatcher.Dispatch<CommandAddStringStub, bool>(command));
        }

        [Fact, TestPriority(8)]
        private async Task TestCqsDispatchCommandAsyncException()
        {
            var container         = mFixture.Injector.Container();
            var commandDispatcher = container.Resolve<ICommandDispatcher>();
            var command           = new CommandAddStringStub();

            await Assert.ThrowsAnyAsync<Exception>(() => commandDispatcher
                                                       .DispatchAsync<CommandAddStringStub, bool>(command));
        }
    }
}