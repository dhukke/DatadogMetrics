using DatadogMetrics.Configuration;
using StatsdClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/the-get", () =>
{
    using var dogStatsdService = new DogStatsdService();

    if (!dogStatsdService.Configure(DataDogStatsDConfiguration.DogstatsdConfig))
    {
        throw new InvalidOperationException(
            "Cannot initialize DogstatsD. Set optionalExceptionHandler argument in the `Configure` method for more information."
        );
    }

    dogStatsdService.Counter("project_metric.getcounter", 1, tags: DataDogTags.Default);

    return string.Empty;
})
.WithName("Get")
.WithOpenApi();

app.MapPost("/the-post", () =>
{
    using var dogStatsdService = new DogStatsdService();

    if (!dogStatsdService.Configure(DataDogStatsDConfiguration.DogstatsdConfig))
    {
        throw new InvalidOperationException(
            "Cannot initialize DogstatsD. Set optionalExceptionHandler argument in the `Configure` method for more information."
        );
    }

    dogStatsdService.Increment("project_metric.postcounter", tags: DataDogTags.Default);
})
.WithName("Post")
.WithOpenApi();

app.Run();
