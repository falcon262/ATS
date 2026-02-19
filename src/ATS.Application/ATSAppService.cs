using ATS.Localization;
using Volo.Abp.Application.Services;

namespace ATS;

/* Inherit your application services from this class.
 */
public abstract class ATSAppService : ApplicationService
{
    protected ATSAppService()
    {
        LocalizationResource = typeof(ATSResource);
    }
}
