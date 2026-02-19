using ATS.Samples;
using Xunit;

namespace ATS.EntityFrameworkCore.Domains;

[Collection(ATSTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ATSEntityFrameworkCoreTestModule>
{

}
