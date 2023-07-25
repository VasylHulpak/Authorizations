using BasicAuthenticationWebApi.Models;
using BasicAuthenticationWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthenticationWebApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[AllowAnonymous]
		[HttpPost("authenticate")]
		public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
		{
			var user = await _userService.Authenticate(model.Username, model.Password);

			return Ok(user);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var users = await _userService.GetAll();
			return Ok(users);
		}
	}
}
