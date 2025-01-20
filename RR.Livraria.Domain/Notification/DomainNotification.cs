using RR.Livraria.Domain.Interfaces.Notification;

namespace RR.Livraria.Domain.Notification;

public class DomainNotification : IDomainNotification
{
    private readonly List<Notification> _notifications;

    public DomainNotification()
    {
        _notifications = [];
    }

    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public bool HasNotifications => _notifications.Count > 0;

    public void AddNotification(string key, string message)
    {
        _notifications.Add(new Notification(key, message));
    }
}
