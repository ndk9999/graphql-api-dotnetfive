using LesJacksonCourse.Data;
using LesJacksonCourse.Models;

namespace LesJacksonCourse.GraphQL.Commands
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor
                .Description("Represents any executable command");

            descriptor
                .Field(c => c.Platform)
                .ResolveWith<PlatformResolver>(s => s.GetPlatform(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the platform to which the command belongs");
        }

        private class PlatformResolver
        {
            public Platform GetPlatform([Parent] Command command, [ScopedService] AppDbContext context)
            {
                return context.Platforms.FirstOrDefault(x => x.Id == command.PlatformId);
            }
        }
    }
}