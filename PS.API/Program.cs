using Microsoft.OpenApi.Models;
using PS.Domain.Interfaces.Services;
using PS.Services.Imp;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                SetSwagger(options);
            });
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IApiTokenService, ApiTokenService>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddSingleton<ICrawlerDataService, CrawlerDataService>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void SetSwagger(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "PS API", Version = "v1" });

            options.AddSecurityDefinition("ApiToken", new OpenApiSecurityScheme
            {
                Description = "ApiToken necessário no cabeçalho da requisição. Exemplo: 'ApiToken: {token}'",
                Name = "ApiToken",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "ApiKeyScheme"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiToken"
                },
                Scheme = "ApiKeyScheme",
                Name = "ApiToken",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
        }
    }
}
