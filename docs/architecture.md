# Briefo Backend Boilerplate

## 1. Purpose
- Spin up a **Feature‑Based Modular Monolith** that can grow into multiple bounded contexts without microservice overhead.
- Keep core flows (auth, identity graph, AI content generation) inside a single deployable unit while enforcing boundaries through projects and namespaces.

## 2. Solution Layout
```
src/
 ├─ Briefo.sln
 ├─ Briefo.Api/                  // HTTP surface + integration glue
 ├─ Briefo.Application/          // Use-cases + contracts
 ├─ Briefo.Domain/               // Aggregates, value objects, invariants
 ├─ Briefo.Infrastructure/       // External adapters (DB, queues, AI providers)
 └─ Briefo.Features.ResumeGeneration/ // Sample vertical slice / module
```

### Layer Intent
- **Api** references only the feature modules it needs. It should not host business rules.
- **Features** package the HTTP endpoints, handlers, and application services for a vertical capability (e.g., ResumeGeneration, Payments, Profiles). Think “mini-module” with its own dependency injection extensions.
- **Application** holds contracts shared between features (commands, queries, DTOs) plus orchestrations that span modules.
- **Domain** models the ubiquitous language. No infrastructure dependencies.
- **Infrastructure** implements interfaces from Application/Features (persistence, AI clients, auth providers). Registered through `ServiceCollection` extensions without leaking tech details upward.

## 3. Sample Feature: ResumeGeneration
```
Briefo.Features.ResumeGeneration/
 └─ ResumeBriefs/
     ├─ SampleResumeBriefService.cs   // Implements IResumeBriefService
     └─ ResumeBriefModule.cs          // DI + endpoint wiring
```
- Exposes `GET /api/resume-briefs/sample`.
- Demonstrates how each module self-registers services (`AddResumeBriefModule`) and endpoints (`MapResumeBriefModule`).
- Consumes shared contracts from `Briefo.Application` and aggregates from `Briefo.Domain`.

## 4. Expansion Playbook
1. **New Feature** → `dotnet new classlib -n Briefo.Features.<Name>` → add module extensions, handlers, validators.
2. **Domain Changes** → update `Briefo.Domain`; expose DTO translators in Application.
3. **Infrastructure Adapter** → implement in `Briefo.Infrastructure` and register via `services.AddInfrastructure(builder.Configuration)`.
4. **API Surface** → call `builder.Services.Add<Feature>Module()` and `app.Map<Feature>Module()`.

## 5. Target Capabilities Roadmap
- **Identity & Auth**: dedicated `Authentication` feature with providers (Email, Google, LinkedIn) and token service inside Infrastructure.
- **Content Graph**: central domain aggregate (`ProfessionalProfile`) powering resumes, proposals, and portfolios.
- **Generation Pipeline**: orchestrated workflows (prompt templates, AI calls, post-processors) packaged as their own feature.
- **Quota & Billing**: Stripe integration + policy enforcement middleware.
- **Publishing**: Public profile serving (`briefo.me/<slug>`) handled inside a `Publishing` feature, optionally backed by edge cache.

## 6. Operational Notes
- Stick to **internal modules** until we hit team/org scaling limits; then slice select modules into services.
- Enforce boundaries via `internal` types and explicit project references.
- Prefer request/response contracts in Application so both API and jobs/automation can reuse them.
- Testing: start with feature-level integration tests (minimal API + in-memory services), then layer in domain specs.

این ساختار پایه به ما اجازه می‌دهد سریع MVP را بالا بیاوریم و در عین حال از اول به انبساط‌پذیری فکر کنیم. هر فیچر یک بستهٔ مستقل است که به شکل ماژول به اپی اضافه می‌شود؛ بعداً می‌توانیم حتا همین ماژول‌ها را بیرون بکشیم و به میکروسرویس تبدیل کنیم.

