

namespace Sales.Application.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync(string eventName, string message);
    }
}
