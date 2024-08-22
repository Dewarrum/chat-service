namespace Domain;

public sealed class Message
{
    public Message(string text, Guid userId)
    {
        Id = Guid.NewGuid();
        Text = text;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }

    internal Message() { }

    public Guid Id { get; init; }
    public string Text { get; init; } = default!;
    public Guid UserId { get; init; } = default!;
    public DateTime CreatedAt { get; init; }
}
