using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;
using Allfit_Webproject.Server.Repository;
using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http, // verander ApiKey naar Http
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Register ApplicationDbContext with SQL Server (reads connection string from appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS - allow frontend dev server (adjust origin as needed)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Services and Repositories registration
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<ILidService, LidService>();
builder.Services.AddScoped<ILidRepository, LidRepository>();
builder.Services.AddScoped<ISportschoolRepository, SportschoolRepository>();
builder.Services.AddScoped<ISportschoolService, SportschoolService>();
builder.Services.AddScoped<IAanbodRepository, AanbodRepository>();
builder.Services.AddScoped<IAanbodService, AanbodService>();
builder.Services.AddScoped<IAbonnementService, AbonnementService>();
builder.Services.AddScoped<IAbonnementRepository, AbonnementRepository>();
builder.Services.AddScoped<ILesService, LesService>();
builder.Services.AddScoped<ILesRepository, LesRepository>();
builder.Services.AddScoped<IInschrijvingService, InschrijfService>();
builder.Services.AddScoped<IInschrijfRepository, InschrijvingRepository>();
builder.Services.AddScoped<IVoedingsadviesService, VoedingsadviesService>();
builder.Services.AddScoped<IVoedingsadviesRepository, VoedingsadviesRepository>();
builder.Services.AddScoped<ICoachingService, CoachingService>();
builder.Services.AddScoped<ICoachingRepository, CoachingRepository>();

builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<IFormRepo, FormRepo>();
builder.Services.AddScoped<IVerhaalService, VerhaalService>();
builder.Services.AddScoped<IVerhaalRepository, VerhaalRepository>();

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("ContactFormulierPolicy", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 3,
                Window = TimeSpan.FromMinutes(10),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapFallbackToFile("/index.html");



app.Run();

// Expose Program class for integration testing
public partial class Program { }