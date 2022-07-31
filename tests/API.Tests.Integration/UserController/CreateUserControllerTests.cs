using API.Contracts.Requests;
using API.Contracts.Responses;
using Bogus;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Integration.UserController;

public class CreateUserControllerTests : IClassFixture<UserApiFactory>
{
    private readonly UserApiFactory _userApiFactory;
    private readonly HttpClient _client;
    private readonly Faker<UserRequest> _userGenerator = new Faker<UserRequest>()
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public CreateUserControllerTests(UserApiFactory userApiFactory)
    {
        _userApiFactory = userApiFactory;
        _client = _userApiFactory.CreateClient();
    }

    [Fact]
    public async Task Create_CreatesUser_WhenDataIsValid() 
    {
        // Arrange
        var user = _userGenerator.Generate();

        // Act
        var response = await _client.PostAsJsonAsync("users", user);

        // Assert
        var userResponse = await response.Content.ReadFromJsonAsync<UserResponse>();
        userResponse.Should().BeEquivalentTo(user);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location!.ToString().Should().Be($"http://localhost/users/{userResponse!.Id}");
    }
}
