namespace MoonBark.Framework.Core.Targeting;

using Friflo.Engine.ECS;
using System.Numerics;

/// <summary>
/// Interface for validating whether an entity can target another entity or position.
/// </summary>
/// <remarks>
/// This interface allows games to implement their own targeting logic
/// (factions, teams, alignment, etc.) while providing a consistent API
/// for the ability system to use.
/// </remarks>
public interface ITargetingValidator
{
    /// <summary>
    /// Validates whether a caster can target a specific entity with an ability.
    /// </summary>
    /// <param name="caster">The entity attempting to cast the ability.</param>
    /// <param name="target">The target entity.</param>
    /// <param name="abilityId">The ID of the ability being cast.</param>
    /// <returns>A targeting result indicating whether the target is valid.</returns>
    TargetingResult CanTarget(Entity caster, Entity target, string abilityId);

    /// <summary>
    /// Validates whether a caster can target a specific position with an ability.
    /// </summary>
    /// <param name="caster">The entity attempting to cast the ability.</param>
    /// <param name="position">The target position.</param>
    /// <param name="abilityId">The ID of the ability being cast.</param>
    /// <returns>A targeting result indicating whether the target is valid.</returns>
    TargetingResult CanTargetPosition(Entity caster, Vector2 position, string abilityId);
}
