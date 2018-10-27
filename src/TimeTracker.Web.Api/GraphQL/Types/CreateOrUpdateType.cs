using GraphQL.Types;
using TimeTracker.Web.Api.GraphQL.Types.Outputs;

namespace TimeTracker.Web.Api.GraphQL.Types
{
    public class CreateOrUpdateType : ObjectGraphType<CreateOrUpdateOutput>
    {
        public CreateOrUpdateType()
        {
            Name = nameof(CreateOrUpdateOutput);
            Field(x => x.Id, type: typeof(IdGraphType));
        }
    }
}