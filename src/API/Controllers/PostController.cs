using API.Attributes;
using API.Contracts.Requests;
using API.Mapping;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost("posts")]
    public async Task<IActionResult> Create([FromBody] PostRequest request)
    {
        var post = request.ToPost();
        await _postService.CreateAsync(post);
        var postResponse = post.ToPostResponse();

        return CreatedAtAction("Get", new { postResponse.Id }, postResponse);
    }

    [HttpGet("posts/{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var post = await _postService.GetAsync(id);
        if (post is null)
        {
            return NotFound();
        }

        var postResponse = post.ToPostResponse();

        return Ok(postResponse);
    }

    [HttpGet("posts")]
    public async Task<IActionResult> GetAll()
    {
        var posts = await _postService.GetAllAsync();
        var postsResponse = posts.ToPostsResponse();

        return Ok(postsResponse);
    }

    [HttpPut("posts/{id:guid}")]
    public async Task<IActionResult> Update([FromMultiSource] UpdatePostRequest request)
    {
        var existingPost = await _postService.GetAsync(request.Id);
        if (existingPost is null)
        {
            return NotFound();
        }

        var post = request.ToPost();
        await _postService.UpdateAsync(post);

        var postResponse = post.ToPostResponse();

        return Ok(postResponse);
    }

    [HttpDelete("posts/{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id) 
    {
        var deleted = await _postService.DeleteAsync(id);
        if (!deleted) 
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpGet("posts/getFull/{id:guid}")]
    public async Task<IActionResult> GetFull([FromRoute] Guid id)
    {
        var post = await _postService.GetFullAsync(id);
        if (post is null)
        {
            return NotFound();
        }

        var postResponse = post.ToPostResponse();

        return Ok(postResponse);
    }
}
