namespace API.Contracts.Responses;

public class GetAllCommentsResponse
{
    public IEnumerable<CommentResponse> Comments { get; init; } = Enumerable.Empty<CommentResponse>();
}

