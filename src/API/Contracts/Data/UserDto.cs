namespace API.Contracts.Data;

public class UserDto
{
    public Guid Id { get; set; } = default!;

    public string FullName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public DateTime DateOfBirth { get; init; }

    public DateTime CreatedAt { get; init; }
}
