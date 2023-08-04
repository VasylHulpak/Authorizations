using System.Net.Http.Headers;
using Auth2.Helpers;
using Auth2.Models;
using Auth2.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Auth2.Services
{
	public class PleaseSignService : IPleaseSignService
	{
		private readonly IHttpClientFactory _clientFactory;
		private readonly ILogger<PleaseSignService> _logger;
		private readonly IOptionsSnapshot<PleaseSignOptions> _optionsSnapshot;

		public PleaseSignService(IHttpClientFactory clientFactory, ILogger<PleaseSignService> logger, IOptionsSnapshot<PleaseSignOptions> optionsSnapshot)
		{
			_clientFactory = clientFactory;
			_logger = logger;
			_optionsSnapshot = optionsSnapshot;
		}

		public Task<string> GenerateOAuthRequestUrl(Guid userId)
		{
			var oAuthServerEndpoint = _optionsSnapshot.Value.Domain + "/oauth/authorize";
			
			// PCKE.
			var codeVerifier = Guid.NewGuid().ToString();

			// IMPORTANT: must be alphabetical order.
			var queryParams = new Dictionary<string, string>
			{
				{ "client_id", _optionsSnapshot.Value.ClientId },
				{ "client_name", "E-Signature App" },
				{ "code_challenge", Base64Helper.ComputeHash(codeVerifier) },
				{ "code_challenge_method", "S256" },
				{ "redirect_uri", _optionsSnapshot.Value.RedirectUri },
				{ "response_mode", "query" },
				{ "response_type", "code" },
				{ "scope", "business" },
				{ "state", Base64Helper.ComputeHash(userId.ToString()) }
			};

			var url = QueryHelpers.AddQueryString(oAuthServerEndpoint, queryParams!);
			return Task.FromResult(url);
		}

		public async Task<AuthResponseModel> AuthenticatedAsync(Guid userId)
		{
			var user = new UserModel()
			{
				ExpiresAt = DateTime.Today
			};
			var isAuthenticated = user.ExpiresAt > DateTime.UtcNow;

			if (!isAuthenticated)
			{
				user = await RefreshTokenAsync(user.Id);
				isAuthenticated = user.ExpiresAt > DateTime.UtcNow;
			}

			return new AuthResponseModel
			{
				IsAuthenticated = isAuthenticated
			};
		}

		public async Task<AuthResponseModel> RequestTokenAsync(Guid userId, TokenRequestModel tokenRequestModel)
		{
			using var client = _clientFactory.CreateClient("pleaseSign");
			var parameters = new Dictionary<string, string>
			{
				{ "client_id", _optionsSnapshot.Value.ClientId },
				{ "code", tokenRequestModel.Code },
				{ "code_verifier", Base64Helper.ComputeHash(userId.ToString()) },
				{ "grant_type", "authorization_code" },
				{ "redirect_uri", _optionsSnapshot.Value.RedirectUri }
			};

			var response = await client.PostAsync(new Uri("/oauth/token", UriKind.Relative), new FormUrlEncodedContent(parameters));

			var responseString = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				_logger.LogError("Error PleaseSign getting token : {Response}", responseString);
				throw new Exception($"Error PleaseSign getting token {responseString}");
			}

			var tokens = JsonConvert.DeserializeObject<TokenResponse>(responseString);

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

			var userInfoResponse = await client.GetAsync(new Uri("/oauth/userinfo", UriKind.Relative));
			var userInfoResponseString = await userInfoResponse.Content.ReadAsStringAsync();

			if (!userInfoResponse.IsSuccessStatusCode)
			{
				_logger.LogError("Error getting userinfo : {Response}", userInfoResponseString);
				throw new Exception($"Error getting userinfo {userInfoResponseString}");
			}

			return await AuthenticatedAsync(userId);
		}

		public async Task<UserModel> RefreshTokenAsync(Guid userId)
		{
			// Need to get user from database
			var user = new UserModel()
			{

			};

			var parameters = new List<KeyValuePair<string, string>>
			{
				new ("grant_type", "refresh_token"),
				new ("refresh_token", user.RefreshToken)
			};

			using var client = _clientFactory.CreateClient("pleaseSign");
			var response = await client.PostAsync(new Uri("/oauth/token", UriKind.Relative), new FormUrlEncodedContent(parameters));

			var responseString = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				_logger.LogError("Error PleaseSign getting token : {Response}", responseString);
				throw new Exception($"Error PleaseSign getting token {responseString}");
			}

			var tokens = JsonConvert.DeserializeObject<TokenResponse>(responseString);

			// need to save data
			return user;
		}
	}
}
