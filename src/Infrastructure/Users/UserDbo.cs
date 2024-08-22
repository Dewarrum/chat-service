namespace Infrastructure.Users;

public sealed class UserDbo
{
    public Guid Id { get; init; }
    public string ProviderId { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string DisplayName { get; init; } = default!;
    public string? AvatarUrl { get; init; }
    public DateTime CreatedAt { get; init; }
}
