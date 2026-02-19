using System.Threading.Tasks;

namespace ATS.Data;

public interface IATSDbSchemaMigrator
{
    Task MigrateAsync();
}
