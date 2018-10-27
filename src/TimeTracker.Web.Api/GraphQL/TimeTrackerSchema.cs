using GraphQL;
using GraphQL.Types;

namespace TimeTracker.Web.Api.GraphQL
{
    public class TimeTrackerSchema : Schema
    {
        public TimeTrackerSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<TimeTrackQuery>();
            Mutation = resolver.Resolve<TimeTrackMutation>();
        }
    }
}