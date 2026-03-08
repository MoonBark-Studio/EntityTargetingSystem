# EntityTargetingSystem Migration

## Scope

This plugin now owns the shared targeting contracts used by Thistletide:

- `ITargetingValidator`
- `TargetingResult`
- `TargetingFailureKind`
- `AbilityTargetingComponent`

## Integration Notes

Thistletide's `FactionTargetingValidationSystem` is now an adapter that implements `ITargetingValidator` while preserving game-specific faction rules.
