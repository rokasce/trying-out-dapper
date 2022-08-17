using Microsoft.AspNetCore.Mvc;

namespace API.Contracts.Requests;

public class UpdateCommentRequest
{
    [FromRoute(Name = "id")] public Guid Id { get; init; }
    [FromBody] public CommentRequest Comment { get; set; } = default!;
}
