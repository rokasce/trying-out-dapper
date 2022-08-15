using API.Domain;
using API.Contracts.Requests;
using API.Domain.Common;

namespace API.Mapping;

public static class ApiContractToDomainMapper
{
    public static User ToUser(this UserRequest request)
    {
        return new User
        {
            Id = Id.From(Guid.NewGuid()),
            Email = Email.From(request.Email),
            FullName = FullName.From(request.FullName),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.DateOfBirth))
        };
    }

    public static User ToUser(this UpdateUserRequest request)
    {
        return new User
        {
            Id = Id.From(request.Id),
            Email = Email.From(request.User.Email),
            FullName = FullName.From(request.User.FullName),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.User.DateOfBirth))
        };
    }

    public static Post ToPost(this PostRequest request)
    {
        return new Post
        {
            Id = Id.From(Guid.NewGuid()),
            UserId = Id.From(request.UserId),
            Title = Title.From(request.Title),
            Content = Content.From(request.Content),
        };
    }

    public static Post ToPost(this UpdatePostRequest request)
    {
        return new Post
        {
            Id = Id.From(request.Id),
            UserId = Id.From(request.Post.UserId),
            Title = Title.From(request.Post.Title),
            Content = Content.From(request.Post.Content),
        };
    }
}

