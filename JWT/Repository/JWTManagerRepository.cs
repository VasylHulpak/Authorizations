using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWT.Context;
using JWT.Models;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Repository;

public class JwtManagerRepository: IJwtManagerRepository
{
	private readonly IConfiguration _configuration;
	private readonly IDbContext _context;
	
	public JwtManagerRepository(IConfiguration configuration, IDbContext context)
	{
		_configuration = configuration;
		_context = context;
	}
	
	public TokenModel? Authenticate(Users users)
	{
		if (!_context.Users.Any(x => x.Key == users.Name && x.Value == users.Password)) {
			return null;
		}

		// Else we generate JSON Web Token
		var tokenHandler = new JwtSecurityTokenHandler();
		var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new Claim[]
			{
				new Claim(ClaimTypes.Name, users.Name)                    
			}),
			Expires = DateTime.UtcNow.AddMinutes(10),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
		};
		var token = tokenHandler.CreateToken(tokenDescriptor);
		return new TokenModel { Token = tokenHandler.WriteToken(token) };

	}
}
