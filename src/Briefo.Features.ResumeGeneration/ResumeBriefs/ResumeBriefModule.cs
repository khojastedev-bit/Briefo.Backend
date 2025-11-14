using Briefo.Application.ResumeBriefs.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Briefo.Features.ResumeGeneration.ResumeBriefs;

public static class ResumeBriefModule
{
    public static IServiceCollection AddResumeBriefModule(this IServiceCollection services)
    {
        services.AddSingleton<IResumeBriefService, SampleResumeBriefService>();
        return services;
    }

    public static IEndpointRouteBuilder MapResumeBriefModule(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/resume-briefs")
            .WithTags("ResumeBriefs")
            .WithDescription("Endpoints for generating and managing resume briefs");

        group.MapGet("/sample", async (IResumeBriefService service, CancellationToken cancellationToken) =>
            Results.Ok(await service.GenerateSampleAsync(cancellationToken)))
            .WithName("GetSampleResumeBrief")
            .WithSummary("Retrieves a deterministic sample resume brief")
            .Produces<ResumeBriefDto>();

        return endpoints;
    }
}

