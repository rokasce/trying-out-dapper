namespace API.Contracts.Responses;

public class UserResponse
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = default!;

    public string Email{ get; init; } = default!;
    
    public DateTime DateOfBirth { get; init; } = default!;

    public DateTime CreatedAt { get; init; } = default!;
}
