using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopApp.Core.Models.User;
using ShopApp.Core.Repositories;
using ShopApp.Core.Services;
using ShopApp.WebApi.Data;
using ShopApp.WebApi.Repositories;
using ShopApp.WebApi.Services;
using ShopApp.WebApi.Services.JwtAuth;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// DATABASE
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// DEPENDENCIES
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
// JWT
builder.Services.AddSingleton<JwtService>();
builder.Services.AddScoped<IPasswordHasher<AuthUser>, PasswordHasher<AuthUser>>();
builder.Services.AddScoped<IAuthService, JwtAuthService>();
// REPOS
builder.Services.AddScoped<IUserRepository, UserRepository>();

// AUTHENTICATION
string jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
             ?? builder.Configuration["Jwt:Key"]!;
builder.Services.Configure<JwtSettings>(options =>
{
    builder.Configuration.Bind("Jwt", options);
    options.Key = jwtKey;
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
// AUTHORIZATION
builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        _ = policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// CONTROLLERS
builder.Services.AddControllers();
// ENDPOINTS
builder.Services.AddEndpointsApiExplorer();
// SWAGGER
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// APPLY MIGRATIONS AND SWAGGER IN DEVELOPMENT
if (app.Environment.IsDevelopment())
{
    using (IServiceScope scope = app.Services.CreateScope())
    {
        ApplicationDbContext db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
    }

    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
