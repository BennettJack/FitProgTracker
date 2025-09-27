using System.Security.Claims;
using fpt_backend.Controllers.GymControllers;
using fpt_backend.Data;
using fpt_backend.DbRepositories.GymRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:3000") // React dev server
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); // must allow credentials for cookies
        });
});


builder.Services.AddDbContext<FptDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConString"))); //temp connection string
builder.Services.AddScoped<EquipmentService>();
builder.Services.AddScoped<EquipmentRepository>();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"/var/keys/dataprotection"))
    .SetApplicationName("bennettj.SSO");

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.Name = ".bennettj.Sso";
    options.Cookie.Domain = "localhost"; // if apps share parent domain
    options.Cookie.SameSite = SameSiteMode.None;
#if DEBUG
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
#else
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
#endif
})
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Authority = builder.Configuration["Authentication:OIDC:Authority"];
    options.ClientId = builder.Configuration["Authentication:OIDC:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:OIDC:ClientSecret"];
    options.CallbackPath = "/signin-oidc"; 
    options.ResponseType = "code";
    options.UsePkce = true; // optional - safe to use
    options.SaveTokens = true; // keep id/access/refresh tokens in auth properties
    options.GetClaimsFromUserInfoEndpoint = true;

    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
    options.Scope.Add("groups");
    options.Scope.Add("offline_access"); // if you want refresh tokens
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "preferred_username",
        RoleClaimType = "groups"
    };

    options.Events = new OpenIdConnectEvents
    {
        OnTokenValidated = context =>
        {
            Console.WriteLine("OnTokenValidated");
            var identity = context.Principal.Identity as ClaimsIdentity;

            // Grab the groups claim
            var groupsClaim = identity.FindAll("groups");

            if (groupsClaim.Any())
            {
                // Assuming groupsClaim is a JSON array, parse it
                var claims = groupsClaim.Select(x => new Claim(x.Type, x.Value)).ToList();

                foreach (var group in claims)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, group.Value));
                }
            }

            return Task.CompletedTask;
        },
        OnRedirectToIdentityProviderForSignOut = ctx =>
        {
            // send id_token_hint if available
            var idToken = ctx.HttpContext.User.FindFirst("id_token")?.Value;
            if (!string.IsNullOrEmpty(idToken))
            {
                ctx.ProtocolMessage.IdTokenHint = idToken;
            }
            return Task.CompletedTask;
        },
        OnTicketReceived = ctx =>
        {
        // After successful OIDC login, redirect to frontend
        Console.WriteLine("test thing");
        ctx.ReturnUri = "https://localhost:3000";
        return Task.CompletedTask;
    }
    };
});


var app = builder.Build();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
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

app.Run();