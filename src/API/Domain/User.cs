using API.Domain.Common;

namespace API.Domain;

public class User
{
    public UserId Id { get; init; } = UserId.From(Guid.NewGuid());

    public FullName FullName { get; init; } = default!;

    public Email Email { get; init; } = default!;
    
    public DateOfBirth DateOfBirth { get; init; } = default!;
}
