namespace API.Contracts.Responses;

public class GetAllPostsResponse
{
    public IEnumerable<PostResponse> Posts { get; init; } = Enumerable.Empty<PostResponse>();
}

