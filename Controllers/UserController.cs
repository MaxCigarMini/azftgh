using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZAMETKI_FINAL.Abstraction;
using ZAMETKI_FINAL.Dto_Vm;
using ZAMETKI_FINAL.Model;
using ZAMETKI_FINAL.Services;

namespace ZAMETKI_FINAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userService;

        public UserController(IUserInterface userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<int> AddUser([FromBody] UserDto userDto)
        {
            var user = _userService.CreateUser(userDto.UserName.Trim(), userDto.Password.Trim());
            return Ok(user.UserId);
        }
    }
}
