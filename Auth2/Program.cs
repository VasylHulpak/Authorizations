using System.Net;
using System.Net.Http.Headers;
using Auth2.Helpers;
using Auth2.Options;
using Auth2.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var auth = builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = IdentityConstants.ApplicationScheme;
	options.DefaultChallengeScheme = "github";
});

auth.AddCookie(IdentityConstants.ApplicationScheme, opt =>
{
	opt.LoginPath = "/api/auth/logIn";
	opt.
});
auth.AddGitHub("github", options =>
{
	options.ClientId = "80b6dfafa4828f62176e";
	options.ClientSecret = "797323350210f1350f991e4e3363118ecde012e0";
	options.Events.OnCreatingTicket += context =>
	{
		return Task.CompletedTask;
	};
});

builder.Services.AddScoped<IGithubService, GithubService>();
builder.Services.AddScoped<IPleaseSignService, PleaseSignService>();
builder.Services.AddScoped<ILinkedinService, LinkedinService>();
builder.Services.AddScoped<IInstagramService, InstagramService>();

// Options
var pleaseSignOptions = builder.Configuration.GetSection("PleaseSign");
var linkedinOptions = builder.Configuration.GetSection("Linkedin");
var instagramOptions = builder.Configuration.GetSection("Instagram");

builder.Services.Configure<PleaseSignOptions>(pleaseSignOptions);
builder.Services.Configure<LinkedInOptions>(linkedinOptions);
builder.Services.Configure<InstagramOptions>(instagramOptions);

//Http clients
builder.Services.AddPleaseSignHttpClient(pleaseSignOptions);
builder.Services.AddInstagramHttpClient(instagramOptions);
builder.Services.AddLinkedInHttpClient(linkedinOptions);

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
