using ATS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ATS.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ATSController : AbpControllerBase
{
    protected ATSController()
    {
        LocalizationResource = typeof(ATSResource);
    }
}
