using System;
using GraphQL.Types;
using MediatR;
using TimeTracker.Application.TimeTrack.Query;
using TimeTracker.Web.Api.GraphQL.Types;

namespace TimeTracker.Web.Api.GraphQL
{
    public class TimeTrackQuery : ObjectGraphType
    {
        public TimeTrackQuery(IMediator mediator)
        {
            Name = nameof(TimeTrackQuery);

            FieldAsync<TimeTrackType>(
                "TimeTrack",
                "The time track",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                        {Name = "id", Description = "id of the time track"}
                ),
                async context => await mediator.Send(new GetTimeTrackById(context.GetArgument<Guid>("id"))));

            FieldAsync<ListGraphType<TimeTrackType>>(
                "TimeTracks",
                "The time tracks of user",
                resolve: async context => await mediator.Send(new GetTimeTracks()));
        }
    }
}