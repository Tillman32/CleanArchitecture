# TODOS

## WASM / feat/WASM

### OPFS Error UI
**What:** Show a user-visible error banner when OPFS is unavailable.
**Why:** In private browsing, old Safari, or storage-quota-exceeded scenarios, SQLite WASM init fails silently and the user sees a blank posts list with no explanation.
**Pros:** User-facing, easy to implement (try/catch in Program.cs + a simple error component).
**Cons:** Minor scope expansion.
**Context:** Program.cs awaits ApplicationDbInitializer.Initialize() before RunAsync. If that throws, wrap in try/catch and render an `<AppError>` component before RunAsync. Added during /plan-eng-review on feat/WASM.
**Depends on:** SQLite WASM setup being complete.

---

### Schema Migration Strategy for OPFS
**What:** Define a plan for applying EF Core schema changes to an existing OPFS SQLite DB.
**Why:** `EnsureCreated()` creates the schema for fresh DBs but won't apply new columns (e.g., the Tags field) to an OPFS DB a user already has. Future changes will silently miss existing users.
**Pros:** Prevents future data loss and "field doesn't exist" runtime errors.
**Cons:** EF Core migrations in WASM are non-trivial; may require a custom migration runner.
**Context:** Current plan uses EnsureCreated (acceptable for fresh install). Before the next schema change, decide: EF Core migrations, custom versioning table, or "nuke and reseed" with a schema version check.
**Depends on:** Tags field addition being shipped first.

---

### Unit Tests for WasmArticleService
**What:** Add xUnit tests for WasmArticleService covering all paths.
**Why:** No tests shipped with the service. xUnit + Moq infrastructure already exists.
**Pros:** Catches regressions in service logic. Mock IGenericRepository<ArticleEntity> to isolate.
**Cons:** SQLite WASM itself can't be unit tested, only the service logic.
**Context:** Test cases needed: ListAllArticles (with data, empty DB), GetArticle (found, not found → null), CreateArticle (valid DTO, null DTO), UploadArticleImageAsync (must throw NotSupportedException). See CleanArchitecture.Test/Data/GenericRepositoryTest.cs for existing style.
**Depends on:** WasmArticleService implementation.
