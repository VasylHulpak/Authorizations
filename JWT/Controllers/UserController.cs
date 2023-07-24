using JWT.Models;
using JWT.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers;

// [Authorize]
[Route("api/Users")]
[ApiController]  
public class UsersController : ControllerBase
{
	private readonly IJwtManagerRepository _jWtManager;

	public UsersController(IJwtManagerRepository jWtManager)
	{
		_jWtManager = jWtManager;
	}

	[HttpGet]
	[Authorize]
	public List<string> Get()
	{
		var users = new List<string>
		{
			"Satinder Singh",
			"Amit Sarna",
			"Davin Jon"
		};

		return users;
	}

	[AllowAnonymous]
	[HttpPost]
	[Route("authenticate")]
	public IActionResult Authenticate(Users model)
	{
		var token = _jWtManager.Authenticate(model);

		if (token == null)
		{
			return Unauthorized();
		}

		return Ok(token);
	}
}
