## How to run the project

**Prerequisites**
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server LocalDB (installed by default with Visual Studio, or via [SQL Server Express/LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb))

**Steps**
1. Clone the repository and open `Flashcards.Solomonlol.slnx`.
2. Check the connection string in `Flashcards.Solomonlol/appsettings.json` — by default it points to LocalDB:
   ```json
   "DefaultConnection": "Server=(localdb)\\FlashcardsDB;Database=Flashcards;Trusted_Connection=true;"
   ```
   Change it if you want to point to a different SQL Server instance.
3. Run the app:
   ```bash
   cd Flashcards.Solomonlol
   dotnet run
   ```
4. The database and tables are created automatically on first run via `Database.EnsureCreated()` — no manual migration step is needed. There's no seed data, so you'll start with an empty list of stacks.

---

## What the app does

Flashcards is a **console app** for creating study decks ("stacks") of flashcards and tracking study sessions:

- **Stacks** — named collections of flashcards (e.g. "Spanish verbs"). Stack names are unique. You can create, rename, and delete stacks.
- **Flashcards** — each belongs to a stack and has a `Question` and an `Answer`. A question must be unique within its stack. You can create, update, and delete flashcards.
- **Study mode** — pick a stack, choose which of its flashcards to study (multi-select), then answer each question one by one; the app checks your answer against the stored one (case-insensitive) and tallies a score.
- **Session history** — every finished study session is saved with a date/time and score, and can be viewed later as a table (date, time, score).

The whole app is menu-driven with `Spectre.Console`: a main menu leads into sub-menus for managing stacks and flashcards, and results/tables are rendered directly in the console.

---

## Architectural choices

The project is a single console project (no separate class library), organized by folder:

| Folder | Responsibility |
|---|---|
| `Model` / `Model/Dto` | Entity classes (`Stack`, `Flashcard`, `SessionHistory`) and DTOs (`FlashcardDto`, `SessionDto`) used to shape what's shown to the user |
| `Data` | EF Core `ApplicationContext`, repositories (`StackRepository`, `FlashcardRepository`, `SessionRepository`), and `UnitOfWork` |
| `Interfaces` | Repository and service interfaces (`IStackRepository`, `IStackService`, etc.) |
| `Services` | Business logic (`StackService`, `FlashcardService`, `SessionService`) — validation, orchestration, and console error messages |
| `Controllers` | `MainMenu` — the console UI layer that drives all the menus and prompts |

Key patterns used:

- **Repository + Unit of Work** — each entity has its own repository behind an interface (`IStackRepository`, `IFlashcardRepository`, `ISessionRepository`); `UnitOfWork` creates one shared `ApplicationContext` per operation, exposes the repositories, and centralizes `SaveChangesAsync()` in a single `Save()` method.
- **No DI container** — services and the `UnitOfWork` are instantiated directly (`new StackService()`, `using var unitOfWork = new UnitOfWork()`) rather than resolved through `Microsoft.Extensions.DependencyInjection`. This keeps the app simple, at the cost of tighter coupling between layers compared to a DI-based setup.
- **`ApplicationContext.EnsureCreated()`** is used instead of EF Core Migrations — it creates the database/schema from the current model if it doesn't exist, but (unlike migrations) it won't evolve an existing schema when the model changes later.
- **DTOs** (`FlashcardDto`, `SessionDto`) decouple what's displayed in the console from the EF entities — e.g. `SessionDto` splits a single `DateTime` into separate `DateOnly`/`TimeOnly` for display, and `FlashcardDto` avoids exposing the `Stack` navigation property to the UI.
- **Fluent API constraints** in `OnModelCreating` — unique index on `Stack.Name`, a composite unique index on `(Flashcard.Question, StackID)` so the same question can't be duplicated within a stack, and cascade delete from `Stack` to its `Flashcard`s and `SessionHistory` entries.
- **Console-first error handling** — services catch exceptions internally and print them via `Spectre.Console` (`AnsiConsole.MarkupLine`) rather than letting them bubble up, so the menu loop never crashes on invalid input (e.g. duplicate stack name).

---

## Reflection



- I used EnsureCreated() for this application. However, for future projects, I’ve learned about migrations, so my approach will change.
- I am not using DI in this project. At the time I was building this application, I didn't yet know how to use it. However, I will use DI in future projects.
- Overall, this turned out to be a decent assignment for gaining experience with EF Core and practicing the Repository and Unit of Work patterns.
