using ATS.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;
using Volo.Abp.BackgroundJobs;

namespace ATS;

[DependsOn(
    typeof(ATSDomainModule),
    typeof(ATSApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpBackgroundJobsAbstractionsModule)
    )]
public class ATSApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ATSApplicationModule>();
        });

        // Configure AI Options
        Configure<AIOptions>(options =>
        {
            configuration.GetSection("AI").Bind(options);
            var openAiSection = configuration.GetSection("OpenAI");
            if (openAiSection.Exists())
            {
                options.ApiKey = openAiSection["ApiKey"] ?? options.ApiKey;
                options.OrganizationId = openAiSection["OrganizationId"] ?? options.OrganizationId;
                options.ProjectId = openAiSection["ProjectId"] ?? options.ProjectId;
            }
        });

        // Enable background jobs
        Configure<AbpBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = true;
        });
    }
}
