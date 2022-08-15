using API.Domain.Common;

namespace API.Domain
{
    public class BaseEntity
    {
        public Id Id { get; init; } = Id.From(Guid.NewGuid());
    }
}
