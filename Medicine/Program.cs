
using Application.Extensions;
using Infrastructure.Extensions;
using Medicine.Extensions;

namespace Medicine;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .RegisterApi(builder.Configuration)
            .AddApplication()
            .AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger(options =>
            {
                options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("./v1/swagger.json", "Medicine API v1");
            });
        }

        app.UseCors("AllowAll");

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
