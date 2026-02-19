using Volo.Abp.Modularity;

namespace ATS;

[DependsOn(
    typeof(ATSDomainModule),
    typeof(ATSTestBaseModule)
)]
public class ATSDomainTestModule : AbpModule
{

}
