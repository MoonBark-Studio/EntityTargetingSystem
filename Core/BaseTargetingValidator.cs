namespace MoonBark.EntityTargetingSystem;

using Friflo.Engine.ECS;
using System.Numerics;

/// <summary>
/// Base implementation of ITargetingValidator with common targeting checks.
/// </summary>
/// <remarks>
/// This class provides base implementations for common targeting checks
/// like range and line-of-sight. Games can extend this class to add
/// game-specific targeting logic (factions, teams, alignment, etc.).
/// </remarks>
public abstract class BaseTargetingValidator : ITargetingValidator
{
    /// <summary>
    /// Gets the targeting rules for a specific ability.
    /// </summary>
    /// <param name="abilityId">The ability ID.</param>
    /// <returns>The targeting rules for the ability.</returns>
    protected abstract AbilityTargetingRules GetAbilityTargetingRules(string abilityId);

    /// <summary>
    /// Validates whether a caster can target a specific entity with an ability.
    /// </summary>
    /// <param name="caster">The entity attempting to cast the ability.</param>
    /// <param name="target">The target entity.</param>
    /// <param name="abilityId">The ID of the ability being cast.</param>
    /// <returns>A targeting result indicating whether the target is valid.</returns>
    public virtual TargetingResult CanTarget(Entity caster, Entity target, string abilityId)
    {
        // Get targeting rules for the ability
        var rules = GetAbilityTargetingRules(abilityId);
        
        // Check range
        var rangeResult = CheckRange(caster, target, rules.Range);
        if (!rangeResult.CanTarget)
        {
            return rangeResult;
        }
        
        // Check line of sight
        if (rules.RequiresLineOfSight)
        {
            var losResult = CheckLineOfSight(caster, target);
            if (!losResult.CanTarget)
            {
                return losResult;
            }
        }
        
        // Allow game-specific validation
        return ValidateTargetSpecific(caster, target, abilityId, rules);
    }

    /// <summary>
    /// Validates whether a caster can target a specific position with an ability.
    /// </summary>
    /// <param name="caster">The entity attempting to cast the ability.</param>
    /// <param name="position">The target position.</param>
    /// <param name="abilityId">The ID of the ability being cast.</param>
    /// <returns>A targeting result indicating whether the target is valid.</returns>
    public virtual TargetingResult CanTargetPosition(Entity caster, Vector2 position, string abilityId)
    {
        // Get targeting rules for the ability
        var rules = GetAbilityTargetingRules(abilityId);
        
        // Check range
        var rangeResult = CheckRangeToPosition(caster, position, rules.Range);
        if (!rangeResult.CanTarget)
        {
            return rangeResult;
        }
        
        // Check line of sight
        if (rules.RequiresLineOfSight)
        {
            var losResult = CheckLineOfSightToPosition(caster, position);
            if (!losResult.CanTarget)
            {
                return losResult;
            }
        }
        
        // Allow game-specific validation
        return ValidatePositionSpecific(caster, position, abilityId, rules);
    }

    /// <summary>
    /// Validates game-specific targeting rules for entity targeting.
    /// </summary>
    /// <param name="caster">The entity attempting to cast the ability.</param>
    /// <param name="target">The target entity.</param>
    /// <param name="abilityId">The ID of the ability being cast.</param>
    /// <param name="rules">The targeting rules for the ability.</param>
    /// <returns>A targeting result indicating whether the target is valid.</returns>
    protected abstract TargetingResult ValidateTargetSpecific(
        Entity caster,
        Entity target,
        string abilityId,
        AbilityTargetingRules rules);

    /// <summary>
    /// Validates game-specific targeting rules for position targeting.
    /// </summary>
    /// <param name="caster">The entity attempting to cast the ability.</param>
    /// <param name="position">The target position.</param>
    /// <param name="abilityId">The ID of the ability being cast.</param>
    /// <param name="rules">The targeting rules for the ability.</param>
    /// <returns>A targeting result indicating whether the target is valid.</returns>
    protected abstract TargetingResult ValidatePositionSpecific(
        Entity caster,
        Vector2 position,
        string abilityId,
        AbilityTargetingRules rules);

    /// <summary>
    /// Checks if the target is within range of the caster.
    /// </summary>
    /// <param name="caster">The caster entity.</param>
    /// <param name="target">The target entity.</param>
    /// <param name="range">The maximum range.</param>
    /// <returns>A targeting result indicating whether the target is in range.</returns>
    protected virtual TargetingResult CheckRange(Entity caster, Entity target, float range)
    {
        // NOTE: This is a simplified implementation.
        // In a real implementation, you would check if both entities have position components
        // and calculate the distance between them.
        
        // For now, assume in range
        return TargetingResult.Success();
    }

    /// <summary>
    /// Checks if the position is within range of the caster.
    /// </summary>
    /// <param name="caster">The caster entity.</param>
    /// <param name="position">The target position.</param>
    /// <param name="range">The maximum range.</param>
    /// <returns>A targeting result indicating whether the position is in range.</returns>
    protected virtual TargetingResult CheckRangeToPosition(Entity caster, Vector2 position, float range)
    {
        // NOTE: This is a simplified implementation.
        // In a real implementation, you would check if the caster has a position component
        // and calculate the distance to the target position.
        
        // For now, assume in range
        return TargetingResult.Success();
    }

    /// <summary>
    /// Checks if there is a clear line of sight to the target.
    /// </summary>
    /// <param name="caster">The caster entity.</param>
    /// <param name="target">The target entity.</param>
    /// <returns>A targeting result indicating whether there is a clear line of sight.</returns>
    protected virtual TargetingResult CheckLineOfSight(Entity caster, Entity target)
    {
        // NOTE: This is a simplified implementation.
        // In a real implementation, you would perform raycasting or pathfinding
        // to check if there are obstacles between the caster and target.
        
        // For now, assume clear line of sight
        return TargetingResult.Success();
    }

    /// <summary>
    /// Checks if there is a clear line of sight to the position.
    /// </summary>
    /// <param name="caster">The caster entity.</param>
    /// <param name="position">The target position.</param>
    /// <returns>A targeting result indicating whether there is a clear line of sight.</returns>
    protected virtual TargetingResult CheckLineOfSightToPosition(Entity caster, Vector2 position)
    {
        // NOTE: This is a simplified implementation.
        // In a real implementation, you would perform raycasting or pathfinding
        // to check if there are obstacles between the caster and position.
        
        // For now, assume clear line of sight
        return TargetingResult.Success();
    }
}

/// <summary>
/// Defines targeting rules for an ability.
/// </summary>
public struct AbilityTargetingRules
{
    /// <summary>
    /// Whether the ability can target hostile entities.
    /// </summary>
    public bool CanTargetHostile { get; set; }

    /// <summary>
    /// Whether the ability can target neutral entities.
    /// </summary>
    public bool CanTargetNeutral { get; set; }

    /// <summary>
    /// Whether the ability can target friendly entities.
    /// </summary>
    public bool CanTargetFriendly { get; set; }

    /// <summary>
    /// Whether the ability can target entities of the same faction.
    /// </summary>
    public bool CanTargetSameFaction { get; set; }

    /// <summary>
    /// Maximum range for the ability.
    /// </summary>
    public float Range { get; set; }

    /// <summary>
    /// Whether the ability requires line of sight.
    /// </summary>
    public bool RequiresLineOfSight { get; set; }

    /// <summary>
    /// Creates new targeting rules.
    /// </summary>
    public AbilityTargetingRules(
        bool canTargetHostile,
        bool canTargetNeutral,
        bool canTargetFriendly,
        bool canTargetSameFaction,
        float range,
        bool requiresLineOfSight)
    {
        CanTargetHostile = canTargetHostile;
        CanTargetNeutral = canTargetNeutral;
        CanTargetFriendly = canTargetFriendly;
        CanTargetSameFaction = canTargetSameFaction;
        Range = range;
        RequiresLineOfSight = requiresLineOfSight;
    }
}

