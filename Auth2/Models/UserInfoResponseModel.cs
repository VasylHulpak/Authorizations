using Newtonsoft.Json;

namespace Auth2.Models
{
	public class UserInfoResponseModel
	{
		[JsonProperty(PropertyName = "sub")]
		public string Sub { get; set; } = null!;

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; } = null!;

		[JsonProperty(PropertyName = "given_name")]
		public string GivenName { get; set; } = null!;

		[JsonProperty(PropertyName = "family_name")]
		public string FamilyName { get; set; } = null!;

		[JsonProperty(PropertyName = "created")]
		public DateTime Created { get; set; }

		[JsonProperty(PropertyName = "email")]
		public string Email { get; set; } = null!;

		[JsonProperty(PropertyName = "accounts")]
		public Account[] Accounts { get; set; } = null!;
	}
	public class Account
	{
		[JsonProperty(PropertyName = "account_id")]
		public string AccountId { get; set; } = null!;

		[JsonProperty(PropertyName = "is_default")]
		public bool IsDefault { get; set; }

		[JsonProperty(PropertyName = "account_name")]
		public string AccountName { get; set; } = null!;

		[JsonProperty(PropertyName = "base_uri")]
		public string BaseUri { get; set; } = null!;
	}
}
