using BackEndTest.Data;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BackEndTest.Services
{
    public class ServiceFacade
    {
        private readonly AuthOptions _options;
        private WebApplication _webApplication;
        public ServiceFacade(WebApplicationBuilder webAppBuilder)
        {
            _options = new AuthOptions();


            Configure(webAppBuilder);
            _webApplication = webAppBuilder.Build();

            if (_webApplication.Environment.IsDevelopment())
            {
                _webApplication.UseSwagger();
                _webApplication.UseSwaggerUI();
            }

            _webApplication.UseHttpsRedirection();

            _webApplication.UseAuthentication();
            _webApplication.UseAuthorization();

            _webApplication.MapControllers();
        }

        public void Run()
        {
            _webApplication.Run();
        }

        public void Configure(WebApplicationBuilder builder)
        {
            ConfigureDbContext(builder);
            ConfigureAuthorization(builder);
            ConfigureAuthentication(builder);
            ConfigureControllers(builder);
            ConfigureApiExplorer(builder);
            ConfigureSwaggerGen(builder);
            ConfigureExcelPackage();
        }

        private void ConfigureDbContext(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        }

        private void ConfigureAuthorization(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization();
        }

        private void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _options.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = _options.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = _options.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                    };
                });
        }

        private void ConfigureControllers(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
        }

        private void ConfigureApiExplorer(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
        }

        private void ConfigureSwaggerGen(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        private void ConfigureExcelPackage()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
    }
}
