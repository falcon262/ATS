using ATS.Samples;
using Xunit;

namespace ATS.EntityFrameworkCore.Applications;

[Collection(ATSTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ATSEntityFrameworkCoreTestModule>
{

}
