using GraphQL.Types;

namespace TimeTracker.Web.Api.GraphQL.Types
{
    public class TimeTrackTypeEnum : EnumerationGraphType
    {
        public TimeTrackTypeEnum()
        {
            Name = "TimeTrackType";
            Description = "Types of TimeTrack";
            AddValue("IN", "You are entering", (int)Domain.TimeTrack.TimeTrackType.In);
            AddValue("OUT", "You are exiting", (int)Domain.TimeTrack.TimeTrackType.Out);
        }
    }
}