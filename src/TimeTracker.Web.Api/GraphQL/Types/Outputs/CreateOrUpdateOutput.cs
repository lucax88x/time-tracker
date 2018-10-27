using System;

namespace TimeTracker.Web.Api.GraphQL.Types.Outputs
{
    public class CreateOrUpdateOutput
    {
        public Guid Id { get; }

        public CreateOrUpdateOutput(Guid id)
        {
            Id = id;
        }
    }
}