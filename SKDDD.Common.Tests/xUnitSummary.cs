
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoFixture;
using AutoFixture.Xunit2;
using SKUT.AutoFixture;
using SKUT.Xunit;
using Xunit;

namespace SKDDD.Common.Tests
{
    #region HelperClasses

    public interface ITestInterface
    {
        int             EventVersion { get; set; }
        public DateTime OccurredOn   { get; set; }
    }

    public class TestStub : ITestInterface
    {
        public TestStub(string testProperty)
        {
            TestProperty = testProperty;
        }

        public string   TestProperty { get; set; }
        public int      EventVersion { get; set; } = 1;
        public DateTime OccurredOn   { get; set; } = DateTime.Now;

        #region OVERRIDDEN

        protected bool Equals(TestStub other)
        {
            return TestProperty == other.TestProperty && EventVersion == other.EventVersion &&
                   OccurredOn.Equals(other.OccurredOn);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((TestStub) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TestProperty, EventVersion, OccurredOn);
        }

        #endregion
    }

    #endregion

    public class ListOfExplicitDataClass : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator() => Data.GetEnumerator();

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] {true, false},
                new object[] {false, true}
            };

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // Input data from in-class member
        [Theory]
        [MemberData(nameof(Data))]
        public void Test(bool valueA, bool valueB)
        {
            Assert.NotEqual(valueA, valueB);
        }
    }

    public class XUnitTests
    {
        // Most simple test
        [Fact]
        private void TestNoParameters() { }

        // Input explicit parameter values inline
        [Theory]
        [InlineData(true, false)] // Arguments must match the amount of test method parameters
        [InlineData(false, true)] // A 2nd test run with different data
        public void TestInlineData(bool valueA, bool valueB)
        {
            Assert.NotEqual(valueA, valueB);
        }

        // Input data from data defined in another class
        [Theory]
        [ClassData(typeof(ListOfExplicitDataClass))]
        [MemberData(nameof(ListOfExplicitDataClass.Data), MemberType = typeof(ListOfExplicitDataClass))]
        public void TestInputData(bool valueA, bool valueB)
        {
            Assert.NotEqual(valueA, valueB);
        }

        // Runs with auto-generated data while the int parameter is constrained to be in a certain range
        [Theory]
        [AutoData]
        private void TestAutoData([Range(0, 10)] int value)
        {
            Assert.InRange(value, 0, 10);
        }

        // Runs with explicit data for the first parameter and
        // auto-generated and constrained data for the second parameter
        [Theory]
        [InlineAutoData(4)]
        private void TestInlineAutoData(int valueA, [Range(0, 10)] int valueB)
        {
            Assert.Equal(4, valueA);
            Assert.InRange(valueB, 0, 10);
        }

        // Runs in both cases with auto-generated data and
        // injects the fixture provided by the AutoData/InlineAutoData attribute instances
        [Theory]
        [AutoData]
        [InlineAutoData]
        private void TestInjectedFixtureAndRegister(IFixture fixture)
        {
            // Register how a certain type is created
            // Note that inside the lambda I use the fixture to also auto-generate the constructor argument data on-the-fly
            fixture.Register(() => new TestStub(fixture.Create<string>()));

            // This will create an array of 10 TestStub instances. Each one with a random testProperty value
            // NOTE: all other properties are default initialised
            var many = fixture.CreateMany<TestStub>(10);

            var lastTestProperty = "";
            foreach (var testStub in many)
            {
                Assert.Equal(1, testStub.EventVersion);
                Assert.NotEqual(lastTestProperty, testStub.TestProperty);

                lastTestProperty = testStub.TestProperty;
            }
        }

        private static void TestFrozenReference(IFixture fixture, object original)
        {
            var single  = fixture.Create<TestStub>();
            var many    = fixture.CreateMany<TestStub>(10);
            var another = fixture.Create<TestStub>();

            Assert.True(object.ReferenceEquals(original, single));
            Assert.True(object.ReferenceEquals(original, another));
            foreach (var testStub in many)
            {
                Assert.True(object.ReferenceEquals(original, testStub));
            }
        }

        // Freezes an instance so it is not re-allocated again in subsequent calls
        [Theory]
        [AutoData]
        private void TestFreeze(IFixture fixture)
        {
            // Freezing makes all subsequent creation calls of TestStub produce the same instance
            var original = fixture.Freeze<TestStub>();

            XUnitTests.TestFrozenReference(fixture, original);
        }

        // Freezes an instance so it is not re-allocated again in subsequent calls
        [Theory]
        [AutoData]
        private void TestFrozen([Frozen] TestStub original, IFixture fixture)
        {
            XUnitTests.TestFrozenReference(fixture, original);
        }

        // A trait allows to group test methods into categories (visible in the unit test explorer)
        [Fact, Trait("Category", "NSubstitute")]
        private void TestTrait() { }

        // If there are interfaces or abstract classes in the parameter list [AutoData] or [InlineAutoData] does not work anymore
        // We need to use [AutoSubstituteData] or [InlineAutoSubstituteData] to auto-mock via AutoFixture + NSubstitute
        [Theory]
        [AutoSubstituteData]
        private void TestInterfaceOrAbstractClass(IFixture fixture, ITestInterface domainEvent)
        {
            Assert.NotNull(fixture);
            Assert.NotNull(domainEvent);
        }

        // Customize using a custom specimen
        [Theory]
        [InlineAutoSubstituteData(5)] // Provide the mocking value explicitly, the other parameters auto-generated
        private void TestDataCustomizationSpecimen(int valueForMocking,
                                                   ITestInterface[] autoMocked,
                                                   IFixture fixture)
        {
            // Here we still have all values randomly auto-mocked
            foreach (var domainEvent in autoMocked)
            {
                Assert.NotEqual(valueForMocking, domainEvent.EventVersion);
            }

            //--------------------------------------------------------------------
            // Now customize via our specimenBuilder, so that
            // all following int values will be valueForMocking (i.e. 5)
            //
            // BUT only for those that are named "EventVersion"
            //--------------------------------------------------------------------
            fixture.Customizations.Add(new GenericValueSpecimen<int>(5, nameof(TestStub.EventVersion)));

            // ints created directly won't be affected as they are anonymous and have no property name
            var many = fixture.CreateMany<int>();
            foreach (var val in many)
            {
                Assert.NotEqual(valueForMocking, val);
            }

            // The EventVersion int property of TestStub will though
            var manyMore = fixture.CreateMany<TestStub>();
            foreach (var domainEvent in manyMore)
            {
                Assert.Equal(valueForMocking, domainEvent.EventVersion);
            }
        }

        // Customize using Inject
        [Theory]
        [InlineAutoSubstituteData(5)] // Provide the mocking value explicitly, the other parameters auto-generated
        private void TestDataCustomizationInject(int valueForMocking, IFixture fixture)
        {
            //--------------------------------------------------------------------
            // Customize via Inject, so that
            // all following int values will be valueForMocking.
            //
            // This is equivalent to: fixture.Customizations.Add(new GenericValueSpecimen<int>(5));
            //--------------------------------------------------------------------
            fixture.Inject(valueForMocking);

            var single    = fixture.Create<TestStub>();
            var singleInt = fixture.Create<int>();

            Assert.Equal(valueForMocking, single.EventVersion);
            Assert.Equal(valueForMocking, singleInt);
        }

        // Customize auto-mocked instances using a test data builder
        [Theory]
        [AutoSubstituteData]
        private void TestPostDataBuilder(IFixture fixture)
        {
            // Let fixture create a builder for customized TestStub instance customization
            // Note: changes are only applied AFTER construction of the object, so constructor parameters are auto-generated!
            var postComposedBuilder = fixture.Build<TestStub>()
                                             .With((x) => x.EventVersion, 5) // Explicitly changed
                                             .Without(x => x.OccurredOn); // default initialised (i.e. no auto-generated value)

            var postComposedInstance = postComposedBuilder.Create<TestStub>();
            var fullyMockedInstance  = fixture.Create<TestStub>();

            // explicitly changed
            Assert.Equal(5, postComposedInstance.EventVersion);
            // default initialized (i.e. no auto-generated value)
            Assert.NotEqual(fullyMockedInstance.OccurredOn, postComposedInstance.OccurredOn);
            // TestProperty was filled by a constructor parameter. So both will have an auto-generated value
            Assert.NotEqual(fullyMockedInstance.TestProperty, postComposedInstance.TestProperty);
        }
    }
}