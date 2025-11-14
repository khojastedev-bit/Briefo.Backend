using Briefo.Domain.ResumeBriefs;

namespace Briefo.Application.ResumeBriefs.Contracts;

public interface IResumeBriefService
{
    Task<ResumeBriefDto> GenerateSampleAsync(CancellationToken cancellationToken = default);
}

public sealed record ResumeBriefDto(
    string Persona,
    string TargetRole,
    string Summary,
    IReadOnlyCollection<string> Highlights)
{
    public static ResumeBriefDto FromDomain(ResumeBrief aggregate) =>
        new(aggregate.Persona, aggregate.TargetRole, aggregate.Summary, aggregate.Highlights);
}

