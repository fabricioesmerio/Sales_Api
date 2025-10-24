
using Sales.Application.Interfaces;

namespace Sales.Application.Messaging
{
    public class FakeRebusPublisher : IEventPublisher
    {

        public Task PublishAsync(string eventName, string message)
        {
            Console.WriteLine($"[EVENT PUBLISHED] {eventName} -> {message}");
            return Task.CompletedTask;
        }
    }
}
