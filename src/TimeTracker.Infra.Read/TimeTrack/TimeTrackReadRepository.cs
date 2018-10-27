using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using TimeTracker.Infra.Read.Core;

namespace TimeTracker.Infra.Read.TimeTrack
{
    public interface ITimeTrackReadRepository
    {
        Task Add(Guid id, DateTimeOffset when);
        Task<ImmutableArray<TimeTrackReadDto>> Get();
        Task<TimeTrackReadDto> GetById(Guid id);
    }

    public class TimeTrackReadRepository : ITimeTrackReadRepository
    {
        private readonly IReadRepository _repository;

        public TimeTrackReadRepository(ReadRepositoryFactory readRepositoryFactory)
        {
            _repository = readRepositoryFactory.Build("timetrack");
        }

        public async Task Add(Guid id, DateTimeOffset when)
        {
            var dto = new TimeTrackReadDto(id, when);

            await _repository.Set(id, dto);
            await _repository.SortedSetAdd("by-when", dto.When.UtcTicks, dto);
        }

        public async Task<ImmutableArray<TimeTrackReadDto>> Get()
        {
            return await _repository.SortedSetRangeByScore<TimeTrackReadDto>("by-when");
        }

        public async Task<TimeTrackReadDto> GetById(Guid id)
        {
            return await _repository.Get<TimeTrackReadDto>(id);
        }
    }
}