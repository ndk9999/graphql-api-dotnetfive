using GraphQL.Server.Ui.Voyager;
using LesJacksonCourse.Data;
using LesJacksonCourse.GraphQL;
using LesJacksonCourse.GraphQL.Platforms;
using LesJacksonCourse.GraphQL.Commands;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    //.AddQueryableOffsetPagingProvider()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions();
    // .AddProjections();          // Do not use this method when we use GraphQL types & resolvers

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseWebSockets();

app.MapGraphQL();

app.MapGraphQLVoyager(new VoyagerOptions() {
    GraphQLEndPoint = "/graphql"
}, "/graphql-voyager");

app.Run();
