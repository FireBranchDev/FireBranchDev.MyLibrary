using FireBranchDev.MyLibrary.Application;
using FireBranchDev.MyLibrary.Persistence;
using JsonApiSerializer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.ObjectPool;
using System.Buffers;

namespace FireBranchDev.MyLibrary.Api;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.

        builder.Services.AddApplicationServices();
        builder.Services.AddPersistenceServices(builder.Configuration);

        var sp = builder.Services.BuildServiceProvider();
        var logger = sp.GetRequiredService<ILoggerFactory>();
        var objectPoolProvider = sp.GetRequiredService<ObjectPoolProvider>();

        builder.Services.AddControllers(options =>
        {
            var serializerSettings = new JsonApiSerializerSettings();

            var jsonOptions = new MvcNewtonsoftJsonOptions();

            var jsonApiOutputFormatter = new NewtonsoftJsonOutputFormatter(serializerSettings, ArrayPool<char>.Shared, options, jsonOptions);
            jsonApiOutputFormatter.SupportedMediaTypes.Clear();
            jsonApiOutputFormatter.SupportedMediaTypes.Add("application/vnd.api+json");
            options.OutputFormatters.Insert(0, jsonApiOutputFormatter);

            var jsonApiInputFormatter = new NewtonsoftJsonInputFormatter(logger.CreateLogger<NewtonsoftJsonInputFormatter>(), serializerSettings, ArrayPool<char>.Shared, objectPoolProvider, options, jsonOptions);
            jsonApiInputFormatter.SupportedMediaTypes.Clear();
            jsonApiInputFormatter.SupportedMediaTypes.Add("application/vnd.api+json");
            options.InputFormatters.Insert(0, jsonApiInputFormatter);
        });

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
