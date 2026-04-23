# EntityTargetingSystem

A Godot C# plugin that provides faction-based targeting validation for abilities and skills in ECS-driven games. It defines which entities an ability can target based on hostility, neutrality, friendship, and faction alignment.

## Key Features

- **Faction-Aware Targeting** – Restrict abilities to hostile, neutral, friendly, or same-faction entities via `AbilityTargetingComponent`.
- **Custom Validators** – Plug in your own `ITargetingValidator` implementation for game-specific rules (team logic, PvP flags, etc.).
- **ECS-Native** – Built on Friflo.Engine.ECS components for high-performance entity queries.
- **Targeting Context** – Rich context struct carrying caster, target, position, ability ID, range, and line-of-sight requirements.
- **Framework Integration** – Registers cleanly with the MoonBark Framework module registry via `EntityTargetingModule`.

## Architecture

```
EntityTargetingSystem/
├── Core/
│   ├── AbilityTargetingComponent.cs      # ECS component defining targeting dispositions
│   ├── TargetingContext.cs               # Context record for validation requests
│   ├── ITargetingValidator.cs            # Interface for custom validation logic
│   ├── BaseTargetingValidator.cs         # Abstract base for validators
│   └── EntityTargetingModule.cs          # Framework module bootstrap
├── Tests/
│   └── AbilityTargetingComponentTests.cs # Unit tests
└── MoonBark.EntityTargetingSystem.csproj # Main project file
```

## Dependencies

- **Godot 4.x** with .NET support
- **.NET 8.0**
- **Friflo.Engine.ECS** (3.6.0) – ECS framework
- **MoonBark.Framework** – Shared framework types and module registry

## Usage

```csharp
// Create a targeting component that allows hostile and neutral targets
var targeting = new AbilityTargetingComponent(
    canTargetHostile: true,
    canTargetNeutral: true,
    canTargetFriendly: false,
    canTargetSameFaction: false
);

// Add to an entity
entity.AddComponent(targeting);

// Validate a target
bool valid = targeting.CanTarget(
    isHostile: true,
    isNeutral: false,
    isFriendly: false,
    isSameFaction: false
);
```

Register the module with your game’s service registry:

```csharp
var module = new EntityTargetingModule(myCustomValidator);
module.ConfigureServices(services);
```
