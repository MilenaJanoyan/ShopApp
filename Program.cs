using System.Text;
using ShopApp.Data;
using ShopApp.Settings;
using ShopApp.Middleware;
using ShopApp.Extensions;
using ShopApp.Users.Repository;
using ShopApp.Products.Service;
using ShopApp.Products.Repository;
using ShopApp.Authorization.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopApp.Authorization.Repository;
using ShopApp.Products.Service.Interface;
using ShopApp.Users.Repository.Interface;
using ShopApp.Products.Repository.Interface;
using ShopApp.Authorization.Service.Interfaces;
using ShopApp.Authorization.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.IgnoreNullValues = true;
    });

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });

    services.AddAuthorization();

    services.AddDbContext<ShopDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

    services.AddScoped<ITokenRepository, TokenRepository>();
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<IPasswordService, PasswordService>();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddSwaggerDocumentation();
}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "MIMODA Shop API V1");
            c.RoutePrefix = string.Empty;
        });
    }
    else
    {
        app.UseExceptionHandler("/Error");
    }

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();
}
