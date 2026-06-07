using Serilog;
using Datadog.Trace;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.DatadogLogs(
        apiKey: builder.Configuration["DD_API_KEY"] ?? "",
        source: "csharp",
        service: builder.Configuration["DD_SERVICE"] ?? "example-dotnet-datadog-app",
        tags: [
            $"env:{builder.Configuration["DD_ENV"] ?? "production"}, version:{builder.Configuration["DD_VERSION"] ?? "1.0.0"}"
        ]
    )
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddSource("MyApp")
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(o =>
        { 
            o.Endpoint = new Uri("http://datadog-agent:8126");
        }));

var app = builder.Build();

app.MapGet("/", () => "Home page!");

app.MapGet("/trace", (ILogger<Program> logger) =>
{
    using var scope = Datadog.Trace.Tracer.Instance.StartActive("custom-operation");
    logger.LogInformation("Custom trace generated");
    scope.Span.SetTag("custom-tag", "value");
    return Results.Ok(new { traced = true });
});

app.Run();