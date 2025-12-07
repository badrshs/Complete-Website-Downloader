# Complete Website Downloader - Copilot Instructions

## Architecture Overview

Windows Forms (.NET Framework 4.7.2) desktop app that wraps GNU wget for recursive website downloads. The key architectural pattern is **dependency injection via interfaces** with event-driven async operations.

```
MainForm.cs (UI) → IWebsiteDownloader (interface) → WgetDownloader (implementation)
                 → AppSettings/DownloadHistory (persistence via JSON)
                 → ResourceExtractor (extracts embedded wget.exe at runtime)
```

## Build & Run

```powershell
# Restore and build
nuget restore WebsiteDownloader.sln
msbuild WebsiteDownloader.sln /p:Configuration=Release

# Output: WebsiteDownloader\bin\Release\WebsiteDownloader.exe
```

**Important**: Costura.Fody embeds all dependencies (including `wget.exe`) into a single executable. Changes to embedded resources require a rebuild.

## Key Patterns & Conventions

### Service Layer Pattern
All core operations go through interfaces in `Services/`:
- `IWebsiteDownloader` - download operations with events (`ProgressChanged`, `DownloadCompleted`)
- `IAppLogger` - logging abstraction

When adding new functionality, follow this pattern:
```csharp
// 1. Define interface with events for async notifications
public interface INewService : IDisposable
{
    event EventHandler<ProgressEventArgs> ProgressChanged;
    Task DoWorkAsync(CancellationToken ct);
}
```

### Settings & Persistence
- All persistent data uses `Newtonsoft.Json` and lives in `%LocalAppData%\WebsiteDownloader\`
- `AppSettings.cs` - user preferences (uses atomic write pattern with `.tmp` file)
- `DownloadHistory.cs` - thread-safe with `_lockObject` pattern
- Add new settings to `AppSettings` class with defaults, they auto-persist

### String Resources
All user-facing strings must go in `Resources/Strings.cs` - not inline:
```csharp
// Good: Uses centralized strings
MessageBox.Show(Strings.ValidationEnterUrl);

// Bad: Inline strings
MessageBox.Show("Please enter a URL");
```

### Constants
Magic values belong in `AppConstants.cs`:
```csharp
public const int MaxHistoryItems = 50;
public static string AppDataFolder => Path.Combine(...);
```

## Critical Implementation Details

### Embedded wget.exe
`wget.exe` is an **EmbeddedResource** extracted to temp at runtime:
```csharp
ResourceExtractor.ExtractWget() // Returns path to extracted exe
```
The file is versioned (`WebsiteDownloader_wget_{version}.exe`) to avoid conflicts.

### Async/Cancellation Pattern
Downloads use `CancellationTokenSource` with proper cleanup:
```csharp
_cts = new CancellationTokenSource();
await _downloader.DownloadAsync(options, _cts.Token);
// Cancellation: _cts.Cancel() + process termination
```

### Error Detection
`MainForm.cs` uses regex patterns to classify wget output:
- `ErrorPatterns` - HTTP errors, connection failures
- `WarningPatterns` - SSL issues, skipped files  
- `IgnorePatterns` - progress indicators (false positive prevention)

## Windows Forms Conventions

- Designer files (`*.Designer.cs`) are auto-generated - don't edit manually
- UI updates must be on UI thread - use `BeginInvoke` for cross-thread calls
- Forms follow naming: `MainForm`, `SettingsForm`, `HistoryForm`

## CI/CD

GitHub Actions workflow (`.github/workflows/dotnet-desktop.yml`) builds both Debug and Release on `windows-latest`. Signing certificate is optional via secrets.
