namespace RR.Livraria.Domain.Interfaces.Notification;

public interface IDomainNotification
{
    IReadOnlyCollection<Domain.Notification.Notification> Notifications { get; }
    bool HasNotifications { get; }
    void AddNotification(string key, string message);
}
