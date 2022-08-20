using API.Domain;
using API.Domain.Common;
using API.Mapping;
using API.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace API.Services;

public class CommentService: ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserService _userService;

    public CommentService(ICommentRepository commentRepository, IUserService userService)
    {
        _commentRepository = commentRepository;
        _userService = userService;
    }

    public async Task<bool> CreateAsync(Comment comment)
    {
        await ValidateIfUserExit(comment);

        var existingComment = await _commentRepository.GetAsync(comment.Id.Value);
        if (existingComment is not null)
        {
            var message = $"A comment with id {comment.Id} already exists";
            throw new ValidationException(message, GenerateValidationError(message));
        }

        var commentDto = comment.ToCommentDto();

        return await _commentRepository.CreateAsync(commentDto);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _commentRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
        var commentDtos = await _commentRepository.GetAllAsync();

        return commentDtos.Select(x => x.ToComment());
    }

    public async Task<Comment?> GetAsync(Guid id)
    {
        var commentDto = await _commentRepository.GetAsync(id);

        return commentDto?.ToComment();
    }

    public async Task<bool> UpdateAsync(Comment comment)
    {
        await ValidateIfUserExit(comment);

        var commentDto = comment.ToCommentDto();

        return await _commentRepository.UpdateAsync(commentDto);
    }

    private static ValidationFailure[] GenerateValidationError(string message) 
    {
        return new[] 
        { 
            new ValidationFailure(nameof(Comment), message) 
        };
    }

    private async Task ValidateIfUserExit(Comment comment)
    {
        if (comment.UserId is not null)
        {
            var user = await _userService.GetAsync(comment!.UserId.Value);
            if (user == null)
            {
                var message = "A comment without existing user cannot be created";
                throw new ValidationException(message, GenerateValidationError(message));
            }
        }
    }
}

