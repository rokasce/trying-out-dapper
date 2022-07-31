using API.Domain;
using API.Contracts.Requests;
using API.Domain.Common;

namespace API.Mapping;

public static class ApiContractToDomainMapper
{
    public static User ToUser(this UserRequest request)
    {
        return new User
        {
            Id = UserId.From(Guid.NewGuid()),
            Email = Email.From(request.Email),
            FullName = FullName.From(request.FullName),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.DateOfBirth))
        };
    }

    public static User ToUser(this UpdateUserRequest request)
    {
        return new User
        {
            Id = UserId.From(request.Id),
            Email = Email.From(request.User.Email),
            FullName = FullName.From(request.User.FullName),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.User.DateOfBirth))
        };
    }
}

