using ATS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ATS.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ATSEntityFrameworkCoreModule),
    typeof(ATSApplicationContractsModule)
)]
public class ATSDbMigratorModule : AbpModule
{
}
