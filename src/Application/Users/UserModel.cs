using Infrastructure.Users;

namespace Application.Users;

public sealed record UserModel(
    Guid Id,
    string ProviderId,
    string DisplayName,
    string Email,
    DateTime CreatedAt
)
{
    public static UserModel From(UserDbo user) =>
        new(user.Id, user.ProviderId, user.DisplayName, user.Email, user.CreatedAt);
}
