using Microsoft.EntityFrameworkCore;
using ShopApp.WebApi.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

WebApplication app = builder.Build();

// Apply migrations automatically in development
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
