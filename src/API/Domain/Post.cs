using API.Domain.Common;

namespace API.Domain;

public class Post
{
    public UserId Id { get; init; } = UserId.From(Guid.NewGuid());
    
    public UserId UserId { get; init; } = UserId.From(Guid.NewGuid());

    public Title Title { get; init; } = default!;

    public Title Content { get; init; } = default!;
}
