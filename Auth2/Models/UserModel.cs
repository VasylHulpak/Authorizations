namespace Auth2.Models
{
	public class UserModel
	{
		public Guid Id { get; set; }
	
		public DateTime ExpiresAt { get; set; }
	
		public string RefreshToken { get; set; } = null!;
	}
}
