using System.Collections.Immutable;
using TimeTracker.Core.Interfaces;
using TimeTracker.Infra.Read.TimeTrack;

namespace TimeTracker.Application.TimeTrack.Query
{
    public class GetTimeTracks : Query<ImmutableArray<TimeTrackReadDto>>
    {
    }
}