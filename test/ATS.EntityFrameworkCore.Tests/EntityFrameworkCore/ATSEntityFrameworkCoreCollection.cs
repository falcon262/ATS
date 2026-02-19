using Xunit;

namespace ATS.EntityFrameworkCore;

[CollectionDefinition(ATSTestConsts.CollectionDefinitionName)]
public class ATSEntityFrameworkCoreCollection : ICollectionFixture<ATSEntityFrameworkCoreFixture>
{

}
