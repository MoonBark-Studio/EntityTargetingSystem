using MoonBark.Framework.Core;
using MoonBark.Framework.Targeting;

namespace MoonBark.EntityTargeting;

/// <summary>
/// Registers EntityTargetingSystem services with the Framework module registry.
/// Games must provide a concrete <see cref="ITargetingValidator"/> implementation
/// (faction rules, team logic, etc.) and pass it to this module.
/// </summary>
public sealed class EntityTargetingModule : IFrameworkModule
{
    private readonly ITargetingValidator _validator;

    public EntityTargetingModule(ITargetingValidator validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public void ConfigureServices(IServiceRegistry services)
    {
        services.Register(_validator);
    }
}
