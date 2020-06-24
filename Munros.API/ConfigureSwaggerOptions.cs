using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Munros.API
{
    // Source: https://github.com/microsoft/aspnet-api-versioning/blob/master/samples/aspnetcore/SwaggerSample/ConfigureSwaggerOptions.cs
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc(
                "Munros",
                new OpenApiInfo()
                {
                    Title = "Munros API",
                    Version = "V1"
                });
        }
    }
}
