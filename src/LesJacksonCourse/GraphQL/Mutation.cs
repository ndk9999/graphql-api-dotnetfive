using HotChocolate.Subscriptions;
using LesJacksonCourse.Data;
using LesJacksonCourse.GraphQL.Commands;
using LesJacksonCourse.GraphQL.Platforms;
using LesJacksonCourse.Models;

namespace LesJacksonCourse.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, 
            [ScopedService] AppDbContext context, [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken = default)
        {
            var platform = new Platform()
            {
                Name = input.Name
            };

            context.Platforms.Add(platform);
            await context.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);

            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context)
        {
            var command = new Command()
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };

            context.Commands.Add(command);
            await context.SaveChangesAsync();

            return new AddCommandPayload(command);
        }
    }
}