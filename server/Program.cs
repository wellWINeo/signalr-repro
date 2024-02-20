using Microsoft.AspNetCore.Authentication.JwtBearer;
using server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = (context) =>
            {
                var accessToken = context.Request.Headers.Authorization.ToString().Split(" ").LastOrDefault()
                    ?? context.Request.Query["bearer"].FirstOrDefault()
                    ?? context.Request.Query["access_token"].FirstOrDefault()
                    ?? context.Request.Cookies["bearer"]
                    ;

                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<PingHub>("/hub");

app.Run();