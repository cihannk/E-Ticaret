using ETicaretWebApi.Common;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Middlewares;
using ETicaretWebApi.Services;
using ETicaretWebApi.Services.JWT;
using ETicaretWebApi.Services.Payment;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Reflection;
using System.Text;

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
builder.Services.AddSingleton<ITokenOperations, JwtOperations>();
builder.Services.AddScoped<IPaymentService, StripePaymentService>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                      });
});

var keyBytes = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSecret"));
var securityKey = new SymmetricSecurityKey(keyBytes);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opts =>
{
    opts.SaveToken = true;
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = "ETicaretApp",
        ValidAudience = "ETicaretApp",
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = securityKey
    };
});

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe").GetValue<string>("SecretKey");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//custom

app.UseCustomExceptionMiddleware();

app.Run();
