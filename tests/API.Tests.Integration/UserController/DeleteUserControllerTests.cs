using API.Contracts.Requests;
using API.Contracts.Responses;
using Bogus;
using FluentAssertions;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Integration.UserController;

public class DeleteUserControllerTests : IClassFixture<UserApiFactory>
{
    private readonly UserApiFactory _userApiFactory;
    private readonly HttpClient _client;
    private readonly Faker<UserRequest> _userGenerator = new Faker<UserRequest>()
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public DeleteUserControllerTests(UserApiFactory userApiFactory)
    {
        _userApiFactory = userApiFactory;
        _client = _userApiFactory.CreateClient();
    }

    [Fact]
    public async Task Delete_ShouldRemoveAllAssociatedContent_WhenUserIsDeleted() 
    {
        // Arrange
        var user = _userGenerator.Generate();

        // Act
        var userResponse = await _client.PostAsJsonAsync("users", user);
        var createdUser = await userResponse.Content.ReadFromJsonAsync<UserResponse>();

        var post = new PostRequest
        {
            UserId = createdUser!.Id,
            Title = "Test Post #1",
            Content = "Test Post #1",
        };

        var postResponse = await _client.PostAsJsonAsync("posts", post);
        var createdPost = await postResponse.Content.ReadFromJsonAsync<PostResponse>();
        
        var comment = new CommentRequest 
        {
            UserId = createdUser!.Id,
            PostId = createdPost!.Id,
            Content = "Test Comment #1"
        };

        var commentResponse = await _client.PostAsJsonAsync("comments", comment);

        var deleteResponse = await _client.DeleteAsync($"users/{createdUser.Id}");

        var getUserPostsResponse = await _client.GetAsync("posts");
        var allUserPosts = await getUserPostsResponse.Content.ReadFromJsonAsync<GetAllPostsResponse>();

        var getUserCommentsResponse = await _client.GetAsync("comments");
        var allUserComments = await getUserCommentsResponse.Content.ReadFromJsonAsync<GetAllCommentsResponse>();

        // Assert
        deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        postResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        commentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        allUserPosts!.Posts.Should().BeEquivalentTo(Enumerable.Empty<PostResponse>());
        allUserComments!.Comments.Should().BeEquivalentTo(Enumerable.Empty<CommentResponse>());
    }
}
