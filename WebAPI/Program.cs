using Microsoft.EntityFrameworkCore;
using WebAPI.ApiService;
using WebAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Service>();
builder.Services.AddControllers().AddNewtonsoftJson();

var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conString));

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var dbCon = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbCon.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();