using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth2.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class AuthController : ControllerBase
	{
		[HttpGet]
		[Route("LogInWith")]
		public ActionResult LogInWith(string schema)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return Challenge(new AuthenticationProperties()
				{
					RedirectUri = "/api/Auth/LoggedIn"
				}, schema);
			}
			return new ChallengeResult();
		}

		[HttpGet]
		[Authorize()]
		[Route("LoggedIn")]
		public string LoggedIn()
		{
			return "Logged in";
		}

		[HttpGet]
		[Route("callback")]
		public string callback()
		{
			return "Logged in";
		}
	}
}
