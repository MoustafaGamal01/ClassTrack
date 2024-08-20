using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ClassTrack.DataAccessLayer.Models.Context;
using ClassTrack.DataAccessLayer.Models;
using ClassTrack.DataAccessLayer.Repositories.AttendenceRepos;
using ClassTrack.DataAccessLayer.Repositories.IRepository.IAttendenceRepos;
using ClassTrack.DataAccessLayer.Repositories.IRepository;
using ClassTrack.DataAccessLayer.Repositories;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline
        Configure(app, app.Environment);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

        services.AddEndpointsApiExplorer();

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IAttendExcuseRepository, AttendExcuseRepository>();
        services.AddScoped<IAttendenceRepository, AttendenceRepository>();

        services.AddDbContext<MyContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("RemoteCS"));
        });

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<MyContext>()
            .AddDefaultTokenProviders();

        var jwtSettings = configuration.GetSection("Jwt").Get<JwtOptions>();
        services.AddSingleton(jwtSettings);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
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

        services.AddCors(corsOptions =>
        {
            corsOptions.AddPolicy("MyPolicy", corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeladSanad API", Version = "v1" });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "JWT Authorization header using the Bearer scheme.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            };
            c.AddSecurityDefinition("Bearer", securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
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
            };
            c.AddSecurityRequirement(securityRequirement);
        });
    }

    private static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeladSanad API v1");
                c.OAuthClientId("swagger-client-id");
                c.OAuthClientSecret(""); 
                c.OAuthAppName("Swagger UI");
            });
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        
        app.UseHttpsRedirection();
        app.UseCors("MyPolicy");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}
