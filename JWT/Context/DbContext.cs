namespace JWT.Context;

public class DbContext : IDbContext
{
	public IDictionary<string, string> Users { get; set; }

	public DbContext()
	{
		Users = new Dictionary<string, string>()
		{
			{ "user1", "password1" },
			{ "user2", "password2" },
			{ "user3", "password3" },
		};
	}
}
