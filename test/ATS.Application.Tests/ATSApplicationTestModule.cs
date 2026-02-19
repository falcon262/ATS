using Volo.Abp.Modularity;

namespace ATS;

[DependsOn(
    typeof(ATSApplicationModule),
    typeof(ATSDomainTestModule)
)]
public class ATSApplicationTestModule : AbpModule
{

}
