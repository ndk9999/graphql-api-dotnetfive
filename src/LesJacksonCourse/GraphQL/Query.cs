using LesJacksonCourse.Data;
using LesJacksonCourse.Models;

namespace LesJacksonCourse.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseProjection]         // Do not use this attribute when we use GraphQL types & resolvers
        public IQueryable<Platform> GetPlatforms([ScopedService] AppDbContext context)
        {
            return context.Platforms;
        }


        [UseDbContext(typeof(AppDbContext))]
        [UseProjection]         // Do not use this attribute when we use GraphQL types & resolvers
        public IQueryable<Command> GetCommands([ScopedService] AppDbContext context)
        {
            return context.Commands;
        }
    }
}