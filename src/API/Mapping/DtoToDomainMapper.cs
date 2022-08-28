using API.Contracts.Data;
using API.Domain;
using API.Domain.Common;

namespace API.Mapping;

public static class DtoToDomainMapper
{
    public static User ToUser(this UserDto userDto)
    {
        return new User
        {
            Id = Id.From(userDto.Id),
            Email = Email.From(userDto.Email),
            FullName = FullName.From(userDto.FullName),
            DateOfBirth = DateCreated.From(userDto.DateOfBirth),
            CreatedAt = DateCreated.From(userDto.CreatedAt)
        };
    }

    public static Post ToPost(this PostDto postDto)
    {
        return new Post
        {
            Id = Id.From(postDto.Id),
            UserId = Id.From(postDto.UserId),
            Title = Title.From(postDto.Title),
            Content = Content.From(postDto.Content),
            CreatedAt = DateCreated.From(postDto.CreatedAt),
            Comments = postDto.Comments.Select(x => x.ToComment()).ToList(),
        };
    }

    public static Comment ToComment(this CommentDto commentDto)
    {
        return new Comment
        {
            Id = Id.From(commentDto.Id),
            UserId = Id.From(commentDto.UserId),
            PostId = Id.From(commentDto.PostId),
            Content = Content.From(commentDto.Content),
            CreatedAt = DateCreated.From(commentDto.CreatedAt)
        };
    }
}

