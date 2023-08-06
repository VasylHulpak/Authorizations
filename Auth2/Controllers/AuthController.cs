using Microsoft.AspNetCore.Authentication;
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
				return Challenge(new AuthenticationProperties
				{
					RedirectUri = "/"
				}, schema);
			}
			return new ChallengeResult();
		}

		[HttpGet]
		[Route("LoggedIn")]
		public string LoggedIn()
		{
			return "Logged in with" + User.Identity.AuthenticationType;
		}
	}
}
