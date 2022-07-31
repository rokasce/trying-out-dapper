using API.Contracts.Responses;
using API.Domain;

namespace API.Mapping;

public static class DomainToApiContractMapper
{
    public static UserResponse ToUserResponse(this User customer)
    {
        return new UserResponse
        {
            Id = customer.Id.Value,
            Email = customer.Email.Value,
            FullName = customer.FullName.Value,
            DateOfBirth = customer.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
        };
    }

    public static GetAllUsersResponse ToUsersResponse(this IEnumerable<User> customers)
    {
        return new GetAllUsersResponse
        {
            Users = customers.Select(x => new UserResponse
            {
                Id = x.Id.Value,
                Email = x.Email.Value,
                FullName = x.FullName.Value,
                DateOfBirth = x.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
            })
        };
    }
}

