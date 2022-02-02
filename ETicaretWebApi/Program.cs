using ETicaretWebApi.Common;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Middlewares;
using ETicaretWebApi.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

IConfiguration configuration = builder.Configuration;

// Add services to the container.

//builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
//{
//    builder.WithOrigins("http://localhost:3000")
//           .AllowAnyMethod()
//           .AllowAnyHeader();
//}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ETicaretDbContext>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSingleton<ILoggerService, ConsoleLoggerService>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

//custom
app.UseCustomExceptionMiddleware();

app.Run();
