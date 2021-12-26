using Duende.IdentityServer.EntityFramework.Options;
using GraphQLTMS.Core.Domain;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GraphQLTMS.Core.Data
{
    public class ApplicationUserDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationUserDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

    }
}