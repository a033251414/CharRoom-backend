using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class MessagesController : ControllerBase
  {
    private readonly MessageService _messageService;

    public MessagesController(MessageService messageService)
    {
      _messageService = messageService;
    }

    [HttpGet("{groupId}")]
    public async Task<IActionResult> GetMessagesByGroupId(string groupId)
    {
      var messages = await _messageService.GetMessagesByGroupIdAsync(groupId);
      return Ok(messages);
    }


    [HttpPost]
    public async Task<IActionResult> CreateMessage([FromBody] Message message)
    {

      if (message == null)
        return BadRequest("Invalid message");

      Console.WriteLine($"[後端接收資料] GroupId: {message.GroupId}, UserId: {message.UserName}, Content: {message.Content}");
      await _messageService.CreateMessageAsync(message);
      return Ok(message);
    }

    [HttpPost("clear")]
    public async Task<IActionResult> ClearMessage([FromBody] string id)
    {
      if (string.IsNullOrEmpty(id))
        return BadRequest(new { error = "ID is required." });

      var updated = await _messageService.SetMessageToNullAsync(id);
      if (!updated)
        return NotFound(new { error = "Message not found." });

      return Ok(new { success = true, id });
    }

    [HttpPost("searchingroup")]
    public async Task<IActionResult> SearchMessagesInGroup([FromBody] MessageSearchRequest request)
    {
       if (string.IsNullOrWhiteSpace(request.GroupId) || string.IsNullOrWhiteSpace(request.Keyword))
          return BadRequest(new { error = "GroupId 與 Keyword 都必須提供。" });

       var results = await _messageService.SearchMessagesInGroupAsync(request.GroupId, request.Keyword);

       return Ok(results);
    }


    }
}
