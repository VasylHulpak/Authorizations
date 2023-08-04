using System.ComponentModel.DataAnnotations;

namespace BasicAuthenticationWebApi.Models
{
	public class AuthenticateModel
	{
		[Required]
		public string Username { get; set; } = null!;

		[Required]
		public string Password { get; set; } = null!;
	}
}
