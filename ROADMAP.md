# EntityTargetingSystem — Roadmap
Updated: 2026-04-14

## Overview
ECS targeting validation system providing ITargetingValidator, TargetingResult, TargetingFailureKind, and AbilityTargetingComponent for ability systems.

## Development Phases

### Phase 1: Integration (Current)
- [ ] Fix build issues in EntityTargetingSystem project.
- [ ] Integrate targeting validation with Abilities plugin.
- [ ] Verify ITargetingValidator contract with AbilityExecution pipeline.
- [ ] Add unit tests for core validation logic.

### Phase 2: Core Features
- [ ] Implement line-of-sight checking in BaseTargetingValidator.
- [ ] Add area-of-effect (AoE) targeting validation.
- [ ] Support multi-target validation for abilities.
- [ ] Add targeting priority/filter system.

### Phase 3: Performance
- [ ] Add batch processing for mass targeting validation.
- [ ] Implement targeting result caching.
- [ ] Profile and optimize hot paths.

## Key Deliverables

| Feature | Status | Priority |
|---------|--------|----------|
| ITargetingValidator interface | Done | P0 |
| TargetingResult with failure details | Done | P0 |
| TargetingFailureKind enum | Done | P0 |
| AbilityTargetingComponent | Done | P0 |
| BaseTargetingValidator | Done | P1 |
| Line-of-sight validation | Planned | P2 |
| AoE targeting | Planned | P2 |
| Batch processing | Planned | P3 |
| Targeting cache | Planned | P3 |

## Dependencies

| Plugin | Purpose |
|--------|---------|
| Abilities | Ability definitions and targeting rule lookup |
| MoonBark.Framework | Shared ECS contracts and constants |
