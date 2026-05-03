# HelloWorld.App ─ fertiges Git-Repository (neu initialisiert)
# _____________________________________________________________________________
# 1) .gitignore (gitignore.txt)
**
# .NET
bin/
obj/
*.user
*.suo
*.sln.docstates
.vs/
*.log
*.tlog
*.lastbuildstate
*.cache
*.pdb
*.dll
*.exe
!*.exe.config

# IDEs (Rider / VS Code)
.idea/
*.DotSettings.user
.vscode/

# OS
.DS_Store
Thumbs.db

# NuGet
*.nuget.props
*.nuget.targets
*.nupkg

# Output / Publish
publish/
out/

# „deterministic build“ Artefakte
*.AssemblyAttributes.cs
**

# 2) HelloWorld.App.csproj (HelloWorld.App.csproj)
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <Deterministic>true</Deterministic>
    <AssemblyTitle>HelloWorld.App</AssemblyTitle>
  </PropertyGroup>
  <Import Project="InstallTargets.targets" />
</Project>

# 3) InstallTargets.targets (InstallTargets.targets)
<Project>
  <!-- Zusätzliche deterministische Build-Flags für Reproduzierbarkeit -->
  <PropertyGroup>
    <!-- Git-Commit-Hash als Teil der Assembly-Version (nur wenn im Git-Repo verfügbar) -->
    <SourceRevisionId Condition="Exists('$(MSBuildThisFileDirectory).git\HEAD')">$([System.IO.File]::ReadAllText('$(MSBuildThisFileDirectory).git\HEAD').Substring(0,7))</SourceRevisionId>
  </PropertyGroup>
  <!-- Optional: Fügt AssemblyMetadata-Attribute hinzu -->
  <ItemGroup>
    <AssemblyMetadata Include="Repository.Url" Value="https://github.com/DevSwarm/HelloWorld.App.git" />
    <AssemblyMetadata Include="SourceRevisionId" Value="$(SourceRevisionId)" />
  </ItemGroup>
</Project>

# 4) Program.cs (file-scoped namespace, nullable refs)
using System;

namespace HelloWorld.App;

internal static class Program
{
    internal static async Task Main()
    {
        Console.Write("Wie heißt du? ");
        var name = await Console.In.ReadLineAsync();
        Console.WriteLine($"Hallo, {name}!");
    }
}

# 5) README.md (README.md)
# HelloWorld.App

Ein minimales .NET 8 Konsolen-Hello-World-Beispiel, das den Nutzer persönlich begrüßt.

## Build

Im Repository-Stamm ausführen:
```shell
dotnet build -c Release
```

## Ausführen

```shell
dotnet run -c Release --project HelloWorld.App
```

ODER
```shell
dotnet HelloWorld.App/bin/Release/net8.0/HelloWorld.App.dll
```

## Systemvoraussetzungen

- .NET 8 SDK (Runtime reicht **nicht**)

## Lizenz

MIT – siehe [LICENSE](LICENSE).

# 6) CONTRIBUTING.md (CONTRIBUTING.md)
# Contributing

Wir freuen uns über Pull-Requests!

## Branching-Modell
- `main` – immer stabil & release-bereit
- `feature/<issue-nr>-<sinngemäße-kurzbeschreibung>`

## Commit-Conventions
```
type(scope): kurze Beschreibung
```
Beispiele: `feat(cli): add --version flag`, `fix(readme): correct build command`

gültige Typen: `feat`, `fix`, `docs`, `test`, `chore`, `refactor`

## Pull-Request-Checklist
- [ ] `dotnet build` und `dotnet test` lokal grün
- [ ] CHANGELOG.md erweitert falls nötig
- [ ] PR-Template ausgefüllt

## CI
GitHub Actions (siehe `.github/workflows/ci.yml`) baut und testet jeden Push.

# 7) .github/workflows/ci.yml (ci.yml)
name: CI

on:
  push:
    branches: [main]
  pull_request:

jobs:
  build:
    runs-on: ubuntu-latest
    permissions:
      contents: read
    steps:
    - uses: actions/checkout@v4
    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: '8.0.x'
    - name: Restore
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore -c Release
    - name: Test
      run: dotnet test --no-build -c Release --verbosity normal

# 8) Lokaler Testbefehl & Git-Setup-Skript (quickstart.sh)
#!/usr/bin/env bash
set -euo pipefail

# Repository frisch initialisieren (nur 1x ausführen)
git init
git add -
git commit -m "feat(repo): initial .NET 8 HelloWorld.App"
git tag v1.0.0

# Build & Run lokaler Selbst-Test
dotnet build -c Release
dotnet run -c Release --project HelloWorld.App <<<'Ada'

# Tag v1.0.0 ist nun gesetzt und kann per `git push origin --tags` gepusht werden

# 9) GitHub Release Notes (RELEASE.md – Direkt für Web-UI)
## v1.0.0 – Erste stabile Version

- **Neu**: Konsolenanwendung begrüßt den Nutzer mit personalisierdem Hallo
- **Tech-Stack**: .NET 8, deterministische Builds, nullable references
- **CI**: GitHub Actions für automatisches Build & Test
- **Assets**: Quell-Code-Archive (`Source code (zip)`, `Source code (tar.gz)`)

Download & Run: `dotnet run --project HelloWorld.App`.

# ____________________________________________________________________