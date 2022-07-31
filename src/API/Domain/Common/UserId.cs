using ValueOf;

namespace API.Domain.Common;

public class UserId : ValueOf<Guid, UserId>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty) 
        {
            throw new ArgumentException("User Id cannot be empty", nameof(UserId));
        }
    }
}
