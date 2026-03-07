# EntityTargetingSystem

A C# ECS plugin for validating entity targeting in Godot games using Friflo.Engine.ECS.

## Purpose

This plugin provides a flexible targeting validation system that allows games to implement custom targeting logic while providing a consistent API for ability systems to use.

## Core Functionality

### 1. ITargetingValidator Interface

The `ITargetingValidator` interface defines the contract for targeting validation:

```csharp
public interface ITargetingValidator
{
    TargetingResult CanTarget(Entity caster, Entity target, string abilityId);
    TargetingResult CanTargetPosition(Entity caster, Vector2 position, string abilityId);
}
```

### 2. Targeting Result

The `TargetingResult` class provides detailed targeting information:
- `CanTarget` - Whether targeting is allowed
- `FailureReason` - Human-readable explanation of failure
- `FailureKind` - Categorized failure reason (Range, LineOfSight, Faction, Other)

### 3. Targeting Context

The `TargetingContext` class provides additional context for targeting decisions:
- Caster entity
- Target entity or position
- Ability being used
- World state

### 4. Base Targeting Validator

The `BaseTargetingValidator` class provides a convenient base implementation with common validation logic:
- Range checks
- Line-of-sight checks
- Faction/team validation
- Extensible for custom rules

## Architecture

```
EntityTargetingSystem/
├── Core/
│   ├── ITargetingValidator.cs       # Targeting validation interface
│   ├── TargetingResult.cs          # Targeting result with details
│   ├── TargetingContext.cs         # Context for targeting decisions
│   └── BaseTargetingValidator.cs   # Base implementation with common logic
└── EntityTargetingSystem.csproj    # Project file
```

## Dependencies

- **Friflo.Engine.ECS** - ECS framework (version 3.4.2)

## Usage Example

### Implementing a Custom Validator

```csharp
public class FactionTargetingValidator : ITargetingValidator
{
    private readonly FactionSystem _factionSystem;

    public FactionTargetingValidator(FactionSystem factionSystem)
    {
        _factionSystem = factionSystem;
    }

    public TargetingResult CanTarget(Entity caster, Entity target, string abilityId)
    {
        // Check if target is in same faction
        if (caster.HasComponent<FactionComponent>() && target.HasComponent<FactionComponent>())
        {
            ref var casterFaction = ref caster.GetComponent<FactionComponent>();
            ref var targetFaction = ref target.GetComponent<FactionComponent>();

            if (casterFaction.FactionId == targetFaction.FactionId)
            {
                // Cannot target allies (unless ability allows it)
                return TargetingResult.Failure("Cannot target allies", TargetingFailureKind.Faction);
            }
        }

        // Check range
        if (caster.HasComponent<PositionComponent>() && target.HasComponent<PositionComponent>())
        {
            ref var casterPos = ref caster.GetComponent<PositionComponent>();
            ref var targetPos = ref target.GetComponent<PositionComponent>();
            float distance = Vector2.Distance(casterPos.Position, targetPos.Position);

            if (distance > GetAbilityRange(abilityId))
            {
                return TargetingResult.Failure($"Target out of range (distance: {distance:F1}, range: {GetAbilityRange(abilityId):F1})", TargetingFailureKind.Range);
            }
        }

        return TargetingResult.Success();
    }

    public TargetingResult CanTargetPosition(Entity caster, Vector2 position, string abilityId)
    {
        // Check if position is in range
        if (caster.HasComponent<PositionComponent>())
        {
            ref var casterPos = ref caster.GetComponent<PositionComponent>();
            float distance = Vector2.Distance(casterPos.Position, position);

            if (distance > GetAbilityRange(abilityId))
            {
                return TargetingResult.Failure($"Position out of range (distance: {distance:F1}, range: {GetAbilityRange(abilityId):F1})", TargetingFailureKind.Range);
            }
        }

        return TargetingResult.Success();
    }

    private float GetAbilityRange(string abilityId)
    {
        // Return range based on ability ID
        return abilityId switch
        {
            "fireball" => 10.0f,
            "heal" => 5.0f,
            _ => 5.0f
        };
    }
}
```

### Using the Validator

```csharp
// Create validator
var validator = new FactionTargetingValidator(factionSystem);

// Check if can target entity
var result = validator.CanTarget(casterEntity, targetEntity, "fireball");

if (result.CanTarget)
{
    // Proceed with ability execution
    ExecuteAbility(casterEntity, targetEntity, "fireball");
}
else
{
    // Show failure reason to player
    ShowMessage(result.FailureReason);
}
```

### Using BaseTargetingValidator

```csharp
public class AdvancedTargetingValidator : BaseTargetingValidator
{
    protected override bool CheckLineOfSight(Entity caster, Entity target, string abilityId)
    {
        // Custom line-of-sight implementation
        return HasLineOfSight(caster, target);
    }

    protected override bool CheckFactionRules(Entity caster, Entity target, string abilityId)
    {
        // Custom faction rules
        if (IsFriendlyAbility(abilityId))
        {
            return AreAllies(caster, target);
        }
        else
        {
            return AreEnemies(caster, target);
        }
    }
}
```

## Targeting Failure Kinds

The `TargetingFailureKind` enum categorizes targeting failures:

| Kind | Description |
|------|-------------|
| `Range` | Target is out of range |
| `LineOfSight` | No line of sight to target |
| `Faction` | Faction/team restrictions |
| `Other` | Other custom failures |

## Design Principles

- **Flexible** - Games can implement custom targeting logic
- **Consistent API** - Single interface for all targeting validation
- **Detailed Feedback** - Clear failure reasons for UI feedback
- **Extensible** - Easy to add custom validation rules
- **Performance** - Efficient validation for real-time use

## Integration with Other Plugins

This plugin integrates with:
- **AbilitySystem** - Provides ability definitions for targeting rules
- **AbilityExecution** - Uses targeting validation in ability execution pipeline

## Common Targeting Scenarios

### Faction-Based Targeting
- Prevent targeting allies with hostile abilities
- Prevent targeting enemies with friendly abilities
- Support neutral/hostile/friendly relationships

### Range-Based Targeting
- Check distance between caster and target
- Support different ranges for different abilities
- Account for terrain and obstacles

### Line-of-Sight Targeting
- Check if target is visible to caster
- Account for walls and obstacles
- Support abilities that don't require line of sight

### Custom Rules
- Ability-specific targeting restrictions
- Status-based targeting (e.g., can't target stealthed enemies)
- Resource-based targeting (e.g., requires mana to target)

## Future Enhancements

Potential additions:
- Targeting filters (e.g., only target enemies with low health)
- Targeting priorities (e.g., prioritize closest enemy)
- Area-of-effect targeting validation
- Multi-target validation
- Targeting preview and visualization

## License

[Your License Here]
