namespace MoonBark.EntityTargetingSystem;

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

/// <summary>
/// Result of a targeting validation check.
/// </summary>
public readonly record struct TargetingResult(
    bool CanTarget,
    string? FailureReason,
    TargetingFailureKind FailureKind
)
{
    /// <summary>
    /// Creates a successful targeting result.
    /// </summary>
    public static TargetingResult Success() => new(true, null, TargetingFailureKind.None);

    /// <summary>
    /// Creates a failed targeting result.
    /// </summary>
    /// <param name="failureReason">Human-readable reason for the failure.</param>
    /// <param name="failureKind">The kind of failure.</param>
    public static TargetingResult Failure(string failureReason, TargetingFailureKind failureKind) =>
        new(false, failureReason, failureKind);
}

/// <summary>
/// The kind of targeting failure.
/// </summary>
public enum TargetingFailureKind
{
    /// <summary>
    /// No failure - targeting is valid.
    /// </summary>
    None,

    /// <summary>
    /// Target is out of range.
    /// </summary>
    OutOfRange,

    /// <summary>
    /// Line of sight to target is blocked.
    /// </summary>
    LineOfSightBlocked,

    /// <summary>
    /// Target is of an invalid type.
    /// </summary>
    InvalidTargetType,

    /// <summary>
    /// Targeting is restricted by faction/relationship rules.
    /// </summary>
    FactionRestriction,

    /// <summary>
    /// Other targeting failure.
    /// </summary>
    Other
}

