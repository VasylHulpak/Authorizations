namespace BasicAuthenticationWebApi.Models;

public class UserValidate
{
	public static bool Login(string username, string password)
	{
		UserBL userBL = new UserBL();
		var UserLists = userBL.GetUsers();
		return UserLists.Any(user =>
			user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
			&& user.Password == password);
	}
}
