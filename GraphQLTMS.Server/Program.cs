using GraphQLTMS.Core.Data;
using GraphQLTMS.Core.Domain;
using GraphQLTMS.Core.GraphQL;
using GraphQLTMS.Core.GraphQL.Types;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationUserDbContext>(options =>
    options.UseSqlServer(connectionString)
);
builder.Services.AddPooledDbContextFactory<TMSDbContext>(options =>
    options.UseSqlServer(connectionString)
);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationUserDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationUserDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    //.AddMutationType<Mutation>()
    //.AddSubscriptionType<Subscription>()
    //.AddType<PlatformType>()
    .AddType<CustomerType>()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions() // keeps track of subscribers in memory (can be in DB)
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment());

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapGraphQL();
});

app.UseGraphQLVoyager(new GraphQL.Server.Ui.Voyager.VoyagerOptions()
{
    GraphQLEndPoint = "/graphql"
}, "/graphqlvoyager");

app.UseIdentityServer();
app.UseAuthentication();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

await DbInitializer.Populate(app, app.Configuration, app.Environment);

app.Run();
