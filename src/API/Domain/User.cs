using API.Domain.Common;

namespace API.Domain;

public class User : BaseEntity
{
    public FullName FullName { get; init; } = default!;

    public Email Email { get; init; } = default!;
    
    public DateCreated DateOfBirth { get; init; } = default!;

    public List<Post> Posts { get; init; } = new List<Post>();
}
