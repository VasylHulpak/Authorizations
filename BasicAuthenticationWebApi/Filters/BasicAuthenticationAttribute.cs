using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using BasicAuthenticationWebApi.Models;
using BasicAuthenticationWebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace BasicAuthenticationWebApi.Filters
{
	public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		private readonly IUserService _userService;

		public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserService userService) : base(options, logger, encoder, clock)
		{
			_userService = userService;
		}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			var endpoint = Context.GetEndpoint();
			
			if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
				return AuthenticateResult.NoResult();

			if (!Request.Headers.ContainsKey("Authorization"))
				return AuthenticateResult.Fail("Missing Authorization Header");

			User? user = null;
			try
			{
				var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
				var credentialBytes = Convert.FromBase64String(authHeader.Parameter!);
				var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
				var username = credentials[0];
				var password = credentials[1];
				user = await _userService.Authenticate(username, password);
			}
			catch
			{
				return AuthenticateResult.Fail("Invalid Authorization Header");
			}

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.Username),
			};
			var identity = new ClaimsIdentity(claims, Scheme.Name);
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, Scheme.Name);

			return AuthenticateResult.Success(ticket);
		}
	}
}
