using Briefo.Application.ResumeBriefs.Contracts;
using Briefo.Domain.ResumeBriefs;

namespace Briefo.Features.ResumeGeneration.ResumeBriefs;

internal sealed class SampleResumeBriefService : IResumeBriefService
{
    private static readonly ResumeBrief SampleBrief = new(
        Persona: "Product Designer",
        TargetRole: "Senior Product Designer @ Briefo",
        Summary:
        "Fuses UX research with AI-assisted iteration to deliver polished resume briefs in minutes. Leads cross-functional pods and drives measurable improvements in candidate conversion.",
        Highlights: new[]
        {
            "Automated an AI brief review workflow that cut editing time by 45%",
            "Launched shared brief templates consumed by 3 customer segments in MVP",
            "Mentors junior designers on storytelling and structured writing"
        });

    public Task<ResumeBriefDto> GenerateSampleAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(ResumeBriefDto.FromDomain(SampleBrief));
}

