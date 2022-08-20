using API.Contracts.Requests;
using API.Mapping;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost("comments")]
    public async Task<IActionResult> Create([FromBody] CommentRequest request)
    {
        var comment = request.ToComment();
        await _commentService.CreateAsync(comment);
        var commentResponse = comment.ToCommentResponse();

        return CreatedAtAction("Get", new { commentResponse.Id }, commentResponse);
    }

    [HttpGet("comments/{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var comment = await _commentService.GetAsync(id);
        if (comment is null)
        {
            return NotFound();
        }

        var commentResponse = comment.ToCommentResponse();

        return Ok(commentResponse);
    }

    [HttpGet("comments")]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentService.GetAllAsync();
        var commentsResponse = comments.ToCommentsResponse();

        return Ok(commentsResponse);
    }

    /*    [HttpPut("posts/{id:guid}")]
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
    */

    [HttpDelete("comments/{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _commentService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }

}
