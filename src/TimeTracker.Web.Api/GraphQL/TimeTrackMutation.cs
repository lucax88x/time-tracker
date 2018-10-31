using System;
using System.Threading.Tasks;
using GraphQL.Types;
using MediatR;
using TimeTracker.Application.TimeTrack.Commands;
using TimeTracker.Web.Api.GraphQL.Types;
using TimeTracker.Web.Api.GraphQL.Types.Inputs;
using TimeTracker.Web.Api.GraphQL.Types.Outputs;

namespace TimeTracker.Web.Api.GraphQL
{
    public class TimeTrackMutation : ObjectGraphType<TimeTrackInput>
    {
        public TimeTrackMutation(IMediator mediator)
        {
            Name = nameof(TimeTrackMutation);

            async Task<CreateOrUpdateOutput> CreateTimeTrack(ResolveFieldContext<TimeTrackInput> context)
            {
                var input = context.GetArgument<TimeTrackInput>("timeTrack");
                var id = await mediator.Send(new CreateTimeTrack(input.When, input.Type, input.Id));
                return new CreateOrUpdateOutput(id);
            }

            Field<CreateOrUpdateType>(
                "timeTrack",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<TimeTrackInputType>> {Name = "timeTrack"}
                ),
                resolve: (Func<ResolveFieldContext<TimeTrackInput>, Task<CreateOrUpdateOutput>>) CreateTimeTrack);
        }
    }
}