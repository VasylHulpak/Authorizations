using Auth2.Helpers;
using Auth2.Options;
using Auth2.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");
app.Run();
