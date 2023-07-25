using BasicAuthenticationWebApi.Filters;
using BasicAuthenticationWebApi.Services;
using Microsoft.AspNetCore.Authentication;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserService, UserService>();;

builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services
	.AddAuthentication("BasicAuthentication")
	.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var app = builder.Build();
app.UseRouting();


app.UseCors(x => x
	.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
