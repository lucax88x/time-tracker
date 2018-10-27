using GraphQL.Types;
using TimeTracker.Web.Api.GraphQL.Types.Inputs;

namespace TimeTracker.Web.Api.GraphQL.Types
{
    public class TimeTrackInputType : InputObjectGraphType<TimeTrackInput>
    {
        public TimeTrackInputType()
        {
            Name = nameof(TimeTrackInput);
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.When);
        }
    }
}