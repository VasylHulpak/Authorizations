using System.Net.Http.Headers;
using System.Text.Json;
using Auth2.Options;
using Microsoft.AspNetCore.Authentication.OAuth;
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

var instagramOptions = builder.Configuration.GetSection("Instagram");
var githubOptions = builder.Configuration.GetSection("Github");
var linkedinOptions = builder.Configuration.GetSection("Linkedin");

builder.Services.Configure<InstagramOptions>(instagramOptions);
builder.Services.Configure<GithubOptions>(githubOptions);
builder.Services.Configure<LinkedinOptions>(linkedinOptions);

auth.AddGitHub("github", options =>
{
	options.ClientId = githubOptions.GetValue<string>("ClientId")!;
	options.ClientSecret = githubOptions.GetValue<string>("ClientSecret")!;
	options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
	options.TokenEndpoint = "https://github.com/login/oauth/access_token";
	options.UserInformationEndpoint = "https://api.github.com/user";
	options.CallbackPath = githubOptions.GetValue<string>("Callback");
	
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
	options.ClientId = instagramOptions.GetValue<string>("ClientId")!;
	options.ClientSecret = instagramOptions.GetValue<string>("ClientSecret")!;
	options.CallbackPath = instagramOptions.GetValue<string>("Callback");
});

auth.AddInstagram("linkedIn", options =>
{
	options.ClientId = linkedinOptions.GetValue<string>("ClientId")!;
	options.ClientSecret = linkedinOptions.GetValue<string>("ClientSecret")!;
	options.CallbackPath = linkedinOptions.GetValue<string>("Callback");
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
