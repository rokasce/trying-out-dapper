using API.Domain.Common;

namespace API.Domain;

public class Comment: BaseEntity
{
    public Id UserId { get; init; } = Id.From(Guid.NewGuid());

    public Id PostId { get; init; } = Id.From(Guid.NewGuid());

    public Content Content { get; init; } = default!;

    public Post Post { get; init; } = default!;
    
    public User User { get; init; } = default!;
}
