using Microsoft.AspNetCore.Mvc;

namespace API.Contracts.Requests;

public class UpdateUserRequest
{
    [FromRoute(Name = "id")] public Guid Id { get; init; }
    [FromBody] public UserRequest User { get; set; } = default!;
}
