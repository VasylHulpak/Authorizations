using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth2.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class AuthController: ControllerBase
	{
		[HttpGet]
		[Route("LogInWith")]
		public ActionResult LogInWith(string schema)
		{
			return Challenge(new OAuthChallengeProperties()
			{
				RedirectUri = "/api/Auth/LoggedIn"
			}, schema);
		}
		
		[HttpGet]
		[Authorize()]
		[Route("LoggedIn")]
		public string LoggedIn()
		{
			return "Logged in";
		}
	}
}
