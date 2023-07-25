namespace BasicAuthenticationWebApi.Models;

public class UserBL
{
	public List<User> GetUsers()
	{
		return new List<User>()
		{
			new ()
			{
				Id = 101,
				UserName = "MaleUser",
				Password = "123456"
			},
			new ()
			{
				Id = 101,
				UserName = "FemaleUser",
				Password = "abcdef"
			}
		};
	}
}
