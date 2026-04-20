namespace MoonBark.EntityTargetingSystem.Core;

using Friflo.Engine.ECS;
using System.Numerics;

/// <summary>
/// Context for targeting validation.
/// </summary>
/// <remarks>
/// This struct provides all the information needed to validate a targeting request.
/// </remarks>
public readonly record struct TargetingContext(
    Entity Caster,
    Entity? Target,
    Vector2? TargetPosition,
    string AbilityId,
    float Range,
    bool RequiresLineOfSight
) : IEquatable<TargetingContext>
{
    public bool Equals(TargetingContext other) =>
        Caster == other.Caster &&
        Target == other.Target &&
        TargetPosition == other.TargetPosition &&
        AbilityId == other.AbilityId &&
        Range == other.Range &&
        RequiresLineOfSight == other.RequiresLineOfSight;

    public static bool operator ==(TargetingContext left, TargetingContext right) => left.Equals(right);
    public static bool operator !=(TargetingContext left, TargetingContext right) => !left.Equals(right);
}

