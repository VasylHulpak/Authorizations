namespace Auth2.Models
{
	public class TokenRequestModel
	{
		public string Code { get; set; } = null!;
		public string State { get; set; } = null!;
	}
}
