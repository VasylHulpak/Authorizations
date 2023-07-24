namespace JWT.Context;

public interface IDbContext
{
	IDictionary<string, string> Users { get; set; }
}
