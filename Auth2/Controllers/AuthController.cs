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
			return Challenge(new AuthenticationProperties
			{
				RedirectUri = "/api/auth/response",
			}, schema);
		}

		[HttpGet]
		[Authorize]
		[Route("github-response")]
		public async Task<IActionResult> GithubResponse()
		{
			// var result = await HttpContext.AuthenticateAsync("github");
 		//
			// var claims = result.Principal.Identities
			// 	.FirstOrDefault().Claims.Select(claim => new
			// 	{
			// 		claim.Issuer,
			// 		claim.OriginalIssuer,
			// 		claim.Type,
			// 		claim.Value
			// 	});
 		//
			return Redirect("/api/auth/response");
		}

		[HttpGet]
		[Authorize]
		[Route("response")]
		public string response()
		{
			// var result = await HttpContext.AuthenticateAsync("github");
			//
			// var claims = result.Principal.Identities
			// 	.FirstOrDefault().Claims.Select(claim => new
			// 	{
			// 		claim.Issuer,
			// 		claim.OriginalIssuer,
			// 		claim.Type,
			// 		claim.Value
			// 	});
			//
			return "User was authorized with " + HttpContext.User.Identity.AuthenticationType + " schema";
		}
		
		
		[HttpGet]
		[Route("callback-pleaseSign")]
		public string CallbackPleaseSign()
		{
			var r = HttpContext.User;
			return "Please authorize";
		}
		
		
		[HttpGet]
		[Route("LoggedIn")]
		public string LoggedIn()
		{
			return "Please authorize";
		}
	}

}
