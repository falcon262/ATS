using Volo.Abp.Modularity;

namespace ATS;

/* Inherit from this class for your domain layer tests. */
public abstract class ATSDomainTestBase<TStartupModule> : ATSTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
