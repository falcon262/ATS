using Microsoft.Extensions.Localization;
using ATS.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ATS;

[Dependency(ReplaceServices = true)]
public class ATSBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ATSResource> _localizer;

    public ATSBrandingProvider(IStringLocalizer<ATSResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
