namespace MoonBark.EntityTargetingSystem.Tests;

using MoonBark.EntityTargetingSystem.Core;
using MoonBark.Framework.Targeting;
using Xunit;

public sealed class AbilityTargetingComponentTests
{
    [Fact]
    public void AnyTarget_AllowsEveryDisposition()
    {
        AbilityTargetingComponent component = AbilityTargetingComponent.AnyTarget();

        Assert.True(component.CanTarget(true, false, false, false));
        Assert.True(component.CanTarget(false, true, false, false));
        Assert.True(component.CanTarget(false, false, true, false));
        Assert.True(component.CanTarget(false, false, false, true));
    }

    [Fact]
    public void HostileAndNeutralOnly_RejectsFriendly()
    {
        AbilityTargetingComponent component = AbilityTargetingComponent.HostileAndNeutralOnly();

        Assert.True(component.CanTarget(true, false, false, false));
        Assert.True(component.CanTarget(false, true, false, false));
        Assert.False(component.CanTarget(false, false, true, false));
        Assert.False(component.CanTarget(false, false, true, true));
    }

    [Fact]
    public void Failure_CapturesReasonAndKind()
    {
        TargetingResult result = TargetingResult.Failure(TargetingFailureKind.NoLineOfSight, "blocked");

        Assert.False(result.CanTarget);
        Assert.Equal("blocked", result.FailureReason);
        Assert.Equal(TargetingFailureKind.NoLineOfSight, result.FailureKind);
    }
}
