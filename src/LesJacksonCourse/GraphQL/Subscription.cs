using LesJacksonCourse.Models;

namespace LesJacksonCourse.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public Platform OnPlatformAdded([EventMessage] Platform platform)
        {
            return platform;
        }
    }
}