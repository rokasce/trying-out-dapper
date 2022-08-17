using API.Contracts.Responses;
using API.Domain;

namespace API.Mapping;

public static class DomainToApiContractMapper
{
    public static UserResponse ToUserResponse(this User customer)
    {
        return new UserResponse
        {
            Id = customer.Id.Value,
            Email = customer.Email.Value,
            FullName = customer.FullName.Value,
            DateOfBirth = customer.DateOfBirth.Value,
            CreatedAt = customer.CreatedAt.Value
        };
    }

    public static GetAllUsersResponse ToUsersResponse(this IEnumerable<User> customers)
    {
        return new GetAllUsersResponse
        {
            Users = customers.Select(x => new UserResponse
            {
                Id = x.Id.Value,
                Email = x.Email.Value,
                FullName = x.FullName.Value,
                DateOfBirth = x.DateOfBirth.Value,
                CreatedAt = x.CreatedAt.Value
            })
        };
    }

    public static PostResponse ToPostResponse(this Post post)
    {
        return new PostResponse
        {
            Id = post.Id.Value,
            UserId = post.UserId.Value,
            Title = post.Title.Value,
            Content = post.Content.Value,
            CreatedAt = post.CreatedAt.Value
        };
    }

    public static GetAllPostsResponse ToPostsResponse(this IEnumerable<Post> posts)
    {
        return new GetAllPostsResponse
        {
            Posts = posts.Select(post => new PostResponse
            {
                Id = post.Id.Value,
                UserId = post.UserId.Value,
                Title = post.Title.Value,
                Content = post.Content.Value,
                CreatedAt = post.CreatedAt.Value
            })
        };
    }

    public static CommentResponse ToCommentResponse(this Comment comment)
    {
        return new CommentResponse
        {
            Id = comment.Id.Value,
            UserId = comment.UserId.Value,
            PostId = comment.PostId.Value,
            Content = comment.Content.Value,
            CreatedAt = comment.CreatedAt.Value
        };
    }

    public static GetAllCommentsResponse ToCommentsResponse(this IEnumerable<Comment> comments)
    {
        return new GetAllCommentsResponse
        {
            Comments = comments.Select(comment => new CommentResponse
            {
                Id = comment.Id.Value,
                UserId = comment.UserId.Value,
                PostId = comment.PostId.Value,
                Content = comment.Content.Value,
                CreatedAt = comment.CreatedAt.Value
            })
        };
    }
}

