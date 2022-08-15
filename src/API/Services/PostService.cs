using API.Domain;
using API.Mapping;
using API.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace API.Services;

public class PostService : IPostService 
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<bool> CreateAsync(Post post)
    {
        var existingPost = await _postRepository.GetAsync(post.Id.Value);
        if(existingPost is not null) 
        {
            var message = $"A post with id {post.Id} already exists";
            throw new ValidationException(message, GenerateValidationError(message));
        }

        var postDto = post.ToPostDto();

        return await _postRepository.CreateAsync(postDto);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _postRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        var postDtos = await _postRepository.GetAllAsync();

        return postDtos.Select(x => x.ToPost());
    }

    public async Task<IEnumerable<Post>> GetAllUsersPostsAsync(Guid userId)
    {
        var postDtos = await _postRepository.GetUserPosts(userId);

        return postDtos.Select(x => x.ToPost());
    }

    public async Task<Post?> GetAsync(Guid id)
    {
        var postDto = await _postRepository.GetAsync(id);

        return postDto?.ToPost();
    }

    public async Task<bool> UpdateAsync(Post post)
    {
        var postDto = post.ToPostDto();

        return await _postRepository.UpdateAsync(postDto);
    }

    private static ValidationFailure[] GenerateValidationError(string message) 
    {
        return new[] 
        { 
            new ValidationFailure(nameof(Post), message) 
        };
    }
}

