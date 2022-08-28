using API.Contracts.Data;

namespace API.Repositories;

public interface IPostRepository
{
    Task<bool> CreateAsync(PostDto post);

    Task<PostDto> GetAsync(Guid id);

    Task<IEnumerable<PostDto>> GetAllAsync();

    Task<bool> UpdateAsync(PostDto post);

    Task<bool> DeleteAsync(Guid id);

    Task<IEnumerable<PostDto>> GetUserPosts(Guid userId);

    Task<PostDto?> GetFullAsync(Guid id);
}
