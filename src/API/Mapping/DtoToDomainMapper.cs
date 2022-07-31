using API.Contracts.Data;
using API.Domain;
using API.Domain.Common;

namespace API.Mapping;

public static class DtoToDomainMapper
{
    public static User ToUser(this UserDto userDto)
    {
        return new User
        {
            Id = UserId.From(userDto.Id),
            Email = Email.From(userDto.Email),
            FullName = FullName.From(userDto.FullName),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(userDto.DateOfBirth))
        };
    }
}

