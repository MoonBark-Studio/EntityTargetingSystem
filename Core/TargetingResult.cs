namespace EntityTargetingSystem;

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

