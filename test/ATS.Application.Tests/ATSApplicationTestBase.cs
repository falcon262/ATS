using Volo.Abp.Modularity;

namespace ATS;

public abstract class ATSApplicationTestBase<TStartupModule> : ATSTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
