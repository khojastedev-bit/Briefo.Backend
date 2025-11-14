namespace Briefo.Domain.ResumeBriefs;

public sealed record ResumeBrief(
    string Persona,
    string TargetRole,
    string Summary,
    IReadOnlyCollection<string> Highlights);

