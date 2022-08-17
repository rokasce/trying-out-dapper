using API.Domain.Common;

namespace API.Domain;

public class Post: BaseEntity
{
    public Id UserId { get; init; } = Id.From(Guid.NewGuid());

    public Title Title { get; init; } = default!;

    public Content Content { get; init; } = default!;

    public List<Comment> Comments { get; init; } = new List<Comment>();
}
