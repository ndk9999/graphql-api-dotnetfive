using LesJacksonCourse.Data;
using LesJacksonCourse.Models;

namespace LesJacksonCourse.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor
                .Description("Represents any software or service that has a command line interface.");

            descriptor
                .Field(p => p.LicenseKey)
                .Ignore();

            descriptor
                .Field(p => p.Commands)
                .ResolveWith<CommandResolver>(s => s.GetCommands(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the list of available commands for this platform");
        }

        private class CommandResolver
        {
            public IQueryable<Command> GetCommands([Parent] Platform platform, [ScopedService] AppDbContext context)
            {
                return context.Commands.Where(x => x.PlatformId == platform.Id);
            }
        }
    }
}