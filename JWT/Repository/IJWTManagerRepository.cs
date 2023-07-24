using JWT.Models;

namespace JWT.Repository;

public interface IJwtManagerRepository
{
	TokenModel? Authenticate(Users users);
}
