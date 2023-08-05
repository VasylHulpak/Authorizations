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
	opt.Cookie.Name = "CookieESignature";
});

auth.AddGitHub("github", options =>
{
	options.ClientId = "80b6dfafa4828f62176e";
	options.ClientSecret = "797323350210f1350f991e4e3363118ecde012e0";
	
	options.Scope.Add("user:email");
	options.CallbackPath = "/api/Auth/LoggedIn";
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
	options.CallbackPath = "/api/Auth/LoggedIn";
});

auth.AddOAuth("pleaseSign", options =>
{
	options.ClientId = "";
	options.ClientSecret = "";
	
	options.Scope.Add("business");
	
	options.AuthorizationEndpoint = "https://private.pleasesign.com.au/oauth/authorize";
	options.TokenEndpoint = "https://private.pleasesign.com.au/oauth/token";
	options.CallbackPath = "/api/Auth/LoggedIn";
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
