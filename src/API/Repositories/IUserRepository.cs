using API.Contracts.Data;

namespace API.Repositories;

public interface IUserRepository
{
    Task<bool> CreateAsync(UserDto user);

    Task<UserDto?> GetAsync(Guid id);

    Task<IEnumerable<UserDto>> GetAllAsync();

    Task<bool> UpdateAsync(UserDto user);

    Task<bool> DeleteAsync(Guid id);
}
