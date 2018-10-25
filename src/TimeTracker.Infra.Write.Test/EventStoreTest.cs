using System;
using System.Threading.Tasks;
using FluentAssertions;
using TimeTracker.Core.Exceptions.Technical;
using TimeTracker.Infra.Write.Ioc;
using TimeTracker.Infra.Write.Test.Sample;
using TimeTracker.Test.Common;
using Xunit;

namespace TimeTracker.Infra.Write.Test
{
    [Trait("Type", "Integration")]
    [Trait("Category", "Database")]
    [Collection("DropCreateDatabase Collection")]
    public class EventStoreTest : IDisposable
    {
        private readonly AutofacSandbox _autofacSandbox;
        private readonly IEventStore _sut;

        public EventStoreTest()
        {
            var configBuilder = new ConfigBuilder();

            _autofacSandbox = new AutofacSandbox(configBuilder.BuildModule(), new Module());

            _sut = _autofacSandbox.Resolve<IEventStore>();
        }

        [Fact]
        public async Task should_save_correctly_with_one_event()
        {
            // GIVEN
            var id = Guid.NewGuid();

            var aggregateRoot = SampleAggregateRoot.Create(id, "some text", -1);

            // WHEN
            await _sut.Save(aggregateRoot);

            // THEN
            var loadedAggregateRoot = await _sut.GetById<SampleAggregateRoot>(id);

            loadedAggregateRoot.Id.Should().Be(id);
            loadedAggregateRoot.Text.Should().Be("some text");
        }

        [Fact]
        public async Task should_save_correctly_with_multiple_events()
        {
            // GIVEN
            var id = Guid.NewGuid();

            var aggregateRoot = SampleAggregateRoot.Create(id, "some text", -1);

            aggregateRoot.ChangeText("text 1");
            aggregateRoot.ChangeText("text 2");
            aggregateRoot.ChangeText("text 3");
            aggregateRoot.ClearText();
            aggregateRoot.ChangeText("final text");

            // WHEN
            await _sut.Save(aggregateRoot);

            // THEN
            var loadedAggregateRoot = await _sut.GetById<SampleAggregateRoot>(id);

            loadedAggregateRoot.Id.Should().Be(id);
            loadedAggregateRoot.Text.Should().Be("final text");
        }

        [Fact]
        public async Task should_have_no_events_after_save()
        {
            // GIVEN
            var aggregateRoot = SampleAggregateRoot.Create(Guid.NewGuid(), "some text", -1);

            // WHEN
            await _sut.Save(aggregateRoot);

            // THEN
            aggregateRoot.HasChanges.Should().BeFalse();
        }

        [Fact]
        public async Task should_give_concurrency_exception_when_trying_to_save_an_aggregate_root_without_events()
        {
            // GIVEN
            var aggregateRoot = EmptyAggregateRoot.Create();

            // WHEN
            Func<Task> action = async () => await _sut.Save(aggregateRoot);

            // THEN
            action.Should().Throw<ConcurrencyException>();
        }

        [Fact]
        public async Task should_give_concurrency_exception_when_trying_to_save_events_on_old_aggregate_root()
        {
            // GIVEN
            var id = Guid.NewGuid();
            var aggregateRoot = SampleAggregateRoot.Create(id, "some text", -1);
            await _sut.Save(aggregateRoot);

            var sameAggregateRoot = SampleAggregateRoot.Create(id, "some text", 1);

            // WHEN
            Func<Task> action = async () => await _sut.Save(sameAggregateRoot);

            // THEN
            action.Should().Throw<ConcurrencyException>();
        }

        [Fact]
        public async Task
            should_give_concurrency_exception_when_trying_to_save_an_aggregate_root_with_non_initial_version_and_no_version_in_db()
        {
            // GIVEN
            var id = Guid.NewGuid();
            var aggregateRoot = SampleAggregateRoot.Create(id, "some text", 12);

            // WHEN
            Func<Task> action = async () => await _sut.Save(aggregateRoot);

            // THEN
            action.Should().Throw<ConcurrencyException>();
        }

        public void Dispose()
        {
            _autofacSandbox?.Dispose();
        }
    }
}