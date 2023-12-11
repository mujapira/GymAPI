using Microsoft.EntityFrameworkCore;
using WorkoutAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));
builder.Services.AddDbContext<ExerciseContext>(opt => opt.UseSqlServer
("Data Source=localhost; Initial Catalog=Developer; User Id=sa; Password=bomBEIRO!@#; TrustServerCertificate=True"));
builder.Services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; options.LowercaseQueryStrings = true; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseRouting();

app.Run();