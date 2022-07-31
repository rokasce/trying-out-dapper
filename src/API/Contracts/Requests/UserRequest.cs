namespace API.Contracts.Requests;

public class UserRequest
{
    public string FullName { get; init; } = default!;

    public string Email{ get; init; } = default!;
    
    public DateTime DateOfBirth { get; init; } = default!;
}
