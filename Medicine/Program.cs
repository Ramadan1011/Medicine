
using Application.Extensions;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Medicine.Extensions;
using Microsoft.EntityFrameworkCore;

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

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

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
