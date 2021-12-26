using Duende.IdentityServer.EntityFramework.Options;
using GraphQLTMS.Core.Domain;
using GraphQLTMS.Shared.Domain;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GraphQLTMS.Core.Data
{
    public class GraphQLTMSDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public GraphQLTMSDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
    }
}