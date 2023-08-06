using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var auth = builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = IdentityConstants.ApplicationScheme;
	options.DefaultChallengeScheme = "github";
});

auth.AddCookie(IdentityConstants.ApplicationScheme, opt =>
{
	opt.LoginPath = "/api/Auth/LoggedIn";
});

auth.AddGitHub("github", options =>
{
	options.CallbackPath = "/api/Auth/LoggedIn";
	options.ClientId = "80b6dfafa4828f62176e";
	options.ClientSecret = "f3e3b2db60418cad049df8484d75bcb911b32058";
	options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
	options.TokenEndpoint = "https://github.com/login/oauth/access_tokenn";
	options.UserInformationEndpoint = "https://api.github.com/user";
	
	options.Scope.Add("user:email");
	options.SaveTokens = true;
	
	options.Events = new OAuthEvents
	{
		OnCreatingTicket = async context =>
		{
			var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
			var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
			response.EnsureSuccessStatusCode();
			var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
			context.RunClaimActions(json.RootElement);
		}
	};
});

auth.AddInstagram("instagram", options =>
{
	options.ClientId = "77eglcyglemv63";
	options.ClientSecret = "6QKJECcDLxYKoH6Y";
});

auth.AddInstagram("linkedIn", options =>
{
	options.ClientId = "77eglcyglemv63";
	options.ClientSecret = "6QKJECcDLxYKoH6Y";
});

auth.AddOAuth("docuSign", options =>
{
	options.ClientId = "";
	options.ClientSecret = "";
	
	options.AuthorizationEndpoint = "https://account-d.docusign.com/oauth/auth";
	options.TokenEndpoint = "https://account-d.docusign.com/oauth/token";
});

auth.AddOAuth("pleaseSign", options =>
{
	options.ClientId = "";
	options.ClientSecret = "";
	
	options.Scope.Add("business");
	
	options.AuthorizationEndpoint = "https://private.pleasesign.com.au/oauth/authorize";
	options.TokenEndpoint = "https://private.pleasesign.com.au/oauth/token";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");
app.Run();
