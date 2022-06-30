using LoopMainProject.Host;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

#region Services

builder.Services.InjectSieve();
builder.Services.InjectContext(builder.Configuration);
builder.Services.InjectUnitOfWork();
builder.Services.InjectUserService();
builder.Services.InjectPostService();
builder.Services.InjectVotingService();

#endregion

#region HealthCheck

builder.Services.AddHealthChecks();

#endregion

#region Cookies

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/User/Login";
    options.LogoutPath = "/User/LogOut";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Testing"))
    {
        endpoints.MapHealthChecks("/Health");
    }
});



app.Run();


public partial class Program { }
