using GraphQL.Server.Ui.Voyager;
using LesJacksonCourse.Data;
using LesJacksonCourse.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections();          // Do not use this method when we use GraphQL types & resolvers

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGraphQL();

app.MapGraphQLVoyager(new VoyagerOptions() {
    GraphQLEndPoint = "/graphql"
}, "/graphql-voyager");

app.Run();
