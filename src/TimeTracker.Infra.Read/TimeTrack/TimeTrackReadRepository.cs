using System;
using System.Threading.Tasks;

namespace TimeTracker.Infra.Read.TimeTrack
{
    public interface ITimeTrackReadRepository
    {
        Task<TimeTrackReadDto> GetById(Guid id);
    }

    public class TimeTrackReadRepository : ITimeTrackReadRepository
    {
        public async Task<TimeTrackReadDto> GetById(Guid id)
        {
            return await Task.FromResult(new TimeTrackReadDto(id, DateTimeOffset.UtcNow));
        }
    }
}