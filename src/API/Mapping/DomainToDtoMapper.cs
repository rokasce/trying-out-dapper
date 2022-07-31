using API.Contracts.Data;
using API.Domain;

namespace API.Mapping;

public static class DomainToDtoMapper
{
    public static UserDto ToUserDto(this User user) 
    {
        return new UserDto
        {
            Id = user.Id.Value,
            FullName = user.FullName.Value,
            Email = user.Email.Value,
            DateOfBirth = user.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
        };
    }
}

