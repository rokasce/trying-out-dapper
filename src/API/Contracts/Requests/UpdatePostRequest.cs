using Microsoft.AspNetCore.Mvc;

namespace API.Contracts.Requests;

public class UpdatePostRequest
{
    [FromRoute(Name = "id")] public Guid Id { get; init; }
    [FromBody] public PostRequest Post { get; set; } = default!;
}
