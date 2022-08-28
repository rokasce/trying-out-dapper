using API.Contracts.Data;
using API.Domain;

namespace API.Mapping;

public static class DomainToDtoMapper
{
    public static UserDto ToUserDto(this User user) 
    {
        return new UserDto
        {
            Id = user.Id.Value,
            FullName = user.FullName.Value,
            Email = user.Email.Value,
            DateOfBirth = user.DateOfBirth.Value,
            CreatedAt = user.CreatedAt.Value,
        };
    }

    public static PostDto ToPostDto(this Post post) 
    {
        return new PostDto
        {
            Id = post.Id.Value,
            UserId = post.UserId.Value,
            Title = post.Title.Value,
            Content = post.Content.Value,
            CreatedAt = post.CreatedAt.Value,
            Comments = post.Comments.Select(x => x.ToCommentDto()).ToList(),
        };
    }

    public static CommentDto ToCommentDto(this Comment comment) 
    {
        return new CommentDto
        {
            Id = comment.Id.Value,
            UserId = comment.UserId.Value,
            PostId = comment.PostId.Value,
            Content = comment.Content.Value,
            CreatedAt = comment.CreatedAt.Value,
        };
    }
}

