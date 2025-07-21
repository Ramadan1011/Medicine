﻿using Application.DTOs.Auth;
using Medicine.ExceptionHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Medicine.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();

        AddOptions(services, configuration);
        AddControllers(services);
        AddSwagger(services);
        services.AddErrorHandlers();
        AddAuthentication(services, configuration);

        return services;
    }

    private static void AddErrorHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
    }

    private static void AddOptions(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<AuthConfig>()
            .Bind(configuration.GetSection(AuthConfig.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    private static void AddControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
            options.ReturnHttpNotAcceptable = true;
            options.RespectBrowserAcceptHeader = true;
        })
            .AddNewtonsoftJson()
            .AddXmlSerializerFormatters()
            .AddXmlDataContractSerializerFormatters();
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Medicine API",
                Description = "Medicine REST API",
                Contact = new OpenApiContact
                {
                    Name = "Choriyev Ramazon",
                    Email = "shukhratovich75@gmail.com",
                    Url = new Uri("https://medicine.uz"),
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            var securityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Enter your JWT token in the text input below.",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                { securityScheme, [] }
            });

            //var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(AuthConfig.SectionName);
        var jwtOptions = section.Get<AuthConfig>()
            ?? throw new InvalidOperationException("JWT configuration settings did not load correctly.");
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });
    }
}
