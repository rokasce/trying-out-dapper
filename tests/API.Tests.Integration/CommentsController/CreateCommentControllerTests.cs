using API.Contracts.Requests;
using API.Contracts.Responses;
using Bogus;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Integration.CommentsController;

public class CreateCommentControllerTests : IClassFixture<UserApiFactory>
{
    private readonly UserApiFactory _userApiFactory;
    private readonly HttpClient _client;
    private readonly Faker<UserRequest> _userGenerator = new Faker<UserRequest>()
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public CreateCommentControllerTests(UserApiFactory userApiFactory)
    {
        _userApiFactory = userApiFactory;
        _client = _userApiFactory.CreateClient();
    }

    [Fact]
    public async Task Create_ShouldNotCreateComment_WhenUserDoesNotExit()
    {
        // Arrange
        var user = _userGenerator.Generate();

        // Act
        var userCreateResponse = await _client.PostAsJsonAsync("users", user);
        var createdUser = await userCreateResponse.Content.ReadFromJsonAsync<UserResponse>();
        
        var post = new PostRequest 
        {
            UserId = createdUser!.Id,
            Title = "Test Post #1",
            Content = "Test Post #1",
        };
        var postCreateResponse = await _client.PostAsJsonAsync("posts", post);
        var createdPost = await postCreateResponse.Content.ReadFromJsonAsync<PostResponse>();

        var comment = new CommentRequest
        {
            UserId = Guid.NewGuid(),
            PostId = createdPost!.Id,
            Content = "Test Comment #1"
        };
        var createCommentResponse = await _client.PostAsJsonAsync("comments", comment);

        // Assert
        createCommentResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
