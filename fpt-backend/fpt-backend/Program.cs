using System.Security.Claims;
using System.Text.Json;
using fpt_backend.Data;
using fpt_backend.Helper_classes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddServices();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy("react", policy =>
    {
        policy
            .WithOrigins("https://localhost:3000") // your React URL
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


builder.Services.AddDbContext<FptDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConString"))); //temp connection string

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:8080/realms/BennettApps";
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = "amino-backend",
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:8080/realms/BennettApps",
            ValidateLifetime = true,
            RoleClaimType = ClaimTypes.Role
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var identity = (ClaimsIdentity)context.Principal.Identity;

                var resourceAccess = identity.FindFirst("resource_access")?.Value;

                if (!string.IsNullOrEmpty(resourceAccess))
                {
                    using var doc = JsonDocument.Parse(resourceAccess);

                    if (doc.RootElement.TryGetProperty("amino-frontend", out var client))
                    {
                        if (client.TryGetProperty("roles", out var roles))
                        {
                            foreach (var role in roles.EnumerateArray())
                            {
                                identity.AddClaim(
                                    new Claim(ClaimTypes.Role, role.GetString())
                                );
                            }
                        }
                    }
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseRouting();
app.UseCors("react");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
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