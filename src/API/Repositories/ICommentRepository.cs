using API.Contracts.Data;

namespace API.Repositories;

public interface ICommentRepository
{
    Task<bool> CreateAsync(CommentDto comment);

    Task<CommentDto> GetAsync(Guid id);

    Task<IEnumerable<CommentDto>> GetAllAsync();

    Task<bool> UpdateAsync(CommentDto comment);

    Task<bool> DeleteAsync(Guid id);

    Task<IEnumerable<CommentDto>> GetPostComments(Guid postId);
}
