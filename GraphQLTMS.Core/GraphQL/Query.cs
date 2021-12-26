using GraphQLTMS.Core.Data;
using GraphQLTMS.Shared.Domain;

namespace GraphQLTMS.Core.GraphQL
{

    public class Query
    {
        [UseDbContext(typeof(TMSDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Customer> GetCustomer([ScopedService] TMSDbContext context)
        {
            return context.Customers;
        }

        [UseDbContext(typeof(TMSDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Carrier> GetCarriers([ScopedService] TMSDbContext context)
        {
            return context.Carriers;
        }

        [UseDbContext(typeof(TMSDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Shipment> GetShipments([ScopedService] TMSDbContext context)
        {
            return context.Shipments;
        }
    }


}
