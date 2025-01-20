namespace RR.Livraria.Domain.Notification;

public record Notification
{
    public Notification(string key, string value)
    {
        Id = Guid.NewGuid();
        Key = key;
        Value = value;
    }

    public Guid Id { get; init; }
    public string Key { get; init; }
    public string Value { get; init; }
}
