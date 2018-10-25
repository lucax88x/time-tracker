using GraphQL.Types;
using TimeTracker.Infra.Read.TimeTrack;

namespace TimeTracker.Web.Api.GraphQL.Types
{
    public class TimeTrackType : ObjectGraphType<TimeTrackReadDto>
    {
        public TimeTrackType()
        {
            Name = "timetrack";
            Description = "It's the single time track";

            Field(d => d.Id, type: typeof(IdGraphType)).Description("The id of the time track.");
            Field(d => d.When).Description("When it happened");
           
        }
    }
}