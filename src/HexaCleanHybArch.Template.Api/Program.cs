using Adapters.Auth;
using Adapters.User;
using Asp.Versioning;
using FluentValidation;
using HealthChecks.UI.Client;
using HexaCleanHybArch.Template.Api.Middleware;
using HexaCleanHybArch.Template.Api.Validator.Bases;
using HexaCleanHybArch.Template.Config.DbContext;
using HexaCleanHybArch.Template.Config.Factory;
using HexaCleanHybArch.Template.Config.Loader;
using HexaCleanHybArch.Template.Core.Interfaces.Auth;
using HexaCleanHybArch.Template.Core.Interfaces.Users;
using HexaCleanHybArch.Template.Core.Mapping;
using HexaCleanHybArch.Template.Core.Services.Auth;
using HexaCleanHybArch.Template.Core.Services.Users;
using HexaCleanHybArch.Template.Infra.Interfaces;
using HexaCleanHybArch.Template.Infra.Logging;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
// Logs
ConfigurationManager? config = builder.Configuration;
Log.Logger = SerilogConfigurator.Configure();

builder.Services.AddSingleton<ILoggerService>(new SerilogLoggerService(Log.Logger));

builder.Services.AddHealthChecks();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Clean Architecture RESTful API",
        Version = "v1",
        Description = "Clean Architecture RESTful API Doc."
    });

    // Add JWT Authentication config
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.\n\nExample: **Bearer eyJhbGciOi...**"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// Load Adapter
ApapterModuleLoader.RegisterAdapter(
    builder.Services,
    builder.Configuration,
    [
        typeof(AdapterUser).Assembly,
        typeof(AdapterAuth).Assembly,
        ]
    );

// Register database
DatabaseFactory.RegisterDatabase(builder.Services, builder.Configuration);

builder.Services.AddControllers();
// Register Automappper
builder.Services.AddAutoMapper(typeof(CoreMappingProfile));
// Register JWT
builder.Services.AddScoped<IJwtAuthService, JwtAuthService>();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:AccessKey"]))
        };
    });

// Register Core
builder.Services.AddScoped<IUserServices, UserService>();

// Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<BaseValidator<object>>();

// API version
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
}).AddMvc()
.AddApiExplorer(options => {
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});

// Rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", cancellationToken: token);
    };

    options.AddPolicy("PerIpPolicy", context =>
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 100,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0
        });
    });
});

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    AppDbContext? dbContext = scope.ServiceProvider.GetService<AppDbContext>();

    if (dbContext != null)
    {
        dbContext.Database.EnsureCreated();
    }
}

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Architecture RESTful API");
        c.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }