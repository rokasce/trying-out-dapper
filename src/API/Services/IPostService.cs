using API.Domain;

namespace API.Services;

public interface IPostService
{
    Task<bool> CreateAsync(Post post);

    Task<Post?> GetAsync(Guid id);

    Task<Post?> GetFullAsync(Guid id);

    Task<IEnumerable<Post>> GetAllAsync();

    Task<bool> UpdateAsync(Post post);

    Task<bool> DeleteAsync(Guid id);
}
