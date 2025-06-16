using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;


namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly GroupService _groupService;

        public GroupController(GroupService groupService)
        {
            _groupService = groupService;
        }

        public class CreateGroupRequest
        {
            public string GroupName { get; set; } = string.Empty;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var groups = await _groupService.GetAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var group = await _groupService.GetAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateGroupRequest request)
        {
            var newGroup = new Group(request.GroupName)
            {
                GroupName = request.GroupName,
                Message = new List<string>()
            };

            await _groupService.CreateAsync(newGroup);

            return CreatedAtAction(nameof(Post), new { id = newGroup.Id }, newGroup);
        }


    }
}
