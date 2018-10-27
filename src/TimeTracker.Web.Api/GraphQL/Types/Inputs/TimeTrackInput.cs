using System;

namespace TimeTracker.Web.Api.GraphQL.Types.Inputs
{
    public class TimeTrackInput
    {
        public Guid Id { get; set; }
        public DateTimeOffset When { get; set; }
    }
}