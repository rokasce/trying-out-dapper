using API.Domain;

namespace API.Services;

public interface ICommentService
{
    Task<bool> CreateAsync(Comment comment);

    Task<Comment?> GetAsync(Guid id);

    Task<IEnumerable<Comment>> GetAllAsync();

    Task<bool> UpdateAsync(Comment comment);

    Task<bool> DeleteAsync(Guid id);
}
