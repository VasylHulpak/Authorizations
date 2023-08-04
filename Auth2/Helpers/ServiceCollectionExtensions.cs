using System.Net.Http.Headers;
using System.Text;

namespace Auth2.Helpers
{
	public static class ServiceCollectionExtensions
	{
		public static void AddPleaseSignHttpClient(this IServiceCollection services, IConfigurationSection options)
		{
			services.AddHttpClient("pleaseSign", client =>
			{
				client.BaseAddress = new Uri(options.GetValue<string>("TokenAPI"), UriKind.Absolute);

				client.DefaultRequestHeaders.Add("X-PLEASESIGN-KEY", options.GetValue<string>("ClientId"));
				client.DefaultRequestHeaders.Add("X-PLEASESIGN-SECRET", options.GetValue<string>("ClientSecret"));

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Base64Encode($"{options.GetValue<string>("ClientId")}:{options.GetValue<string>("ClientSecret")}"));
			});
		}

		static string Base64Encode(string plainText)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(plainTextBytes);
		}
	}
}
