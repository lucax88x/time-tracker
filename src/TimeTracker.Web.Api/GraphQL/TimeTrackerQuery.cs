using System;
using System.Threading.Tasks;
using GraphQL.Types;
using MediatR;
using TimeTracker.Application.TimeTrack.Query;
using TimeTracker.Infra.Read.TimeTrack;
using TimeTracker.Web.Api.GraphQL.Types;

namespace TimeTracker.Web.Api.GraphQL
{
    public class TimeTrackerQuery : ObjectGraphType<TimeTrackReadDto>
    {
        public TimeTrackerQuery(IMediator mediator)
        {
            Name = "Query";

            Func<ResolveFieldContext, Guid, Task<TimeTrackReadDto>> func = async (context, id) => await mediator.Send(new GetTimeTrackById(id));

            FieldDelegate<TimeTrackType>(
                "timetrack",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the time track" }
                ),
                resolve: func
            );
        }
    }
}
