using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        //Jwt
        private readonly JwtService _jwtService;

        public UserController(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            //Jwt
            _jwtService = jwtService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get() =>
            _userService.Get();

        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);
            if (user == null) return NotFound();
            return user;
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            var createdUser = _userService.Create(user);

            //JWT
            var token = _jwtService.GenerateToken(createdUser.UserName,createdUser.Id);
            user.Token = token;

            _userService.UpdateToken(createdUser.Id, token);

            return Ok(new
            {
                //JWT
                token = token,
                user = createdUser,

            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.GetByUserName(request.UserName);

            if (user == null || user.Password != request.Password)
            {
            return Unauthorized(new { message = "帳號或密碼錯誤" });
            }

            // 回傳資料庫中現有的 token
            return Ok(new
            {
              token = user.Token,
             user = new { user.Id, user.UserName }
            });
        }

          

       
    }
}
