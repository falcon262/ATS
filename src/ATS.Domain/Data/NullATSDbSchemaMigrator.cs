using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ATS.Data;

/* This is used if database provider does't define
 * IATSDbSchemaMigrator implementation.
 */
public class NullATSDbSchemaMigrator : IATSDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
