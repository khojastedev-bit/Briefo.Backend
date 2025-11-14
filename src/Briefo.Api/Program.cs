using Briefo.Features.ResumeGeneration.ResumeBriefs;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOpenApi()
    .AddResumeBriefModule();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapResumeBriefModule();

app.Run();
