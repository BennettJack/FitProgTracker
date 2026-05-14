using System.Security.Claims;
using System.Text.Json;
using fpt_backend.Data;
using fpt_backend.Helper_classes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddServices();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "react",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

builder.Services.AddDbContext<FptDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConString"))
);

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:8080/realms/BennettjApps";
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:8080/realms/BennettjApps",

            ValidateAudience = true,
            ValidAudience = "amino-backend",

            ValidateLifetime = true,

            NameClaimType = "preferred_username",

            RoleClaimType = ClaimTypes.Role,
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var identity = context.Principal?.Identity as ClaimsIdentity;
                var principal = context.Principal;

                if (identity == null || principal == null)
                    return Task.CompletedTask;

                var realmAccess = principal.FindFirst("realm_access")?.Value;

                if (string.IsNullOrEmpty(realmAccess))
                    return Task.CompletedTask;
                using var doc = JsonDocument.Parse(realmAccess);

                if (!doc.RootElement.TryGetProperty("roles", out var roles))
                    return Task.CompletedTask;
                foreach (
                    var roleValue in roles
                        .EnumerateArray()
                        .Select(role => role.GetString())
                        .Where(roleValue => !string.IsNullOrEmpty(roleValue))
                )
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, roleValue));
                }

                return Task.CompletedTask;
            },
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("react");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FptDbContext>();
    FiveThreeOneSeeder.Seed(context);
}

app.Run();
