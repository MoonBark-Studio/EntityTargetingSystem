namespace MoonBark.EntityTargetingSystem.Core;

using Friflo.Engine.ECS;

/// <summary>
/// Defines which faction or relationship dispositions an ability can target.
/// </summary>
public struct AbilityTargetingComponent : IComponent, IEquatable<AbilityTargetingComponent>
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
    /// Whether the ability can target entities in the same faction as the caster.
    /// </summary>
    public bool CanTargetSameFaction { get; set; }

    /// <summary>
    /// Creates a new ability targeting component.
    /// </summary>
    /// <param name="canTargetHostile">Whether hostile entities are valid targets.</param>
    /// <param name="canTargetNeutral">Whether neutral entities are valid targets.</param>
    /// <param name="canTargetFriendly">Whether friendly entities are valid targets.</param>
    /// <param name="canTargetSameFaction">Whether same-faction entities are valid targets.</param>
    public AbilityTargetingComponent(bool canTargetHostile, bool canTargetNeutral, bool canTargetFriendly, bool canTargetSameFaction)
    {
        CanTargetHostile = canTargetHostile;
        CanTargetNeutral = canTargetNeutral;
        CanTargetFriendly = canTargetFriendly;
        CanTargetSameFaction = canTargetSameFaction;
    }

    /// <summary>
    /// Creates an unrestricted targeting rule.
    /// </summary>
    public static AbilityTargetingComponent AnyTarget()
    {
        return new AbilityTargetingComponent(true, true, true, true);
    }

    /// <summary>
    /// Creates a hostile-and-neutral-only targeting rule.
    /// </summary>
    public static AbilityTargetingComponent HostileAndNeutralOnly()
    {
        return new AbilityTargetingComponent(true, true, false, false);
    }

    /// <summary>
    /// Creates a neutral-and-friendly-only targeting rule.
    /// </summary>
    public static AbilityTargetingComponent NeutralAndFriendlyOnly()
    {
        return new AbilityTargetingComponent(false, true, true, true);
    }

    /// <summary>
    /// Evaluates whether a target is valid given the supplied disposition values.
    /// </summary>
    /// <param name="isHostile">True when the target is hostile to the caster.</param>
    /// <param name="isNeutral">True when the target is neutral to the caster.</param>
    /// <param name="isFriendly">True when the target is friendly to the caster.</param>
    /// <param name="isSameFaction">True when the target belongs to the same faction.</param>
    /// <returns>True when the target satisfies the rule.</returns>
    public bool CanTarget(bool isHostile, bool isNeutral, bool isFriendly, bool isSameFaction)
    {
        if (isSameFaction && CanTargetSameFaction)
        {
            return true;
        }

        if (isHostile)
        {
            return CanTargetHostile;
        }

        if (isNeutral)
        {
            return CanTargetNeutral;
        }

        if (isFriendly)
        {
            return CanTargetFriendly;
        }

        return false;
    }

    public bool Equals(AbilityTargetingComponent other) =>
        CanTargetHostile == other.CanTargetHostile &&
        CanTargetNeutral == other.CanTargetNeutral &&
        CanTargetFriendly == other.CanTargetFriendly &&
        CanTargetSameFaction == other.CanTargetSameFaction;

    public override bool Equals(object? obj) => obj is AbilityTargetingComponent other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(CanTargetHostile, CanTargetNeutral, CanTargetFriendly, CanTargetSameFaction);
    public static bool operator ==(AbilityTargetingComponent left, AbilityTargetingComponent right) => left.Equals(right);
    public static bool operator !=(AbilityTargetingComponent left, AbilityTargetingComponent right) => !left.Equals(right);
}
