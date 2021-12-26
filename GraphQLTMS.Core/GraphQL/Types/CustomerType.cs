using GraphQLTMS.Shared.Domain;

namespace GraphQLTMS.Core.GraphQL.Types
{
    public class CustomerType : ObjectType<Customer>
    {
        protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
        {
            descriptor.Description("Represents a customer that is allowed to use TMS application.");

            descriptor
                .Field(p => p.SpendingLimit).Ignore();

            //descriptor
            //    .Field(p => p.Commands)
            //    .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
            //    .UseDbContext<AppDbContext>()
            //    .Description("Represents list of available commands for this platform.");
        }

        //private class Resolvers
        //{
        //    public IQueryable<Command> GetCommands([Parent] Platform platform, [ScopedService] AppDbContext context)
        //    {
        //        return context.Commands.Where(p => p.PlatformId == platform.Id);
        //    }
        //}
    }
}
