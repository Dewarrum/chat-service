namespace Domain;

public sealed class User
{
    public User(
        string providerId)
    {
        Id = Guid.NewGuid();
        ProviderId = providerId;
        CreatedAt = DateTime.UtcNow;
    }

    private User()
    {
    }

    public Guid Id { get; init; }
    public string ProviderId { get; init; } = default!;
    public DateTime CreatedAt { get; init; }
}